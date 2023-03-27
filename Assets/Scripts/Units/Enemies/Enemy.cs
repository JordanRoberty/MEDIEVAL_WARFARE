using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float pv;
    protected float speed;
    protected int damage = 1;
    protected int score_value = 1;

    protected int max_droppable_quantity; // The maximum quantity of coins that can be dropped by an enemy
    protected int coin_quantity; // The quantity of coins that will actually be dropped by an enemy
    public GameObject coinPrefab;

    public int get_damage()
    {
        return damage;
    }

    public void set_damage(int new_damage)
    {
        damage = new_damage;
    }

    public float getpv()
    {
        return pv;
    }

    public void setpv(float new_pv)
    {
        pv = new_pv;
    }

    private void initialize_coin_quantity()
    {
        // Calculate the parameters of the normal distribution
        float mean = max_droppable_quantity / 2f;
        float standard_deviation = max_droppable_quantity / 4f;

        // Generate a random number following a normal distribution
        float random_value = RandomFromDistribution.random_normal_distribution(mean, standard_deviation);

        // The coin quantity is a number following a normal distribution between 0 and the enemy's max droppable quantity
        coin_quantity = Mathf.FloorToInt(Mathf.Clamp(random_value, 0, max_droppable_quantity));
    }

    public virtual void die()
    {
        StatsManager.Instance.update_score(score_value);
        
        initialize_coin_quantity();
        
        // Spawn the correct number of coin(s) at the enemy's position
        for (int i = 0; i < coin_quantity; i++)
        {
            Transform coin_parent = GameObject.Find("/Environment/Coins").transform;

            // Add a random offset to the coin's position
            GameObject coin = Instantiate(
                coinPrefab,
                transform.position + new Vector3(Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f), 0),
                Quaternion.identity,
                coin_parent
            );
        }

        // Destroy the enemy
        transform.destroy();
    }
}
