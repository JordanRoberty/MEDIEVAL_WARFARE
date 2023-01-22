using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float pv;
    protected float speed;
    protected float damage;

    public float get_damage()
    {
        return damage;
    }

    public void set_damage(float new_damage)
    {
        damage = new_damage;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bullet bullet = hitInfo.GetComponent<Bullet>();
        if (bullet != null)
        {
            pv -= bullet.damage;
            print("Aie");
            if (pv <= 0){
                Destroy(gameObject);
            }
        }
    }
}
