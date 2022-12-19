using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    private float _horizontal_direction;
    private float _vertical_direction;

    [SerializeField] private LayerMask _ground_layer;
    [SerializeField] private PlayerData _player;

   
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
        
        if (_player.on_ground)
        {
            apply_ground_linear_drag();
            crouch();
            _player.extra_jumps_count = _player.extra_jumps;
            _player.rb.gravityScale = 1f;
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
        _player.rb.AddForce(new Vector2(_horizontal_direction, 0f) * _player.movement_acceleration);

        if(Mathf.Abs(_player.rb.velocity.x) > _player.max_move_speed)
        {
            _player.rb.velocity = new Vector2(Mathf.Sign(_player.rb.velocity.x) * _player.max_move_speed, _player.rb.velocity.y);
        }
    }

    //Check is the player is crouching and change his collider's size if he is
    private void crouch()
    {   
        if(_vertical_direction < 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.5f);
            _player.is_crouching = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            _player.is_crouching = false;
        }
        
    }


    private void apply_ground_linear_drag()
    {
        if(Mathf.Abs(_horizontal_direction) < 0.4f || _player.changing_direction)
        {
            _player.rb.drag = _player.ground_linear_drag;
        }
        else
        {
            _player.rb.drag = 0f;
        }
    }

     private void apply_air_linear_drag()
    {
        _player.rb.drag = _player.air_linear_drag;
    }

    private void jump()
    {
        if (!_player.on_ground)
        {
            _player.extra_jumps_count--;
        }

        //If the player is crouching, the player jump a little bit higher (25% as we reduce his size by 50% (25% top side, 25% bot side))
        if (_player.is_crouching)
        {
            _player.rb.velocity = new Vector2(_player.rb.velocity.x, 0f);
            _player.rb.AddForce(Vector2.up * 1.25f * _player.jump_force, ForceMode2D.Impulse);
        }
        else
        {
            _player.rb.velocity = new Vector2(_player.rb.velocity.x, 0f);
            _player.rb.AddForce(Vector2.up * _player.jump_force, ForceMode2D.Impulse);
        }
        
    }

    private void set_gravity()
    {    
        //jump Cut
        if ( _player.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _player.rb.gravityScale = _player.low_jump_fall_gravity;
        }
        //fast fall
        else if(_player.rb.velocity.y < 0f && _vertical_direction <0)
        {
             _player.rb.gravityScale = _player.fastfall_gravity;
            if(Mathf.Abs(_player.rb.velocity.y) > _player.fastfall_max_speed)
            {
                _player.rb.velocity = new Vector2(_player.rb.velocity.x, Mathf.Sign(_player.rb.velocity.y) * _player.fastfall_max_speed);
                
            }
        } 
        //jump
        else if(_player.rb.velocity.y < 0f)
        {
            _player.rb.gravityScale = _player.fall_gravity;
        }   
        else
        {
            _player.rb.gravityScale = 1f;
        }

       

    }

    private void check_ground_collision()
    {
        // To check the ground collision, we cast rays from the player to the ground. One in front of the player
        // And the other in his back to still detect the ground if the player is on the edge of the ground.
        if(_player.is_crouching)
        {
            _player.on_ground = Physics2D.Raycast(transform.position + _player.ground_raycast_offset , Vector2.down, _player.ground_raycast_length * 0.5f, _ground_layer) ||
                    Physics2D.Raycast(transform.position - _player.ground_raycast_offset , Vector2.down, _player.ground_raycast_length * 0.5f, _ground_layer);
        }
        else
        {
            _player.on_ground = Physics2D.Raycast(transform.position + _player.ground_raycast_offset , Vector2.down, _player.ground_raycast_length, _ground_layer) ||
                    Physics2D.Raycast(transform.position - _player.ground_raycast_offset , Vector2.down, _player.ground_raycast_length, _ground_layer);
        }
        
    }

    //These guizmos show the ray that is cast to check the ground collision
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        if(_player.is_crouching)
        {
            Gizmos.DrawLine(transform.position + _player.ground_raycast_offset, transform.position + _player.ground_raycast_offset + Vector3.down * _player.ground_raycast_length * 0.5f);
            Gizmos.DrawLine(transform.position - _player.ground_raycast_offset, transform.position - _player.ground_raycast_offset + Vector3.down * _player.ground_raycast_length * 0.5f); 
        }
        else
        {
            Gizmos.DrawLine(transform.position + _player.ground_raycast_offset, transform.position + _player.ground_raycast_offset + Vector3.down * _player.ground_raycast_length);
            Gizmos.DrawLine(transform.position - _player.ground_raycast_offset, transform.position - _player.ground_raycast_offset + Vector3.down * _player.ground_raycast_length); 
        }
         

    }
}
