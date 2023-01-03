using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    private MenuState _state;

    public void set_state(MenuState new_state)
    {
        switch (new_state)
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
        CanvasManager.Instance.set_current_menu(MenuState.GAME_OVERLAY);
        _state = MenuState.GAME_OVERLAY;
    }

    private void handle_pause_menu()
    {
        CanvasManager.Instance.set_current_menu(MenuState.PAUSE_MENU);
        _state = MenuState.PAUSE_MENU;
    }
}
