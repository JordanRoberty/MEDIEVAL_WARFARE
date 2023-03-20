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
    [SerializeField] private ItemView _current_weapon_viewer;

    void Start()
    {
        display_current_weapon();
    }

    void display_current_weapon()
    {
        InventoryItem current_weapon = PlayerInfosManager.Instance.current_weapon;

        Debug.Log("Current weapon : " + current_weapon.definition.displayName);

        AsyncOperationHandle<Sprite> load_sprite = current_weapon.definition.GetStaticProperty("item_icon").AsAddressable<Sprite>();
        load_sprite.Completed += (AsyncOperationHandle<Sprite> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = handle.Result;
                _current_weapon_viewer.SetItemView(sprite, current_weapon.definition.displayName, "");
            }
        };
    }
}