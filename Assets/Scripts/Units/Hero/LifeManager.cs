using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    PlayerData player_data;
    // Start is called before the first frame update
    void Start()
    {
        player_data = GetComponent<PlayerData>();
    }

    void Update()
    {
        // Si le personnage est actuellement invincible
        if (player_data.invincibleTime > 0)
        {
            // Décrémentation du temps d'invincibilité
            player_data.invincibleTime -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le collider en contact est l'ennemi
        if (collision.gameObject.tag == "Enemy")
        {
            // Si le personnage n'est pas actuellement invincible
            if (player_data.invincibleTime <= 0.0f)
            {
                // Réduction de la vie du personnage et rendu invincible pendant 2 secondes
                player_data.health -= collision.gameObject.GetComponent<Enemy>().get_damage();
                player_data.invincibleTime = 2.0f;
            }

            // Vérification de la vie du personnage
            if (player_data.health <= 0)
            {
                // Affichage d'un message de game over ou relancement du niveau
                Debug.Log("Game Over");
                Destroy(gameObject);
            }
        }
    }
}
