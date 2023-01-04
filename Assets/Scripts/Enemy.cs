using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float pv;
    protected float speed;
    protected float damage;

    public float get_damage()
    {
        return damage;
    }

    public void set_damage(float new_damage)
    {
        damage = new_damage;
    }

    public void take_damage(int player_damage)
    {
        pv = pv - player_damage;

        if (pv <= 0)
        {
            Destroy(gameObject);
        }
    }

}
