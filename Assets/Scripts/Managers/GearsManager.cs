using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using static GameObjectUtils;
using static GameFoundationUtils;

public class GearsManager : Singleton<GearsManager>
{
    /*===== PRIVATE UI =====*/
    [Header("GEARS MENU")]
    [Header("UI prefabs")]
    [SerializeField] private GameObject _weapon_viewer_prefab;
    [SerializeField] private GameObject _empty_weapon_viewer_prefab;
    [SerializeField] private GameObject _rune_viewer_prefab;
    [SerializeField] private GameObject _empty_rune_viewer_prefab;

    [Space(10)]

    [Header("Gears menu elements")]
    [SerializeField] private ItemView _equiped_weapon_viewer;
    [SerializeField] private Transform _equiped_runes_container;
    [SerializeField] private Transform _available_weapons_container;

    [Space(10)]

    [Header("RUNES MENU")]
    [Header("UI prefabs")]
    [SerializeField] private GameObject _available_rune_viewer_prefab;

    [Header("Runes menu elements")]
    [SerializeField] private GameObject _runes_menu;
    [SerializeField] private Transform _available_runes_container;

    /*===== PRIVATE =====*/
    private int _rune_to_exchange_slot;

    private void Start()
    {
        _rune_to_exchange_slot = -1;
        update_gear_menu();
    }

    public void update_gear_menu()
    {
        display_available_weapons();
        display_equiped_gear();
    }

    private void display_available_weapons()
    {
        List<InventoryItemDefinition> all_weapons = get_inventory_item_definitions_from_tag("WEAPON");
        List<InventoryItem> player_weapons = PlayerInfosManager.Instance.get_player_weapons();

        _available_weapons_container.destroy_children();

        for (int i = 0; i < all_weapons.Count; ++i)
        {
            if (i < player_weapons.Count)
            {
                AvailableWeaponViewer available_weapon_viewer = Instantiate(
                    _weapon_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _available_weapons_container
                ).GetComponent<AvailableWeaponViewer>();

                available_weapon_viewer.init(player_weapons[i]);
            }
            else
            {
                AvailableWeaponViewer empty_weapon_viewer = Instantiate(
                    _empty_weapon_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _available_weapons_container
                ).GetComponent<AvailableWeaponViewer>();

                empty_weapon_viewer.init(all_weapons[i]);
            }
        }
    }

    private void display_equiped_gear()
    {
        display_equiped_weapon();
        display_equiped_runes();
    }

    private void display_equiped_weapon()
    {
        // Get currently equiped weapon
        InventoryItem equiped_weapon = PlayerInfosManager.Instance.equiped_weapon;

        // Display currently equiped weapon in gear equiped weapon viewer
        display_item_in_viewer(equiped_weapon, _equiped_weapon_viewer);
    }

    public void display_equiped_runes()
    {
        int nb_rune_slots = PlayerInfosManager.Instance.equiped_weapon.GetMutableProperty("nb_rune_slots");
        List<InventoryItem> equiped_runes = PlayerInfosManager.Instance.get_equiped_runes();

        // Clear viewer
        _equiped_runes_container.destroy_children();

        // Display rune slots and equiped runes
        for (int rune_slot = 0; rune_slot < nb_rune_slots; rune_slot++)
        {
            if (equiped_runes[rune_slot] != null)
            {
                EquipedRuneViewer equiped_rune_viewer = Instantiate(
                    _rune_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _equiped_runes_container
                ).GetComponent<EquipedRuneViewer>();

                equiped_rune_viewer.init(equiped_runes[rune_slot], rune_slot);
            }
            else
            {
                EmptyRuneSlotViewer empty_rune_slot_viewer = Instantiate(
                    _empty_rune_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _equiped_runes_container
                ).GetComponent<EmptyRuneSlotViewer>();

                empty_rune_slot_viewer.init(rune_slot);
            }
        }
    }

    public void exchange_weapon(string new_weapon_id)
    {
        PlayerInfosManager.Instance.set_equiped_weapon(new_weapon_id);
        update_gear_menu();
    }

    public void start_rune_exchange(int rune_to_exchange_slot)
    {
        _rune_to_exchange_slot = rune_to_exchange_slot;
        List<InventoryItem> runes = get_inventory_items_from_tag("RUNE");

        // Display rune menu before displaying available runes
        // to be able to manipulate active GameObjects
        _runes_menu.SetActive(true);

        _available_runes_container.destroy_children();

        foreach (InventoryItem rune in runes)
        {
            if(rune.GetMutableProperty("equiped") == false)
            {
                // Create a new InventoryItem (rune here) viewer
                AvailableRuneViewer available_rune_viewer = Instantiate(
                    _available_rune_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _available_runes_container
                ).GetComponent<AvailableRuneViewer>();

                // Link the viewer to the InventoryItem it is displaying
                available_rune_viewer.init(rune);
            }
        }
    }

    public void cancel_exchange()
    {
        _rune_to_exchange_slot = -1;
        _runes_menu.SetActive(false);
    }

    public void exchange_rune(string new_rune_id)
    {
        PlayerInfosManager.Instance.exchange_equiped_rune(_rune_to_exchange_slot, new_rune_id);

        _rune_to_exchange_slot = -1;
        display_equiped_runes();

        _runes_menu.SetActive(false);
    }

    public void remove_rune(int rune_to_remove_slot)
    {
        PlayerInfosManager.Instance.remove_equiped_rune(rune_to_remove_slot);
        display_equiped_runes();
    }
}