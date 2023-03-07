using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float health = 10f;
    // Update is called once per frame
    public void take_damages(float damage)
    {
        health = health - (damage * Runes.damage_rune);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
