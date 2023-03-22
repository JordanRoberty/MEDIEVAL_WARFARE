using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : Singleton<RuneManager>
{
    [HideInInspector]
    public float damage_rune = 1f;
    public float speed_rune = 1f;
    public float firing_rate_rune = 1f; // 0 is shooting fast and 1 is normal shot speed
    public float health_rune = 2f;
    public float projectile_size_rune = 2f; 
    public float high_jump_rune = 1f;
    public float money_drop_rate_rune = 1f; // ??
    public float bullet_speed_rune = 1f;

    //  return a projectile ??
    //  player is smaller ??
    //  thornmail
    //  shield ??
    //  boucing shot ??
    //  repulse shot ??
    //  money magnet ??
    //  fastfall damage ??
    //  fly ???
    //  triple shot ???
    //  critial hit ???
    //  multiple shot ???

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
