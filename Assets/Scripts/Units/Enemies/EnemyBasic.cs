using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : Enemy
{
    public EnemyBasic()
    {
        pv = 200.0f;
        speed = 5.0f;
        max_droppable_quantity = 5;
        score_value = 1;
    }

    public float jump_force = 250.0f;
    private GameObject player;
    private Rigidbody2D rigid_body;

    [Header("Ground Collision Variable")]
    [Header("Layer Masks")]
    [SerializeField]
    public LayerMask _ground_layer;
    public LayerMask _edge_layer;

    [SerializeField]
    private float _ground_raycast_length;

    [SerializeField]
    private Vector3 _ground_raycast_offset;
    public bool _on_ground;

    private bool is_jumping = false;
    private float next_jump_time;
    public float raycastDistance = 2f;

    private Vector3 last_position;

    public AudioClip death_sound;

    void Start()
    {
        player = GameObject.Find("Player");
        rigid_body = GetComponent<Rigidbody2D>();

        switch (DifficultyManager.Instance.current_difficulty)
        {
            case 0:
                pv = 1000f;
                break;
            case 1:
                pv = 1500f;
                break;
            case 2:
                pv = 2000f;
                break;
            default:
                pv = 2500f;
                break;
        }
    }

    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, _edge_layer);

        if (collider != null)
        {
            Destroy(gameObject);
        }
        check_ground_collision();
        transform.position += Vector3.left * speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            raycastDistance,
            _ground_layer
        );
        if (
            (player.transform.position.y > transform.position.y + 1f) && _on_ground && !is_jumping
            || hit.collider != null
        )
        {
            is_jumping = true;
            next_jump_time = Time.time + 0.1f;
        }
        if (is_jumping == true && Mathf.Abs(next_jump_time - Time.time) < 0.01f)
        {
            rigid_body.AddForce(new Vector2(0f, jump_force));
            is_jumping = false;
        }
        if (Vector3.Distance(last_position, transform.position) < 0.001f)
        {
            rigid_body.AddForce(new Vector2(0f, jump_force));
        }
        last_position = transform.position;
    }

    private void check_ground_collision()
    {
        Debug.DrawRay(transform.position + _ground_raycast_offset, Vector2.down, Color.red);
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

    private void OnDestroy()
    {
        AudioSystem.Instance.play_sound(death_sound);
    }
}
