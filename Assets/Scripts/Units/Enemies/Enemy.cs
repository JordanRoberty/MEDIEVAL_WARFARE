using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float pv;
    protected float speed;
    protected float damage;

    protected int max_droppable_quantity; // The maximum quantity of coins that can be dropped by an enemy
    public GameObject coinPrefab;

    public float get_damage()
    {
        return damage;
    }

    public void set_damage(float new_damage)
    {
        damage = new_damage;
    }

    public int get_max_droppable_quantity()
    {
        return max_droppable_quantity;
    }

    private void die()
    {
        // Spawn the coin at the enemy's position
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        // Pass the max_quantity value to the CoinPickup script and initialize the coin value
        CoinPickup coinPickup = coin.GetComponentInChildren<CoinPickup>();
        coinPickup.set_max_quantity(max_droppable_quantity);
        coinPickup.initialize_coin_value();

        // Destroy the enemy
        transform.destroy();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            //critical hit
            if (Random.Range(0, 100) < RuneManager.Instance.critial_hit_rune)
            {
                bullet.damage *= 2f;
            }

            if ((pv -= bullet.damage) <= 0)
            {
                die();
            }

            bullet.transform.destroy();
        }
    }
}
