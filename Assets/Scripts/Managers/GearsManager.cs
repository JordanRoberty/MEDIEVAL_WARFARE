using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using static GameFoundationUtils;

public class GearsManager : Singleton<GearsManager>
{
    [SerializeField] private ItemView _equiped_weapon_viewer;

    void Start()
    {
        display_equiped_weapon();
        //display_equiped_runes();
    }

    void display_equiped_weapon()
    {
        InventoryItem equiped_weapon = PlayerInfosManager.Instance.equiped_weapon;

        Debug.Log("Current weapon : " + equiped_weapon.definition.displayName);

        _equiped_weapon_viewer.SetDisplayName(equiped_weapon.definition.displayName);
        display_item_icon_in_viewer(equiped_weapon, _equiped_weapon_viewer);
        _equiped_weapon_viewer.SetDescription("");
    }
}