using System.Collections;
using System.Collections.Generic;
using UnityEngine.GameFoundation;
using UnityEngine;

public class RuneManager : Singleton<RuneManager>
{
    //Stats
    [HideInInspector]
    public float damage_rune;// { get; private set; }
    [HideInInspector]
    public float speed_rune;// { get; private set; }
    [HideInInspector]
    public float firing_rate_rune;// {get; private set;} // 0 is shooting fast and 1 is normal shot speed
    [HideInInspector]
    public int health_rune;//{get; private set;}
    [HideInInspector]
    public float projectile_size_rune;//{get; private set;} 
    [HideInInspector]
    public float high_jump_rune;//{get; private set;}
    [HideInInspector]
    public float money_drop_rate_rune;//{get; private set;} // ??
    [HideInInspector]
    public float bullet_speed_rune;//{get; private set;}

    //Functionnality
    [HideInInspector]
    public int shield_rune;// {get; private set;}
    [HideInInspector]
    public int critial_hit_rune;// {get; private set;}
    [HideInInspector]
    public bool triple_jump_rune;// {get; private set;}

    private List<InventoryItem> _equiped_runes = new List<InventoryItem>();

    //  thornmail
    //  repulse shot ??
    //  money magnet ??
    //  multiple shot ???

    // Start is called before the first frame update
    void Start()
    {
        damage_rune = 1f;
        speed_rune = 1f;
        firing_rate_rune = 1f; // 0 is shooting fast and 1 is normal shot speed
        health_rune = 0;
        projectile_size_rune = 1f; 
        high_jump_rune = 1f;
        money_drop_rate_rune = 1f; // ??
        bullet_speed_rune = 1f;
        shield_rune = 0;
        critial_hit_rune = 0;
        triple_jump_rune = false;
        _equiped_runes = PlayerInfosManager.Instance.get_equiped_runes();
        getRuneModifier();

        transform.GetComponent<Movement2D>().Init();
        transform.GetComponent<PlayerManager>().Init(health_rune, shield_rune);

    }

    private void getRuneModifier()
    {
        foreach(InventoryItem rune in _equiped_runes){
            if(rune != null){
                switch (rune.definition.key)
                {
                    case "commonDamageRune" or "rareDamageRune" or "legendaryDamageRune":
                        damage_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "commonSpeedRune" or "rareSpeedRune" or "legendarySpeedRune":
                        speed_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "commonFiringRateRune": // or "rareFiringRateRune" or "legendaryFiringRateRune"
                        firing_rate_rune = rune.GetMutableProperty("modifier");
                        break;
                    case "commonHealthRune": // or "rareHealthRune" or "legendaryHealthRune"
                        health_rune += rune.GetMutableProperty("modifier");
                        break;
                    case "commonProjectileSizeRune": // or "rareProjectileSizeRune" or "legendaryProjectileSizeRune"
                        projectile_size_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "commonHighJumpRune": // or "rareHighJumpRune" or "legendaryHighJumpRune
                        high_jump_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "commonMoneyDropRateRune": // or "rareMoneyDropRateRune" or "legendaryMoneyDropRateRune
                        money_drop_rate_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "commonBulletSpeedRune": // or "rareBulletSpeedRune" or "legendaryBulletSpeedRune
                        bullet_speed_rune *= rune.GetMutableProperty("modifier");
                        break;
                    case "commonShieldRune": // or "rareShieldRune" or "legendaryShieldRune
                        shield_rune += rune.GetMutableProperty("modifier");
                        break;
                    case "commonCriticalHitRune": // or "rareCriticalHitRune" or "legendaryCriticalHitRune
                        critial_hit_rune += rune.GetMutableProperty("modifier");
                        break;
                    case "commonTripleJumpRune": // or "rareTripleJumpRune" or "legendaryTripleJumpRune
                        triple_jump_rune = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
