using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MenuUIManager : UIManager
{
    [SerializeField]
    private TMP_Dropdown level_selector;
    [SerializeField]
    private TMP_Dropdown difficulty_selector;

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
        set_current_menu(_menus[MenuState.TITLE]);
        _state = MenuState.TITLE;
    }

    private void handle_main()
    {
        set_current_menu(_menus[MenuState.MAIN]);
        _state = MenuState.MAIN;
    }

    private void handle_scores()
    {
        set_current_menu(_menus[MenuState.SCORES]);
        _state = MenuState.SCORES;
    }

    private void handle_shop()
    {
        set_current_menu(_menus[MenuState.SHOP]);
        _state = MenuState.SHOP;
    }

    private void handle_gears()
    {
        set_current_menu(_menus[MenuState.GEARS]);
        _state = MenuState.GEARS;
    }

    private void handle_start_game()
    {
        string menu_selected = level_selector.options[level_selector.value].text;
        SceneManager.LoadSceneAsync(menu_selected);
        set_state((ushort)MenuState.MAIN);
    }

    private void handle_register()
    {
        set_current_menu(_menus[MenuState.REGISTER]);
        _state = MenuState.REGISTER;
    }
}
