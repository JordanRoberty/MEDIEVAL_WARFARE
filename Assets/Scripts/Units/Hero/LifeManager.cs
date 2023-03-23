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
        Debug.Log("Start !");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision !");
        Debug.Log(collision.gameObject.tag);
        // Si le collider en contact est l'ennemi
        if (collision.gameObject.tag == "Enemy")
        {
            
            if(player_data.shield == 0){
                player_data.health -= collision.gameObject.GetComponent<Enemy>().get_damage();

                if (player_data.health <= 0 && GameManager.Instance._state == GameState.RUNNING)
                {
                    GameManager.Instance.set_state(GameState.FAIL_MENU);
                    return;
                }

                // Invincibility for 2 seconds
                invincible.get_invulnerable();
            }
            else
            {
                player_data.shield--;
            }
        }
    }
}

