using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [HideInInspector]public Rigidbody2D rb;

    [Header("Health & score")]
    public float shield = 0f;
    public float max_health = 500f;
    public float health = 100f;
     // Temps pendant lequel le personnage est invincible après avoir été touché (en secondes)
    public float invincibleTime = 2.0f;
    public int score;
    public int nb_coins;

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();

        //RUNES MODIFIER
        max_health *= transform.GetComponent<RuneManager>().health_rune;
        health = max_health;  
        shield = transform.GetComponent<RuneManager>().shield_rune;
    }
}
