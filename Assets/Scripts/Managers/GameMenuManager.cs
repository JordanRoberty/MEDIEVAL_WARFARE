using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MenuManager
{
    private MenuState _state;
    private Dictionary<MenuState, Menu> _menus = new Dictionary<MenuState, Menu>();

    protected override void Awake()
    {
        base.Awake();

        /* Store all the specific menu for a specific state */
        foreach (Transform menu in root_canvas)
        {
            _menus.Add(menu.GetComponent<Menu>().id, menu.GetComponent<Menu>());
        }
    }

    public override void set_state(ushort new_state)
    {
        switch ((MenuState)new_state)
        {
            case MenuState.GAME_OVERLAY:
                handle_game_overlay();
                break;
            case MenuState.PAUSE_MENU:
                handle_pause_menu();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(new_state), new_state, null);
        }
    }

    private void handle_game_overlay()
    {
        push_menu(_menus[MenuState.GAME_OVERLAY]);
        _state = MenuState.GAME_OVERLAY;
    }

    private void handle_pause_menu()
    {
        push_menu(_menus[MenuState.PAUSE_MENU]);
        _state = MenuState.PAUSE_MENU;
    }
}
