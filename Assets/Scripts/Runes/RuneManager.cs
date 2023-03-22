using System.Collections;
using System.Collections.Generic;
using UnityEngine.GameFoundation;
using UnityEngine;

public class RuneManager : Singleton<RuneManager>
{
    public float damage_rune;
    public float speed_rune;
    public float firing_rate_rune; // 0 is shooting fast and 1 is normal shot speed
    public float health_rune;
    public float projectile_size_rune; 
    public float high_jump_rune;
    public float money_drop_rate_rune; // ??
    public float bullet_speed_rune;

    private List<InventoryItem> _equiped_runes = new List<InventoryItem>();

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
        damage_rune = 1f;
        speed_rune = 1f;
        firing_rate_rune = 1f; // 0 is shooting fast and 1 is normal shot speed
        health_rune = 1f;
        projectile_size_rune = 1f; 
        high_jump_rune = 1f;
        money_drop_rate_rune = 1f; // ??
        bullet_speed_rune = 1f;
        _equiped_runes = PlayerInfosManager.Instance.get_equiped_runes();
        getRuneModifier();

        transform.GetComponent<Movement2D>().Init();

    }

    private void getRuneModifier()
    {
        foreach(InventoryItem rune in _equiped_runes){
            if(rune != null){
                switch (rune.definition.key)
                {
                    case "damageRune":
                    // damage_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "commonSpeedRune":
                        speed_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "firingRateRune":
                        //firing_rate_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "healthRune":
                    // health_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "projectileSizeRune":
                    // projectile_size_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "highJumpRune":
                    // high_jump_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "moneyDropRateRune":
                    // money_drop_rate_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "bulletSpeedRune":
                    // bullet_speed_rune = rune.GetMutableProperty("modifier");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
