using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MenuController
{
    private Dictionary<MenuState, Menu> menus = new Dictionary<MenuState, Menu>();

    protected override void Awake()
    {
        base.Awake();

        foreach (Transform menu in root_canvas)
        {
            menus.Add(menu.GetComponent<Menu>().id, menu.GetComponent<Menu>());
        }
    }

    
}
