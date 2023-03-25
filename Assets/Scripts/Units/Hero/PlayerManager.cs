using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : Singleton<PlayerManager>
{
    private Renderer _renderer;
    private RuneManager _rune_manager;

    [Header("Health & score")]
    public int shield = 0;
    public int max_health = 3;
    public int health = 3;
    public int score = 0;

    // Time during which the character is invincible after being hit (in seconds)
    public float invulnerability_time = 2.0f;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rune_manager = GetComponent<RuneManager>();
        health = max_health;
        score = 0;
        Debug.Log("Start !");
    }

    public void Init(int rune_health, int rune_shield)
    {
        // Update Player stats according to equiped runes
        max_health += rune_health;
        health = max_health;
        shield = rune_shield;
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

