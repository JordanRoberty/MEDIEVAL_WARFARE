using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class UIManager : Singleton<UIManager>
{
    [SerializeField]
    protected Transform root_canvas;
    [SerializeField]
    private Menu initial_menu;

    private Menu _current_menu;

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

    public void set_current_menu(Menu new_menu)
    {
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
