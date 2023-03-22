using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.GameFoundation;
using UnityEngine.Assertions;
using UnityEngine.GameFoundation.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// A static class for GameFoundation helpful methods
/// </summary>
public static class GameFoundationUtils
{
    public static InventoryItem create_new_inventory_item(string definition_key)
    {
        InventoryItemDefinition definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(definition_key);
        InventoryItem item = GameFoundationSdk.inventory.CreateItem(definition);

        Assert.IsNotNull(item);

        return item;
    }

    public static void display_item_icon_in_viewer(InventoryItem item, ItemView viewer)
    {
        AsyncOperationHandle<Sprite> load_sprite = item.definition.GetStaticProperty("item_icon").AsAddressable<Sprite>();
        load_sprite.Completed += (AsyncOperationHandle<Sprite> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = handle.Result;
                Assert.IsNotNull(sprite);
                viewer.SetIcon(sprite);
            }
        };
    }

    public static void display_item_in_viewer(InventoryItem item, ItemView viewer)
    {
        viewer.SetDisplayName(item.definition.displayName);
        display_item_icon_in_viewer(item, viewer);
        viewer.SetDescription("");
    }
}

