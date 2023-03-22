using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.Assertions;
using static GameFoundationUtils;

public class PlayerInfosManager : Singleton<PlayerInfosManager>
{
    /*===== PUBLIC =====*/
    public InventoryItem equiped_weapon { get; private set; }

    /*===== PRIVATE =====*/
    private InventoryItem _equiped_weapon_tracker;

    private void Start()
    {
        load_player_equipment();
    }

    private void load_player_equipment()
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
    }

    private void set_equiped_weapon(string new_weapon_id)
    {
        if (new_weapon_id.Length == 0)
        {
            InventoryItem item = create_new_inventory_item("machineGunSword");
            new_weapon_id = item.id;
        }

        // Store current weapon id for future load
        _equiped_weapon_tracker.SetMutableProperty("equiped_weapon_id", new_weapon_id);
        equiped_weapon = GameFoundationSdk.inventory.FindItem(new_weapon_id);

        DEBUG_add_runes_to_weapon();
    }

    public List<InventoryItem> get_equiped_runes()
    {
        Assert.IsNotNull(equiped_weapon);

        int nb_runes_slots = equiped_weapon.GetMutableProperty("nb_rune_slots");
        List<InventoryItem> equiped_runes = new List<InventoryItem>();

        Assert.IsTrue(nb_runes_slots > 0);

        // Get all the equiped rune objects
        for(int rune_slot = 0; rune_slot < nb_runes_slots; rune_slot++)
        {
            string rune_id = equiped_weapon.GetMutableProperty("rune_id_" + rune_slot);
            if (rune_id.Length != 0)
            {
                equiped_runes.Add(GameFoundationSdk.inventory.FindItem(rune_id));
            }
            else
            {
                equiped_runes.Add(null);
            }
        }

        return equiped_runes;
    }

    public void exchange_equiped_rune(InventoryItemIdentifier rune_to_exchange_id, InventoryItemIdentifier rune_to_exchange_with_id)
    {
        int nb_rune_slots = equiped_weapon.GetMutableProperty("nb_rune_slots");
        Assert.IsTrue(rune_to_exchange_id.slot >= 0 && rune_to_exchange_id.slot < nb_rune_slots);

        // Update the weapon slot
        Debug.Log("rune_id_" + rune_to_exchange_id.slot);
        equiped_weapon.SetMutableProperty("rune_id_" + rune_to_exchange_id.slot, rune_to_exchange_with_id.id);

        // Update the removed rune only if the slot wasn't empty
        if (rune_to_exchange_id.id.Length != 0)
        {
            InventoryItem rune_to_exchange = GameFoundationSdk.inventory.FindItem(rune_to_exchange_id.id);
            rune_to_exchange.SetMutableProperty("equiped", false);
        }

        // Update the added rune
        InventoryItem rune_to_exchange_with = GameFoundationSdk.inventory.FindItem(rune_to_exchange_with_id.id);
        rune_to_exchange_with.SetMutableProperty("equiped", true);
    }

    private void DEBUG_add_runes_to_weapon()
    {
        // Create two new runes
        InventoryItem rune_0 = create_new_inventory_item("commonSpeedRune");
        InventoryItem rune_1 = create_new_inventory_item("commonSpeedRune");
        InventoryItem rune_2 = create_new_inventory_item("commonSpeedRune");

        equiped_weapon.SetMutableProperty("rune_id_0", rune_0.id);
        rune_0.SetMutableProperty("equiped", true);

        equiped_weapon.SetMutableProperty("rune_id_1", rune_1.id);
        rune_1.SetMutableProperty("equiped", true);

        Debug.Log(
            $"Equiped weapon : {equiped_weapon.definition.displayName}"
            + "\n-Rune 0 : " + equiped_weapon.GetMutableProperty("rune_id_0")
            + "\n-Rune 1 : " + equiped_weapon.GetMutableProperty("rune_id_1")
            + "\n-Rune 2 : " + equiped_weapon.GetMutableProperty("rune_id_2")
        );
    }
}
