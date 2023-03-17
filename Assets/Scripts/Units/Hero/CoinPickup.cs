using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class CoinPickup : MonoBehaviour
{
    private Currency m_CoinDefinition;
    
    public const int find_quantity = 1; // Quantity of coins to add when the Player moves onto a dropped Coin

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
            // Add the coin to the Player's wallet
            GameFoundationSdk.wallet.Add(m_CoinDefinition, find_quantity);

            // Destroy the Coin GameObject
            Destroy(transform.parent.gameObject);
        }
    }
}
