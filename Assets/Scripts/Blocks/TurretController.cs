using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // public fields
    [SerializeField] public GameObject target;
    [SerializeField] public Transform muzzleMain;
    [SerializeField] public GameObject muzzleEff;
    [SerializeField] public Transform turretHead;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float cooldown;
    [SerializeField] public float lookSpeed;
    [SerializeField] public ParticleSystem part;

    // self fields
    private Animator animator;
    public float timer = 0;
    public bool fireReady = true;   

    // test Mode
    public bool fire;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target) FollowTarget();

        if (!fireReady)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                fireReady = true;
                animator.SetTrigger("Reload");
            }
        }

        if (fire)
        {
            Shoot();
            fire = false;
        }

    }

    public void Shoot()
    {
        if (!fireReady) return;
        //if (curProjectile == null) return;

        fireReady = false;
        animator.SetTrigger("Fire");
        part.Play();
        Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
        GameObject bullet = Instantiate(bulletPrefab, muzzleMain.transform.position, muzzleMain.rotation);
        Projectiles projectile = bullet.GetComponent<Projectiles>();
        projectile.target = target.transform;
        projectile.active = true;
    }


    private void FollowTarget()
    {
        Vector3 targerDir = target.transform.position - turretHead.position;
        targerDir.y = 0;
        turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.rotation, Quaternion.LookRotation(targerDir), lookSpeed * Time.deltaTime);
    }
}
