using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    // Vie maximale du personnage
    public int maxHealth = 100;

    // Vie actuelle du personnage
    public float health = 100.0f;

    // Temps pendant lequel le personnage est invincible après avoir été touché (en secondes)
    public float invincibleTime = 2.0f;

    void Update()
    {
        // Si le personnage est actuellement invincible
        if (invincibleTime > 0)
        {
            // Décrémentation du temps d'invincibilité
            invincibleTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Si le collider en contact est l'ennemi
        if (collider.gameObject.tag == "Enemy")
        {
            // Si le personnage n'est pas actuellement invincible
            if (invincibleTime <= 0)
            {
                // Réduction de la vie du personnage et rendu invincible pendant 2 secondes
                health -= collider.gameObject.GetComponent<Enemy>().get_damage();
                invincibleTime = 2.0f;
            }

            // Vérification de la vie du personnage
            if (health <= 0)
            {
                // Affichage d'un message de game over ou relancement du niveau
                Debug.Log("Game Over");
            }
        }
    }
}
