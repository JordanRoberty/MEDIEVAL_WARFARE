using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weapon_1 : weapon
{    // Update is called once per frame
    override public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
