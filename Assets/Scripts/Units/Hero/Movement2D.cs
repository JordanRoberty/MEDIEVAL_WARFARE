using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public Camera camera;

    [Header("Movement Variables")]
    [SerializeField]
    private float _move_speed = 12f;
    private float _crouch_speed;
    private float _current_speed;

    [SerializeField]
    private bool _changing_direction= false;
    private bool _is_crouching = false;

    [Header("jump Variables")]
    [SerializeField]
    private float _jump_force = 12f;

    [SerializeField]
    private float _fall_gravity = 8f;

    [SerializeField]
    private int _extra_jumps = 1;

    [SerializeField]
    private float _fast_fall_gravity = 500f;

    [SerializeField]
    private float _fastfall_max_speed = 50.0f;
    private int _extra_jumps_count = 0;
    private bool _can_jump = false;
    private bool _on_ground = true;
    private float _default_speed;
    private float _default_crouch_speed;
    private float _current_default_speed;


    [Header("Ground Collision Variable")]
    [SerializeField]
    private float _ground_raycast_length = 0.8f;

    [SerializeField]
    private Vector3 _ground_raycast_offset = new Vector3(.3f, 0f, 0f);

    [SerializeField]
    private LayerMask _ground_layer;

    private Rigidbody2D _rigid_body;
    private float _horizontal_direction;
    private float _vertical_direction;
    public Animator animator;
    bool facingRight = true;
    private BoxCollider2D boxCollider;
    private bool is_boss_scene = false;
    private bool is_jumping = false;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;



    public void Init()
    {
        _rigid_body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        //RUNE MODIFIER
        _move_speed *= transform.GetComponent<RuneManager>().speed_rune;
        _jump_force *= transform.GetComponent<RuneManager>().high_jump_rune;
        if(transform.GetComponent<RuneManager>().triple_jump_rune)
        {
            _extra_jumps++;
        }

        //BOSS SCENE
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("boss_level_"))
        {
            is_boss_scene = true;
        }else{
        _default_speed = camera.GetComponent<CameraBehavior>().speed;

        }

        _current_speed = _move_speed;
        _current_default_speed = _default_speed;

        _crouch_speed = _move_speed * 0.5f;
        _default_crouch_speed = _default_speed * 0.5f;
    }

    private void Update()
    {
        _horizontal_direction = get_input().x;
        _vertical_direction = get_input().y;

        if (_horizontal_direction > 0 && !facingRight)
        {
            if (is_boss_scene)
            {
                Flip();
                FlipSword();
            }
            animator.speed = 2.0f;
            animator.SetBool("IsRunning", true);
        }

        if (_horizontal_direction == 0)
        {
            animator.SetBool("IsRunning", false);
            animator.speed = 1.0f;
        }
        if (_horizontal_direction < 0 && facingRight)
        {
            if (is_boss_scene)
            {
                Flip();
                FlipSword();
            }
            animator.speed = 0.5f;
            animator.SetBool("IsRunning", true);
        }

        _changing_direction =
            (_rigid_body.velocity.x > 0f && _horizontal_direction < 0f)
            || (_rigid_body.velocity.x < 0f && _horizontal_direction > 0f);
        _can_jump |= Input.GetButtonDown("Jump");
    }


     void LateUpdate()
    {
        //Clmp the player to the screen
        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 viewPos = transform.position;
        //clamp the x position to the screen bounds
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x *-1 + objectWidth, screenBounds.x  - objectWidth);
        transform.position = viewPos;
    }
    private void FixedUpdate()
    {
        check_ground_collision();
        move_character();
        if (_on_ground)
        {
            crouch();
            _extra_jumps_count = _extra_jumps;
            _rigid_body.gravityScale = 1f;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsAirborn", false);
        }
        else
        {
            set_gravity();
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsAirborn", true);
        }

        if (_can_jump && _extra_jumps_count > 0)
            jump();
        _can_jump = false;
    }

    ///Return a Vector 2 that contains the horizontal input and place it on the X value and the horizontal input and place it on the Y value
    private Vector2 get_input()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    ///Move the character on the horizontal direction, if the character is faster than _move_speed we clamp it at max speed
    private void move_character()
    {
        //Check if the player doesn't move, the character is moving by a default speed
        if (_horizontal_direction == 0)
        {
            if (!is_boss_scene)
            {
                _rigid_body.velocity = new Vector2(
                        _current_default_speed,
                        _rigid_body.velocity.y);
            }else{
                _rigid_body.velocity = new Vector2(
                        0f,
                        _rigid_body.velocity.y);
            }
        }
        else
        {
            _rigid_body.velocity = new Vector2(
                _horizontal_direction * _current_speed,
                _rigid_body.velocity.y
            );
        }
    }

    ///Check is the player is crouching and change his collider size if he is
    private void crouch()
    {
        if (_vertical_direction < 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.425f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.25f);
            animator.SetBool("IsCrouching", true);
            _current_speed = _crouch_speed;
            _current_default_speed = _default_crouch_speed;
            _is_crouching = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.85f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.13f);
            animator.SetBool("IsCrouching", false);
            _current_speed = _move_speed;
            _current_default_speed = _default_speed;
            _is_crouching = false;
        }
    }

    ///It makes the character jump. If the character is not on ground, it retrieves him an extra jump
    /// then if the player is crouching, the player jump a little bit higher (25% as we reduce his size by 50% (25% top side, 25% bot side))

    private void jump()
    {
        if (!_on_ground)
        {
            _extra_jumps_count--;
            animator.SetBool("IsAirborn", true);
        }

        if (_is_crouching)
        {
            _rigid_body.velocity = new Vector2(_rigid_body.velocity.x, 0f);
            _rigid_body.AddForce(Vector2.up * 1.25f * _jump_force, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsAirborn", true);
        }
        else
        {
            _rigid_body.velocity = new Vector2(_rigid_body.velocity.x, 0f);
            _rigid_body.AddForce(Vector2.up * _jump_force, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsAirborn", true);
        }
    }

    ///This function set the value of the gravity.
    ///The value depends of the player movement (jump cut, fast fall, jump)
    private void set_gravity()
    {
        if(!_can_jump)
        {
            _rigid_body.gravityScale = _fall_gravity;
        }else
        {
            _rigid_body.gravityScale = 1f;
        }

        //fast fall
        if(_vertical_direction < 0)
        {
            _rigid_body.gravityScale = _fast_fall_gravity;
            if (Mathf.Abs(_rigid_body.velocity.y) > _fastfall_max_speed)
            {
                _rigid_body.velocity = new Vector2(
                    _rigid_body.velocity.x,
                    Mathf.Sign(_rigid_body.velocity.y) * _fastfall_max_speed
                );
            }
        }
        
    }

    /// To check the ground collision, we cast rays from the player to the ground.
    /// One in front of the player and the other in his back to still detect the ground if the player is on the edge of the ground.
    private void check_ground_collision()
    {
        //if(_is_crouching)
        //{
        //    _on_ground = Physics2D.Raycast(transform.position + _ground_raycast_offset , Vector2.down, _ground_raycast_length * 0.5f, _ground_layer) ||
        //            Physics2D.Raycast(transform.position - _ground_raycast_offset , Vector2.down, _ground_raycast_length * 0.5f, _ground_layer);
        //    animator.SetBool("IsAirborn", false);
        //    animator.SetBool("IsJumping", false);
        //}
        //else
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
            animator.SetBool("IsAirborn", false);
            animator.SetBool("IsJumping", false);
        }
    }

    //These guizmos show the ray that is cast to check the ground collision
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_is_crouching)
        {
            Gizmos.DrawLine(
                transform.position + _ground_raycast_offset * 2,
                transform.position
                    + _ground_raycast_offset
                    + Vector3.down * _ground_raycast_length * 0.5f
            );
            Gizmos.DrawLine(
                transform.position - _ground_raycast_offset,
                transform.position
                    - _ground_raycast_offset
                    + Vector3.down * _ground_raycast_length * 0.5f
            );
            animator.SetBool("IsCrouching", true);
        }
        else
        {
            Gizmos.DrawLine(
                transform.position + _ground_raycast_offset * 3,
                transform.position
                    + _ground_raycast_offset * 3
                    + Vector3.down * _ground_raycast_length
            );
            Gizmos.DrawLine(
                transform.position - _ground_raycast_offset,
                transform.position - _ground_raycast_offset + Vector3.down * _ground_raycast_length
            );
            animator.SetBool("IsCrouching", false);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void FlipSword()
    {
        GameObject child = gameObject.transform.GetChild(0).gameObject;

        Vector3 currentScale = child.transform.localScale;
        currentScale.x *= -1;
        child.transform.localScale = currentScale;
    }
}
