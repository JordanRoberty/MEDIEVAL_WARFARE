using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : Enemy
{
    public EnemyBasic()
    {
        pv = 200.0f;
        speed = 5.0f;
        damage = 50.0f;
    }

    public GameObject player;
    public float jump_force = 100.0f;
    private Rigidbody2D rigid_body;

    [Header("Ground Collision Variable")]
    [Header("Layer Masks")]
    [SerializeField]
    private LayerMask _ground_layer;

    [SerializeField]
    private float _ground_raycast_length;

    [SerializeField]
    private Vector3 _ground_raycast_offset;
    private bool _on_ground;

    private bool is_jumping = false;
    private float next_jump_time;

    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        check_ground_collision();
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (player.transform.position.y > transform.position.y + 0.1 && _on_ground && !is_jumping)
        {
            is_jumping = true;
            next_jump_time = Time.time + 0.1f;
        }
        if (is_jumping == true && Mathf.Abs(next_jump_time - Time.time) < 0.01f)
        {
            rigid_body.AddForce(new Vector2(0f, jump_force));
            is_jumping = false;
        }
    }

    private void check_ground_collision()
    {
        _on_ground =
            Physics2D.Raycast(
                transform.position + _ground_raycast_offset,
                Vector2.down,
                _ground_raycast_length,
                _ground_layer
            )
            || Physics2D.Raycast(
                transform.position - _ground_raycast_offset,
                Vector2.down,
                _ground_raycast_length,
                _ground_layer
            );
    }
}
