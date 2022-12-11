using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask _ground_layer;

    [Header("Movement Variables")]
    [SerializeField] private float _movement_acceleration = 50f;
    [SerializeField] private float _max_move_speed = 12f;
    [SerializeField] private float _ground_linear_drag = 10f; //a.k.a deceleration
    private float _horizontal_direction;
    private bool _changing_direction => (_rb.velocity.x > 0f && _horizontal_direction < 0f) || (_rb.velocity.x < 0f && _horizontal_direction > 0f);

    [Header("jump Variables")]
    [SerializeField] private float _jump_force = 12f;
    [SerializeField] private float _air_linear_drag = 2.5f;
    [SerializeField] private float _fall_gravity = 8f;
    [SerializeField] private float _low_jump_fall_gravity = 5f;
    [SerializeField] private int _extra_jumps = 1;
    [SerializeField] private float _fastfall_gravity = 500f; 
	[SerializeField] private float _fastfall_max_speed = 50.0f;
    private int _extra_jumps_count;
    private bool _can_jump => Input.GetButtonDown("Jump") && (_on_ground || _extra_jumps_count > 0); 

    [Header("Ground Collision Variable")]
    [SerializeField] private float _ground_raycast_length;
    [SerializeField] private Vector3 _ground_raycast_offset;
    private bool _on_ground;

    //crouch variables
    private bool _is_crouching = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    private void Update()
    {
        _horizontal_direction = get_input().x;
        // I don't know why I had to put the jump() call here
        // But if we move it into FixedUpdate it's not responsive 
        if (_can_jump) jump();
    }

    private void FixedUpdate()
    {
        check_ground_collision();
        move_character();
        
        if (_on_ground)
        {
            apply_ground_linear_drag();
            crouch();
            _extra_jumps_count = _extra_jumps;
            _rb.gravityScale = 1f;
        }
        else
        {
            apply_air_linear_drag();
            set_gravity();
        }

    }

    private Vector2 get_input()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    //Move the character on the horizontal direction
    private void move_character()
    {
        _rb.AddForce(new Vector2(_horizontal_direction, 0f) * _movement_acceleration);

        if(Mathf.Abs(_rb.velocity.x) > _max_move_speed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _max_move_speed, _rb.velocity.y);
        }
    }

    //Check is the player is crouching and change his collider's size if he is
    private void crouch()
    {   
        if(get_input().y < 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.5f);
            _is_crouching = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            _is_crouching = false;
        }
        
    }


    private void apply_ground_linear_drag()
    {
        if(Mathf.Abs(_horizontal_direction) < 0.4f || _changing_direction)
        {
            _rb.drag = _ground_linear_drag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

     private void apply_air_linear_drag()
    {
        _rb.drag = _air_linear_drag;
    }

    private void jump()
    {
        if (!_on_ground)
        {
            _extra_jumps_count--;
        }

        //If the player is crouching, the player jump a little bit higher (25% as we reduce his size by 50% (25% top side, 25% bot side))
        if (_is_crouching)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * 1.25f * _jump_force, ForceMode2D.Impulse);
        }
        else
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * _jump_force, ForceMode2D.Impulse);
        }
        
    }

    private void set_gravity()
    {    
        //jump Cut
        if ( _rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _low_jump_fall_gravity;
        }
        //fast fall
        else if(_rb.velocity.y < 0f && get_input().y <0)
        {
             _rb.gravityScale = _fastfall_gravity;
            if(Mathf.Abs(_rb.velocity.y) > _fastfall_max_speed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Sign(_rb.velocity.y) * _fastfall_max_speed);
                
            }
        } 
        //jump
        else if(_rb.velocity.y < 0f)
        {
            _rb.gravityScale = _fall_gravity;
        }   
        else
        {
            _rb.gravityScale = 1f;
        }

       

    }

    private void check_ground_collision()
    {
        // To check the ground collision, we cast rays from the player to the ground. One in front of the player
        // And the other in his back to still detect the ground if the player is on the edge of the ground.
        if(_is_crouching)
        {
            _on_ground = Physics2D.Raycast(transform.position + _ground_raycast_offset , Vector2.down, _ground_raycast_length * 0.5f, _ground_layer) ||
                    Physics2D.Raycast(transform.position - _ground_raycast_offset , Vector2.down, _ground_raycast_length * 0.5f, _ground_layer);
        }
        else
        {
            _on_ground = Physics2D.Raycast(transform.position + _ground_raycast_offset , Vector2.down, _ground_raycast_length, _ground_layer) ||
                    Physics2D.Raycast(transform.position - _ground_raycast_offset , Vector2.down, _ground_raycast_length, _ground_layer);
        }
        
    }

    //These guizmos show the ray that is cast to check the ground collision
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        if(_is_crouching)
        {
            Gizmos.DrawLine(transform.position + _ground_raycast_offset, transform.position + _ground_raycast_offset + Vector3.down * _ground_raycast_length * 0.5f);
            Gizmos.DrawLine(transform.position - _ground_raycast_offset, transform.position - _ground_raycast_offset + Vector3.down * _ground_raycast_length * 0.5f); 
        }
        else
        {
            Gizmos.DrawLine(transform.position + _ground_raycast_offset, transform.position + _ground_raycast_offset + Vector3.down * _ground_raycast_length);
            Gizmos.DrawLine(transform.position - _ground_raycast_offset, transform.position - _ground_raycast_offset + Vector3.down * _ground_raycast_length); 
        }
         

    }
}
