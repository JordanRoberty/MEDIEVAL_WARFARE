using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [HideInInspector]public Rigidbody2D rb;

    [Header("Health & score")]
    public float max_health = 100f;
    public float health = 100f;
     // Temps pendant lequel le personnage est invincible après avoir été touché (en secondes)
    public float invincibleTime = 2.0f;
    public int score;
    public int nb_coins;
    //RUNES ??

    //[Header("Weapon")]
    //WEAPON???

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //Destroy(gameObject);
            Debug.Log("DEAD 2");
            GameManager.Instance.set_player_status(false);
        }
    }
}
