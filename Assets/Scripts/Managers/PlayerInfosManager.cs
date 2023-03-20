using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerInfosManager : Singleton<PlayerInfosManager>
{
    /*===== PUBLIC =====*/
    public InventoryItem current_weapon;

    /*===== PRIVATE =====*/
    private InventoryItem _current_weapon_definition_key;

    private void Start()
    {
        load_current_weapon();
    }

    private void load_current_weapon()
    {
        // Get the "current weapon" item that contains the current weapon definition key 
        List<InventoryItem> current_weapon_list = new List<InventoryItem>();
        Tag tag = GameFoundationSdk.tags.Find("CURRENT_WEAPON");
        GameFoundationSdk.inventory.FindItems(tag, current_weapon_list);

        if (current_weapon_list.Count < 0)
        {
            return;
        }

        _current_weapon_definition_key = current_weapon_list[0];

        string definition_key = _current_weapon_definition_key.GetMutableProperty("current_weapon_key");

        set_current_weapon(definition_key);
        set_current_runes();
    }

    private void set_current_weapon(string current_weapon_definition_key)
    {
        if (current_weapon_definition_key is null)
        {
            Debug.Log("Error: current weapon definition is null");
            return;
        }

        // Store current weapon key for future load
        _current_weapon_definition_key.SetMutableProperty("current_weapon_key", current_weapon_definition_key);
        InventoryItemDefinition current_weapon_definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(current_weapon_definition_key);

        List<InventoryItem> current_weapon_list = new List<InventoryItem>();
        GameFoundationSdk.inventory.FindItems(current_weapon_definition, current_weapon_list);

        current_weapon = current_weapon_list[0];
    }

    private void set_current_runes()
    {
        // Use the key you've used in the previous tutorial.
        const string definition_key = "speedRune";

        // Finding a definition takes a non-null string parameter.
        InventoryItemDefinition definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(definition_key);

        Debug.Log(definition);

        InventoryItem item = GameFoundationSdk.inventory.CreateItem(definition);

        current_weapon.SetMutableProperty("rune_key_0", item.id);
        Debug.Log(current_weapon.GetMutableProperty("rune_key_0"));
    }
}
