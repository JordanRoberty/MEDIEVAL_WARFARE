using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement2D : MonoBehaviour
{
    [SerializeField] private PlayerData _player;

    [Header("Movement Variables")]
    [SerializeField] private float movement_acceleration = 50f;
    [SerializeField] private float max_move_speed = 12f;
    [SerializeField] private float ground_linear_drag = 10f; //a.k.a deceleration
    private bool changing_direction;
    private bool is_crouching = false;

    [Header("jump Variables")]
    [SerializeField] private float jump_force = 12f;
    [SerializeField] private float air_linear_drag = 2.5f;
    [SerializeField] private float fall_gravity = 8f;
    [SerializeField] private float low_jump_fall_gravity = 5f;
    [SerializeField] private int extra_jumps = 1;
    [SerializeField] private float fastfall_gravity = 500f; 
	[SerializeField] private float fastfall_max_speed = 50.0f;
    private int extra_jumps_count = 0;
    private bool can_jump = false;
    private bool on_ground = true;

    [Header("Ground Collision Variable")]
    [SerializeField] private float ground_raycast_length = 0.8f;
    [SerializeField] private Vector3 ground_raycast_offset = new Vector3(.3f, 0f, 0f);
    [SerializeField] private LayerMask _ground_layer;


    private float _horizontal_direction;
    private float _vertical_direction;

   
    private void Start()
    {   
    }

    private void Update()
    {
        _horizontal_direction = get_input().x;
        _vertical_direction = get_input().y;

        _player.changing_direction = (_player.rb.velocity.x > 0f && _horizontal_direction < 0f) || (_player.rb.velocity.x < 0f && _horizontal_direction > 0f);
        _player.can_jump = Input.GetButtonDown("Jump") && (_player.on_ground || _player.extra_jumps_count > 0); 
        
        // I don't know why I had to put the jump() call here
        // But if we move it into FixedUpdate it's not responsive 
        if (_player.can_jump) jump();
    }

    private void FixedUpdate()
    {
        check_ground_collision();
        move_character();
        
        if (on_ground)
        {
            apply_ground_linear_drag();
            crouch();
            extra_jumps_count = extra_jumps;
            _player.rb.gravityScale = 1f;
        }
        else
        {
            apply_air_linear_drag();
            set_gravity();
        }

    }

///Return a Vector 2 that contains the horizontal input and place it on the X value and the horizontal input and place it on the Y value
    private Vector2 get_input()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    ///Move the character on the horizontal direction, if the character is faster than max_move_speed we clamp it at max speed
    private void move_character()
    {
        _player.rb.AddForce(new Vector2(_horizontal_direction, 0f) * movement_acceleration);

        if(Mathf.Abs(_player.rb.velocity.x) > max_move_speed)
        {
            _player.rb.velocity = new Vector2(Mathf.Sign(_player.rb.velocity.x) * max_move_speed, _player.rb.velocity.y);
        }
    }

    ///Check is the player is crouching and change his collider size if he is
    private void crouch()
    {   
        if(_vertical_direction < 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.5f);
            is_crouching = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            is_crouching = false;
        }
        
    }

    ///Apply the ground linear drag to the character
    private void apply_ground_linear_drag()
    {
        if(Mathf.Abs(_horizontal_direction) < 0.4f || changing_direction)
        {
            _player.rb.drag = ground_linear_drag;
        }
        else
        {
            _player.rb.drag = 0f;
        }
    }

    ///Apply the air linear drag to the character
     private void apply_air_linear_drag()
    {
        _player.rb.drag = air_linear_drag;
    }


    ///It makes the character jump. If the character is not on ground, it retrieves him an extra jump
    /// then if the player is crouching, the player jump a little bit higher (25% as we reduce his size by 50% (25% top side, 25% bot side))

    private void jump()
    {
        if (!on_ground)
        {
            extra_jumps_count--;
        }

        if (is_crouching)
        {
            _player.rb.velocity = new Vector2(_player.rb.velocity.x, 0f);
            _player.rb.AddForce(Vector2.up * 1.25f * jump_force, ForceMode2D.Impulse);
        }
        else
        {
            _player.rb.velocity = new Vector2(_player.rb.velocity.x, 0f);
            _player.rb.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
        }
        
    }

    ///This function set the value of the gravity.
    ///The value depends of the player movement (jump cut, fast fall, jump)
    private void set_gravity()
    {    
        //jump Cut
        if ( _player.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _player.rb.gravityScale = low_jump_fall_gravity;
        }
        //fast fall
        else if(_player.rb.velocity.y < 0f && _vertical_direction <0)
        {
             _player.rb.gravityScale = fastfall_gravity;
            if(Mathf.Abs(_player.rb.velocity.y) > fastfall_max_speed)
            {
                _player.rb.velocity = new Vector2(_player.rb.velocity.x, Mathf.Sign(_player.rb.velocity.y) * fastfall_max_speed);
                
            }
        } 
        //jump
        else if(_player.rb.velocity.y < 0f)
        {
            _player.rb.gravityScale = fall_gravity;
        }   
        else
        {
            _player.rb.gravityScale = 1f;
        }

       

    }


    /// To check the ground collision, we cast rays from the player to the ground.
    /// One in front of the player and the other in his back to still detect the ground if the player is on the edge of the ground.
    private void check_ground_collision()
    {
        if(is_crouching)
        {
            on_ground = Physics2D.Raycast(transform.position + ground_raycast_offset , Vector2.down, ground_raycast_length * 0.5f, _ground_layer) ||
                    Physics2D.Raycast(transform.position - ground_raycast_offset , Vector2.down, ground_raycast_length * 0.5f, _ground_layer);
        }
        else
        {
            on_ground = Physics2D.Raycast(transform.position + ground_raycast_offset , Vector2.down, ground_raycast_length, _ground_layer) ||
                    Physics2D.Raycast(transform.position - ground_raycast_offset , Vector2.down, ground_raycast_length, _ground_layer);
        }
        
    }

    //These guizmos show the ray that is cast to check the ground collision
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        if(is_crouching)
        {
            Gizmos.DrawLine(transform.position + ground_raycast_offset, transform.position + ground_raycast_offset + Vector3.down * ground_raycast_length * 0.5f);
            Gizmos.DrawLine(transform.position - ground_raycast_offset, transform.position - ground_raycast_offset + Vector3.down * ground_raycast_length * 0.5f); 
        }
        else
        {
            Gizmos.DrawLine(transform.position + ground_raycast_offset, transform.position + ground_raycast_offset + Vector3.down * ground_raycast_length);
            Gizmos.DrawLine(transform.position - ground_raycast_offset, transform.position - ground_raycast_offset + Vector3.down * ground_raycast_length); 
        }
         

    }
}
