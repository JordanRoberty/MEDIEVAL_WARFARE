using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class MenuManager : Singleton<MenuManager>
{
    [SerializeField]
    protected Transform root_canvas;
    [SerializeField]
    private Menu initial_menu;

    private Menu current_menu;

    protected override void Awake()
    {
        base.Awake();

        /* Hide by default all the menus */
        foreach (Transform menu in root_canvas)
        {
            menu.gameObject.SetActive(false); ;
        }
    }

    public abstract void set_state(ushort new_state);

    public void push_menu(Menu new_menu)
    {
        new_menu.enter();

        if (current_menu != null)
        {
            if (!new_menu.is_modal)
            {
                current_menu.exit();
                current_menu.gameObject.SetActive(false);
            }
        }

        current_menu = new_menu;
        current_menu.gameObject.SetActive(true);
        if(current_menu.first_button_selected != null)
        {
            EventSystem.current.SetSelectedGameObject(current_menu.first_button_selected);
        }
    }
}
