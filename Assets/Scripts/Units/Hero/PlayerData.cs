using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [HideInInspector]public Rigidbody2D rb;

    [Header("Health & score")]
    public int health = 100;
    public int score;
    public int nb_coins;
    //RUNES ??

    [Header("Weapon")]
    //WEAPON???

    [Header("Movement Variables")]
    public float movement_acceleration = 50f;
    public float max_move_speed = 12f;
    public float ground_linear_drag = 10f; //a.k.a deceleration
    [HideInInspector]public bool changing_direction;

    [Header("jump Variables")]
    public float jump_force = 12f;
    public float air_linear_drag = 2.5f;
    public float fall_gravity = 8f;
    public float low_jump_fall_gravity = 5f;
    public int extra_jumps = 1;
    public float fastfall_gravity = 500f; 
	public float fastfall_max_speed = 50.0f;
    public int extra_jumps_count;
    [HideInInspector]public bool can_jump;

    [Header("Ground Collision Variable")]
    public float ground_raycast_length;
    public Vector3 ground_raycast_offset;
    [HideInInspector]public bool on_ground;

    [Header("Crouch Variable")]
    [HideInInspector]public bool is_crouching = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
