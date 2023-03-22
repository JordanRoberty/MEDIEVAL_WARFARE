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
    [Header("UI prefabs")]
    [SerializeField] private GameObject rune_viewer_prefab;
    [SerializeField] private GameObject empty_rune_viewer_prefab;

    [Header("Gears menu elements")]
    [SerializeField] private ItemView _equiped_weapon_viewer;
    [SerializeField] private Transform _equiped_runes_viewer;

    /*===== PRIVATE =====*/
    private InventoryItem rune_to_change;

    private void Start()
    {
        display_equiped_gear();
    }

    private void display_equiped_gear()
    {
        // DISPLAY WEAPON
        InventoryItem equiped_weapon = PlayerInfosManager.Instance.equiped_weapon;

        display_item_in_viewer(equiped_weapon, _equiped_weapon_viewer);

        // DISPLAY RUNES
        int nb_rune_slots = equiped_weapon.GetMutableProperty("nb_rune_slots");
        List <InventoryItem> equiped_runes = PlayerInfosManager.Instance.equiped_runes;

        // Clear viewer
        _equiped_runes_viewer.destroy_children();

        // Display rune slots and equiped runes
        for (int rune_slot=0; rune_slot < nb_rune_slots; rune_slot++)
        {
            if(equiped_runes[rune_slot] != null)
            {
                ItemView rune_viewer = Instantiate(
                    rune_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _equiped_runes_viewer
                ).GetComponent<ItemView>();

                display_item_in_viewer(equiped_runes[rune_slot], rune_viewer);
            }
            else
            {
                Instantiate(
                    empty_rune_viewer_prefab,
                    Vector3.zero,
                    Quaternion.identity,
                    _equiped_runes_viewer
                );
            }
        }
    }

    public void open_runes_menu()
    {

    }
}