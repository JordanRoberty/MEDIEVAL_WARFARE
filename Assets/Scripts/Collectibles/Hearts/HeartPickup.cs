using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class HeartPickup : MonoBehaviour
{
    //private Currency m_CoinDefinition;
    public int heart_value = 1; // The value of the heart

    void Start()
    {
        // Get the Life data definition for the Heart
        // TODO
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Player moves onto the Coin
        if (collision.CompareTag("Player"))
        {
            //GameFoundationSdk.wallet.Add(m_CoinDefinition, heart_value); // Add the heart to the Player's life
            transform.parent.destroy();
        }
    }
}
