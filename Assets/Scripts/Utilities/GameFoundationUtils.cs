using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.GameFoundation;
using UnityEngine.Assertions;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class GameFoundationUtils
{
    public static InventoryItem create_inventory_item(string definition_key)
    {
        InventoryItemDefinition definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(definition_key);
        InventoryItem item = GameFoundationSdk.inventory.CreateItem(definition);

        Assert.IsNotNull(item);

        return item;
    }
}

