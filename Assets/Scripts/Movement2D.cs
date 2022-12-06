using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 12f;
    [SerializeField] private float _groundLinearDrag = 10f; //a.k.a deceleration
    private float _horizontalDirection;
    private bool _changingDirection => (_rb.velocity.x > 0f && _horizontalDirection < 0f) || (_rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private int _extraJumps = 1;
    private int _extraJumpsValue;
    private bool _canJump => Input.GetButtonDown("Jump") && (_onGround || _extraJumpsValue > 0);
    [SerializeField] private float _fastfall_gravity = 500f; 
	[SerializeField] private float _fastfall_max_speed = 50.0f;

    [Header("Ground Collision Variable")]
    [SerializeField] private float _groundRaycastLength;
    [SerializeField] private Vector3 _groundRaycastOffset;
    private bool _onGround;
    //crouch variables
    private bool _is_crouching = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        _horizontalDirection = GetInput().x;
        // I don't know why I had to put the Jump() call here
        // But if we move it into FixedUpdate it's not responsive 
        if (_canJump) Jump();
    }

    private void FixedUpdate()
    {
        CheckGroundCollision();
        MoveCharacter();
        if (_onGround)
        {
            ApplyGroundLinearDrag();
            _extraJumpsValue = _extraJumps;
            crouch();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
            fast_fall();
        }

    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);

        if(Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
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


    private void ApplyGroundLinearDrag()
    {
        if(Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _groundLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

     private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }

    private void Jump()
    {
        if (!_onGround)
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

    private void FallMultiplier()
    {
        if(_rb.velocity.y < 0f)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if ( _rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }

       

    }

    private void fast_fall()
    {
        if(get_input().y < 0)
        {
            _rb.gravityScale = _fastfall_gravity;
            if(_rb.velocity.y > _fastfall_max_speed && !_is_crouching)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _fastfall_max_speed);
            }       
        }
    }

    private void CheckGroundCollision()
    {
        // To check the ground collision, we cast rays from the player to the ground. One in front of the player
        // And the other in his back to still detect the ground if the player is on the edge of the ground.
        _onGround = Physics2D.Raycast(transform.position + _groundRaycastOffset , Vector2.down, _groundRaycastLength, _groundLayer) ||
                    Physics2D.Raycast(transform.position - _groundRaycastOffset , Vector2.down, _groundRaycastLength, _groundLayer);
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
        Gizmos.DrawLine(transform.position + _groundRaycastOffset, transform.position + _groundRaycastOffset + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(transform.position - _groundRaycastOffset, transform.position - _groundRaycastOffset + Vector3.down * _groundRaycastLength);    
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
