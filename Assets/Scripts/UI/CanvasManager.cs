using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CanvasManager : Singleton<CanvasManager>
{
    private Dictionary<MenuState, Menu> _menus = new Dictionary<MenuState, Menu>();
    private Menu _current_menu;

    protected override void Awake()
    {
        base.Awake();

        /* Hide by default all the menus */
        foreach (Transform menu in transform)
        {
            _menus.Add(menu.GetComponent<Menu>().id, menu.GetComponent<Menu>());
            menu.gameObject.SetActive(false);
        }
    }

    public void set_current_menu(MenuState new_state)
    {
        Menu new_menu = _menus[new_state];

        new_menu.enter();

        if (_current_menu != null)
        {
            if (!new_menu.is_modal)
            {
                hide_menu(_current_menu);
            }
        }

        _current_menu = new_menu;
        show_menu(_current_menu);
    }

    private void hide_menu(Menu menu)
    {
        menu.exit();
        menu.gameObject.SetActive(false);
    }

    private void show_menu(Menu menu)
    {
        menu.gameObject.SetActive(true);
        if (menu.first_button_selected != null) EventSystem.current.SetSelectedGameObject(menu.first_button_selected);
    }
}
