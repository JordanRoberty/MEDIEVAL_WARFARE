using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weapon_2 : weapon
{    // Update is called once per frame
    override public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 10));
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -10));
    }
}
