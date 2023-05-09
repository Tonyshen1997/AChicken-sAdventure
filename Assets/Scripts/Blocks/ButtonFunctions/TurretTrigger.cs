using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    [SerializeField] GameObject turret;
    private TurretController turretController;

    private void Start()
    {
        turretController = turret.GetComponent<TurretController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            turretController.Shoot();
        }
    }
}
