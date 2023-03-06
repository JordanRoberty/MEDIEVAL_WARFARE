using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;

public class GearsManager : Singleton<GearsManager>
{
    [SerializeField] private ItemView _current_weapon_displayer;

    private InventoryItem _current_weapon;

    // Start is called before the first frame update
    void Start()
    {
        List<InventoryItem> current_weapon = new List<InventoryItem>();
        Tag tag = GameFoundationSdk.tags.Find("CURRENT_WEAPON");
        Debug.Log(tag);
        GameFoundationSdk.inventory.FindItems(tag, current_weapon);
        _current_weapon = current_weapon[0];

        display_current_weapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void display_current_weapon()
    {
        if(_current_weapon is null)
        {
            Debug.Log("Error: current item is null");
            return;
        }

        
        Debug.Log("item name: " + _current_weapon.GetMutableProperty("current_weapon_key"));
            
    }
}