using System;
using System.Collections;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DefaultLayers;
using UnityEngine.Promise;

public class GFInit : MonoBehaviour
{
    IEnumerator Start()
    {
        // Creates a new data layer for Game Foundation,
        // with the default parameters.
        MemoryDataLayer dataLayer = new MemoryDataLayer();

        // - Initializes Game Foundation with the data layer.
        // - We use a using block to automatically release the deferred promise handler.
        using (Deferred initDeferred = GameFoundationSdk.Initialize(dataLayer))
        {
            yield return initDeferred.Wait();

            if (initDeferred.isFulfilled)
                OnInitSucceeded();
            else
                OnInitFailed(initDeferred.error);
        }
    }

    // Called when Game Foundation is successfully initialized.
    void OnInitSucceeded()
    {
        Debug.Log("Game Foundation is successfully initialized");

        const string definitionKey = "onehandSword";

        // Finding a definition takes a non-null string parameter.
        InventoryItemDefinition definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(definitionKey);

        // Make sure you retrieved a valid definition.
        if (definition is null)
        {
            Debug.Log($"Definition {definitionKey} not found");
            return;
        }

        // You should be able to get information from your definition now.
        Debug.Log($"Definition {definition.key} '{definition.displayName}' found.");

        InventoryItem item = GameFoundationSdk.inventory.CreateItem(definition);

        Debug.Log($"Item {item.id} of definition '{item.definition.key}' created");
    }

    // Called if Game Foundation initialization fails 
    void OnInitFailed(Exception error)
    {
        Debug.LogException(error);
    }
}