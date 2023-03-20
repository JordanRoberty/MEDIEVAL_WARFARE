using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.Assertions;
using static GameFoundationUtils;

public class PlayerInfosManager : Singleton<PlayerInfosManager>
{
    /*===== PUBLIC =====*/
    public InventoryItem equiped_weapon;

    /*===== PRIVATE =====*/
    private InventoryItem _equiped_weapon_tracker;

    private void Start()
    {
        load_equiped_weapon();
    }

    private void load_equiped_weapon()
    {
        // Get the "current weapon" item that contains the current weapon definition key
        const string definition_key = "equipedWeapon";
        InventoryItemDefinition definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(definition_key);
        List<InventoryItem> equiped_weapon_container = new List<InventoryItem>();
        GameFoundationSdk.inventory.FindItems(definition, equiped_weapon_container);

        Assert.IsTrue(equiped_weapon_container.Count == 1);

        _equiped_weapon_tracker = equiped_weapon_container[0];

        string equiped_weapon_id = _equiped_weapon_tracker.GetMutableProperty("equiped_weapon_id");

        set_equiped_weapon(equiped_weapon_id);
        set_current_runes();
    }

    private void set_equiped_weapon(string new_weapon_id)
    {
        if (new_weapon_id.Length == 0)
        {
            InventoryItem item = create_inventory_item("machineGunSword");
            

            new_weapon_id = item.id;
        }

        // Store current weapon id for future load
        _equiped_weapon_tracker.SetMutableProperty("equiped_weapon_id", new_weapon_id);
        equiped_weapon = GameFoundationSdk.inventory.FindItem(new_weapon_id);
    }

    private void set_current_runes()
    {
        Assert.IsNotNull(equiped_weapon);

        // Use the key you've used in the previous tutorial.
        const string definition_key = "speedRune";

        // Finding a definition takes a non-null string parameter.
        InventoryItemDefinition definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(definition_key);

        InventoryItem item = GameFoundationSdk.inventory.CreateItem(definition);

        equiped_weapon.SetMutableProperty("rune_key_0", item.id);
        Debug.Log(equiped_weapon.GetMutableProperty("rune_key_0"));
    }
}
