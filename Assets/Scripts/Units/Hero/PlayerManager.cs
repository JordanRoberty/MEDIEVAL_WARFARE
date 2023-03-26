using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Movement2D _player_controller;
    [SerializeField] private WeaponManager _weapon_controller;
    [SerializeField] private Renderer _renderer;


    [Header("Health & score")]
    public int shield = 0;
    public int max_health = 3;
    public int health = 3;
    
    // Time during which the character is invincible after being hit (in seconds)
    public float invulnerability_time = 2.0f;

    void Awake()
    {
        _player_controller = GetComponent<Movement2D>();
        _weapon_controller = GetComponentInChildren<WeaponManager>();
        _renderer = GetComponent<Renderer>();
    }

    public void init(Camera main_cam)
    {
        // Update Player stats according to equiped runes
        max_health += RuneManager.Instance.health_rune;
        health = max_health;
        shield = RuneManager.Instance.shield_rune;

        _player_controller.init(main_cam);
        _weapon_controller.init(main_cam);
    }

    //The layer 8 is the player, the 9th is the enemy
    IEnumerator invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Color player_sprite_color = _renderer.material.color;
        player_sprite_color.a = 0.5F;
        _renderer.material.color = player_sprite_color;

        yield return new WaitForSeconds(invulnerability_time);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        player_sprite_color.a = 1.0F;
        _renderer.material.color = player_sprite_color;
    }

    public bool is_dead()
    {
        return health <= 0;
    }

    public void die()
    {
        health = 0;
        Debug.Log("Player died");
        StopCoroutine("invulnerability");

        GameManager.Instance.set_state(GameState.FAIL_MENU);
        
    }

    public void take_damages(int damages)
    {
        if (shield == 0)
        {
            health = Mathf.Clamp(health - damages, 0, max_health);
            //health_text.SetText("HEALTH : " + health);

            if(is_dead() && GameManager.Instance._state == GameState.RUNNING)
            {
                die();
            }
            else
            {
                // Invincibility for 2 seconds
                StartCoroutine("invulnerability");
            }
        }
        else
        {
            shield--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if the collision is with an enemy
        if (collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            take_damages(enemy.get_damage());
        }
    }
}

