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

    public void take_damages(float damage)
    {
        pv -= (damage);

        if (pv <= 0)
        {
            transform.destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bullet bullet = hitInfo.GetComponent<Bullet>();
        if (bullet != null)
        {
            //critical hit
            if(Random.Range(0,100) < RuneManager.Instance.critial_hit_rune){
                bullet.damage *= 2f;
            }
            
            pv -= bullet.damage;
            if (pv <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
