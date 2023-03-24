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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
