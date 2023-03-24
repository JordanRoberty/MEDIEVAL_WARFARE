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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            //critical hit
            if (Random.Range(0, 100) < RuneManager.Instance.critial_hit_rune)
            {
                bullet.damage *= 2f;
            }

            if ((pv -= bullet.damage) <= 0)
            {
                transform.destroy();
                UIManager.Instance.update_score(1);
            }

            bullet.transform.destroy();
        }
    }
}
