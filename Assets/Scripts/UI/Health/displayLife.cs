using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayLife : MonoBehaviour
{
    private int health;
    private int numOfHeart;

    public GameObject player;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        health = player.GetComponent<PlayerManager>().health;
        numOfHeart = player.GetComponent<PlayerManager>().max_health;
    }

    void Update()
    {

        health = player.GetComponent<PlayerManager>().health;

        if (health > numOfHeart)
        {
            health = numOfHeart;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

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
