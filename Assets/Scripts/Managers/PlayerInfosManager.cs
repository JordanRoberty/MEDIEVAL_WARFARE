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
    public List<InventoryItem> equiped_runes { get; private set; }

    /*===== PRIVATE =====*/
    private InventoryItem _equiped_weapon_tracker;

    private void Start()
    {
        equiped_runes = new List<InventoryItem>();
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

        // Update currently equiped runes
        set_equiped_runes();
    }

    private void set_equiped_runes()
    {
        Assert.IsNotNull(equiped_weapon);

        equiped_runes.Clear();
        int nb_runes_slots = equiped_weapon.GetMutableProperty("nb_rune_slots");

        Assert.IsTrue(nb_runes_slots > 0);

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

        Debug.Log(equiped_runes.Count);
    }

    public void exchange_equiped_rune(InventoryItemIdentifier rune_to_exchange, InventoryItemIdentifier rune_to_exchange_with)
    {

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
