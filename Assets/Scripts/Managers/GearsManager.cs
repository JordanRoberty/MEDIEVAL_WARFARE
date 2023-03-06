using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GearsManager : Singleton<GearsManager>
{
    [SerializeField] private ItemView _current_weapon_view;

    private InventoryItem _current_weapon_definition_key;
    private InventoryItem _current_weapon;

    void Start()
    {
        // Get the "current weapon" item that contains the current weapon definition key 
        List<InventoryItem> current_weapon_list = new List<InventoryItem>();
        Tag tag = GameFoundationSdk.tags.Find("CURRENT_WEAPON");
        GameFoundationSdk.inventory.FindItems(tag, current_weapon_list);
        
        if(current_weapon_list.Count < 0)
        {
            return;
        }

        _current_weapon_definition_key = current_weapon_list[0];

        string definition_key = _current_weapon_definition_key.GetMutableProperty("current_weapon_key");

        set_current_weapon(definition_key);
        display_current_weapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void set_current_weapon(string current_weapon_definition_key)
    {
        if (current_weapon_definition_key is null)
        {
            Debug.Log("Error: current weapon definition is null");
            return;
        }

        _current_weapon_definition_key.SetMutableProperty("current_weapon_key", current_weapon_definition_key);
        
        InventoryItemDefinition current_weapon_definition = GameFoundationSdk.catalog.Find<InventoryItemDefinition>(current_weapon_definition_key);

        List<InventoryItem> current_weapon_list = new List<InventoryItem>();
        GameFoundationSdk.inventory.FindItems(current_weapon_definition, current_weapon_list);

        _current_weapon = current_weapon_list[0];
    }

    void display_current_weapon()
    {
        Debug.Log("Current weapon : " + _current_weapon.definition.displayName);

        AsyncOperationHandle<Sprite> load_sprite = _current_weapon.definition.GetStaticProperty("item_icon").AsAddressable<Sprite>();
        load_sprite.Completed += (AsyncOperationHandle<Sprite> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = handle.Result;
                _current_weapon_view.SetItemView(sprite, _current_weapon.definition.displayName, "");
            }
            
        };
    }
}