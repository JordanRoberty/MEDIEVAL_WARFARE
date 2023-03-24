using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class CoinPickup : MonoBehaviour
{
    private Currency m_CoinDefinition;
    public int find_quantity; // Quantity of coins to add when the Player moves onto a dropped Coin
    private int max_quantity; // The maximum quantity of coins that can be dropped by a specific type of enemy

    public void set_max_quantity(int value)
    {
        max_quantity = value;
    }

    public void initialize_coin_value()
    {
        // Calculate the parameters of the normal distribution
        float mean = max_quantity / 2f;
        float standard_deviation = max_quantity / 4f;

        // Generate a random number following a normal distribution
        float random_value = RandomFromDistribution.random_normal_distribution(mean, standard_deviation);

        // The coin value is a number following a normal distribution between 0 and the enemy's max droppable quantity
        find_quantity = Mathf.FloorToInt(Mathf.Clamp(random_value, 0, max_quantity));
    }

    void Start()
    {
        // Get the Currency definition for the Coin
        m_CoinDefinition = GameFoundationSdk.catalog.Find<Currency>("goldCoin");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the the Player moves onto the Coin
        if (collision.CompareTag("Player"))
        {
            GameFoundationSdk.wallet.Add(m_CoinDefinition, find_quantity); // Add the coin to the Player's wallet
            Destroy(transform.parent.gameObject); // Destroy the Coin GameObject
        }
    }
}
