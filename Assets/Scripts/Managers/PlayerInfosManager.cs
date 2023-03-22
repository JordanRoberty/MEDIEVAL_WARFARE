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

    private void Start()
    {
        load_player_equipment();
    }

    private void load_player_equipment()
    {
        // Get the "current weapon" item that contains the current weapon definition key
        List<InventoryItem> weapons = get_inventory_items_from_tag("WEAPON");

        string equiped_weapon_id = "";
        if (weapons.Count > 0)
        {
            
            for (int i = 0; equiped_weapon_id.Length == 0; ++i)
            {
                if (weapons[i].GetMutableProperty("equiped") == true) equiped_weapon_id = weapons[i].id;
            }
        }
        else
        {
            InventoryItem item = create_new_inventory_item("machineGunSword");
            item.SetMutableProperty("equiped", true);
            equiped_weapon_id = item.id;

            DEBUG_add_runes_to_weapon(equiped_weapon_id);
        }

        Assert.IsFalse(equiped_weapon_id.Length == 0);

        set_equiped_weapon(equiped_weapon_id);
    }

    public void set_equiped_weapon(string new_weapon_id)
    {
        // Update previous equiped weapon
        if(equiped_weapon != null) equiped_weapon.SetMutableProperty("equiped", false);

        // Get new equiped weapon
        equiped_weapon = GameFoundationSdk.inventory.FindItem(new_weapon_id);
        
        // Update new equiped weapon
        equiped_weapon.SetMutableProperty("equiped", true);
    }

    public List<InventoryItem> get_player_weapons()
    {
        return get_inventory_items_from_tag("WEAPON");
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

    public void remove_equiped_rune(InventoryItemIdentifier rune_to_remove_id)
    {
        int nb_rune_slots = equiped_weapon.GetMutableProperty("nb_rune_slots");
        Assert.IsTrue(rune_to_remove_id.slot >= 0 && rune_to_remove_id.slot < nb_rune_slots);

        // Update the weapon slot
        equiped_weapon.SetMutableProperty("rune_id_" + rune_to_remove_id.slot, "");

        // Update the added rune
        InventoryItem rune_to_remove = GameFoundationSdk.inventory.FindItem(rune_to_remove_id.id);
        rune_to_remove.SetMutableProperty("equiped", false);
    }

    public void exchange_equiped_rune(InventoryItemIdentifier rune_to_exchange_id, InventoryItemIdentifier rune_to_exchange_with_id)
    {
        int nb_rune_slots = equiped_weapon.GetMutableProperty("nb_rune_slots");
        Assert.IsTrue(rune_to_exchange_id.slot >= 0 && rune_to_exchange_id.slot < nb_rune_slots);

        // Update the weapon slot
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

    private void DEBUG_add_runes_to_weapon(string new_weapon_id)
    {
        InventoryItem weapon = GameFoundationSdk.inventory.FindItem(new_weapon_id);
        create_new_inventory_item("shotgunFlail");

        // Create three new runes
        InventoryItem rune_0 = create_new_inventory_item("commonSpeedRune");
        InventoryItem rune_1 = create_new_inventory_item("commonSpeedRune");
        InventoryItem rune_2 = create_new_inventory_item("commonSpeedRune");

        weapon.SetMutableProperty("rune_id_0", rune_0.id);
        rune_0.SetMutableProperty("equiped", true);

        weapon.SetMutableProperty("rune_id_1", rune_1.id);
        rune_1.SetMutableProperty("equiped", true);

        Debug.Log(
            $"Equiped weapon : {weapon.definition.displayName}"
            + "\n-Rune 0 : " + weapon.GetMutableProperty("rune_id_0")
            + "\n-Rune 1 : " + weapon.GetMutableProperty("rune_id_1")
            + "\n-Rune 2 : " + weapon.GetMutableProperty("rune_id_2")
        );
    }
}
