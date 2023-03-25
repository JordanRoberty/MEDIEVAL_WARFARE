using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayHealth : MonoBehaviour
{
   private int health;
    private int numOfHeart;
    private int shield;
    private int maximum_health;

    public GameObject player;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite shieldHeart;

    public void Awake()
    {
        health = player.GetComponent<PlayerManager>().health;
        numOfHeart = player.GetComponent<PlayerManager>().max_health;
        shield = player.GetComponent<PlayerManager>().shield;

    }

    void Update()
    {

        health = player.GetComponent<PlayerManager>().health;
        shield = player.GetComponent<PlayerManager>().shield;
        numOfHeart = player.GetComponent<PlayerManager>().max_health + shield;

        if (health > numOfHeart)
        {
            health = numOfHeart;
        }

        int current_life = health + shield;

        for (int i = 0; i < hearts.Length ; i++)
        {

            //set the sprite of the heart
            if (i < health )
            {
                hearts[i].sprite = fullHeart;

            }
            else
            {
                if(shield > 0){
                    hearts[i].sprite = shieldHeart;
                    shield--;
                }else
                {
                    Debug.Log("shield : " + shield);
                    hearts[i].sprite = emptyHeart;
                }       
            }

            //set the heart active or not
            if (i < numOfHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
