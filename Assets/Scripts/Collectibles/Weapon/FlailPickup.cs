using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class FlailPickup : MonoBehaviour
{
    private InventoryItemDefinition m_ShotgunFlailDefinition;

    void Start()
    {
        // Get the InventoryItemDefinition for the Shotgun Flail
        m_ShotgunFlailDefinition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>("shotgunFlail");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Player moves onto the item and doesn't already have it
        if (collision.CompareTag("Player"))
        {
            GameFoundationSdk.inventory.CreateItem(m_ShotgunFlailDefinition); // Add the item to the Player's inventory
            transform.parent.destroy();
        }
    }
}
