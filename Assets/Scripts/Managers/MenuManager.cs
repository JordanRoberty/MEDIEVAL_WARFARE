using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum MenuState : ushort
{
    TITLE = 0,
    MAIN = 1,
    SCORES = 2,
    SHOP = 3,
    GEARS = 4,
    START_GAME = 5,
    PAUSE = 6,
    REGISTER = 7,
    LEVEL = 8
}

public class MenuManager : MenuController
{
    [SerializeField]
    private MenuState _initial_state;

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

    private void Start() => set_state((ushort)_initial_state);

    public override void set_state(ushort new_state)
    {
        switch((MenuState)new_state)
        {
            case MenuState.TITLE:
                handle_title();
                break;
            case MenuState.MAIN:
                handle_main();
                break;
            case MenuState.SCORES:
                handle_scores();
                break;
            case MenuState.SHOP:
                handle_shop();
                break;
            case MenuState.GEARS:
                handle_gears();
                break;
            case MenuState.START_GAME:
                handle_start_game();
                break;
            case MenuState.REGISTER:
                handle_register();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(new_state), new_state, null);
        }
    }

    private void handle_title()
    {
        push_menu(_menus[MenuState.TITLE]);
        _state = MenuState.TITLE;
    }

    private void handle_main()
    {
        push_menu(_menus[MenuState.MAIN]);
        _state = MenuState.MAIN;
    }

    private void handle_scores()
    {
        push_menu(_menus[MenuState.SCORES]);
        _state = MenuState.SCORES;
    }

    private void handle_shop()
    {
        push_menu(_menus[MenuState.SHOP]);
        _state = MenuState.SHOP;
    }

    private void handle_gears()
    {
        push_menu(_menus[MenuState.GEARS]);
        _state = MenuState.GEARS;
    }

    private void handle_start_game()
    {
        SceneManager.LoadSceneAsync("level_one");
        set_state((ushort)MenuState.MAIN);        
    }

    private void handle_register()
    {
        push_menu(_menus[MenuState.REGISTER]);
        _state = MenuState.REGISTER;
    }
}