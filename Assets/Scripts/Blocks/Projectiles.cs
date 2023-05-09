using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float speed;
    [SerializeField] float fuzeTimer;
    [SerializeField] Rigidbody rb;


    private bool triggered = false;
    public bool active = false;

    void Update()
    {
        if (active)
        {
            if (transform.position.y < -0.2F)
            {
                Explosion();
            }

            fuzeTimer -= Time.deltaTime;
            if (fuzeTimer < 0)
            {
                Explosion();
            }

            if (!triggered)
            {
                Vector3 Vo = CalculateTrajectory(target.transform.position, transform.position, 1);
                rb.velocity = Vo;
                triggered = true;
            }
        }
    }

    private void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Boss_AI boss = target.gameObject.GetComponent<Boss_AI>();
        boss.getHit();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Boss")
        {
            Explosion();
        }
    }

    private Vector3 CalculateTrajectory(Vector3 targetVector3, Vector3 originVector3, float time)
    {
        Vector3 targetDis = targetVector3 - originVector3;
        Vector3 targetDisXZ = targetDis;
        targetDisXZ.y = 0;

        float Sy = targetDis.y;
        float Sxz = targetDisXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 res = targetDisXZ.normalized;
        res *= Vxz;
        res.y = Vy;
        return res;
    }

}
