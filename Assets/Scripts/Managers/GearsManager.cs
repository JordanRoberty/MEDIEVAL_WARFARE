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
    [Header("UI prefabs")]
    [SerializeField]
    private GameObject rune_viewer_prefab;
    private GameObject empty_rune_viewer_prefab;

    [Header("Gears menu elements")]
    [SerializeField] private ItemView _equiped_weapon_viewer;
    [SerializeField] private Transform _equiped_runes_viewer;

    void Start()
    {
        display_equiped_gear();
    }

    void display_equiped_gear()
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
            ItemView rune_viewer = Instantiate(
                rune_viewer_prefab,
                Vector3.zero,
                Quaternion.identity,
                _equiped_runes_viewer
            ).GetComponent<ItemView>();

            if(rune_slot < equiped_runes.Count)
            {
                display_item_in_viewer(equiped_runes[rune_slot], rune_viewer);
            }
        }
    }
}