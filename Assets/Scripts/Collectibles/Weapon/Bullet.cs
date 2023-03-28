using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage = 2f;
    //public float bullet_size = 1f;

    // Start is called before the first frame update
    void Start()
    {
        damage *= RuneManager.Instance.damage_rune;
        speed *= RuneManager.Instance.bullet_speed_rune;
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCritial = false;
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            //critical hit
            if (Random.Range(0, 100) < RuneManager.Instance.critial_hit_rune)
            {
                damage *= 2f;
                isCritial = true;
            }

            enemy.setpv(enemy.getpv() - damage);
            enemy.displayDamage(damage, isCritial);
            if (enemy.getpv() <= 0)
            {
                enemy.die();
            }
            transform.destroy();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
