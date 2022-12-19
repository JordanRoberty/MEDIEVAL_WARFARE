using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    private List<Menu> menus_list = new List<Menu>();
    private Menu current_menu;

    protected override void Awake()
    {
        base.Awake();

        foreach (Transform menu in transform)
        {
            menus_list.Add(menu.GetComponent<Menu>());
            menu.gameObject.SetActive(false);
        }
        set_menu(Menu_Id.Title_Menu);
    }

    public void set_menu(Menu_Id new_menu_id)
    {
        if(current_menu != null)
        {
            current_menu.gameObject.SetActive(false);
        }

        Menu new_menu = menus_list.Find(menu => menu.id == new_menu_id);
        if(new_menu != null)
        {
            current_menu = new_menu;
            new_menu.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("The desired menu was not found");
        }
    }
}
