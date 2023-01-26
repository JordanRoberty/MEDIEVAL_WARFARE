using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    PlayerData player_data;
    Invincibility invincible;
    // Start is called before the first frame update
    void Start()
    {
        player_data = GetComponent<PlayerData>();
        invincible = GetComponent<Invincibility>();
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le collider en contact est l'ennemi
        if (collision.gameObject.tag == "Enemy")
        {
            // Réduction de la vie du personnage et rendu invincible pendant 2 secondes
            player_data.health -= collision.gameObject.GetComponent<Enemy>().get_damage();
            invincible.get_invulnerable();
        }
    }
}

