using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private Renderer _renderer;
    private RuneManager _rune_manager;

    [Header("Health & score")]
    public float shield = 0f;
    public float max_health = 500f;
    public float health = 100f;
    // Temps pendant lequel le personnage est invincible apr�s avoir �t� touch� (en secondes)
    public float invulnerability_time = 2.0f;
    
    public int score = 0;
    public int nb_coins = 0;
    [SerializeField] private TextMeshProUGUI score_text;
    [SerializeField] private TextMeshProUGUI health_text;
    [SerializeField] private TextMeshProUGUI coins_text;
    

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _rune_manager = GetComponent<RuneManager>();
        Debug.Log("Start !");
        score_text.SetText("SCORE : " + score);
        health_text.SetText("HEALTH : " + health);
        coins_text.SetText("MONEY : " + nb_coins);
    }

    void Update()
    {
        score++;
        score_text.SetText("SCORE : " + score);
    }

    public void Init(float rune_health, float rune_shield)
    {
        //RUNES MODIFIER
        max_health *= rune_health;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le collider en contact est l'ennemi
        if (collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if(shield == 0){
                health -= enemy.get_damage();
                health_text.SetText("HEALTH : " + health);
                if (health <= 0 && GameManager.Instance._state == GameState.RUNNING)
                {
                    StopCoroutine("invulnerability");
                    health_text.SetText("HEALTH : " + 0);
                    GameManager.Instance.set_state(GameState.FAIL_MENU);
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
    }
}

