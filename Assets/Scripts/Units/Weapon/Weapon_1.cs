using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weapon_1 : weapon
{    // Update is called once per frame
    GameObject bullet;

    override public void Shoot()
    {
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.localScale =  bullet.transform.localScale *  Runes.projectile_size_rune;
    }
}
