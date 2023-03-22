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

    public static List<InventoryItem> get_inventory_items_from_tag(string tag_key)
    {
        Tag tag = GameFoundationSdk.tags.Find(tag_key);
        Assert.IsNotNull(tag);
        List<InventoryItem> items = new List<InventoryItem>();
        
        GameFoundationSdk.inventory.FindItems(tag, items);

        return items;
    }

    public static List<InventoryItemDefinition> get_inventory_item_definitions_from_tag(string tag_key)
    {
        Tag tag = GameFoundationSdk.tags.Find(tag_key);
        Assert.IsNotNull(tag);
        List<InventoryItemDefinition> item_definitions = new List<InventoryItemDefinition>();

        GameFoundationSdk.catalog.FindItems(tag, item_definitions);

        return item_definitions;
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

