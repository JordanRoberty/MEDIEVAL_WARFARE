using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weapon_2 : weapon
{    // Update is called once per frame
    GameObject bullet;
    override public void Shoot()
    {
        bullet =Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.localScale =  bullet.transform.localScale *  RuneManager.Instance.projectile_size_rune;

        bullet =Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 10));
        bullet.transform.localScale =  bullet.transform.localScale *  RuneManager.Instance.projectile_size_rune;

        bullet =Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -10));
        bullet.transform.localScale =  bullet.transform.localScale *  RuneManager.Instance.projectile_size_rune;

    }
}
