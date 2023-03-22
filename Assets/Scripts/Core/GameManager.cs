using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.GameFoundation;

public class GameManager : Singleton<GameManager>
{
    public GameState _state { get; private set; }

    [SerializeField]
    private GameState _initial_state;
    [SerializeField]
    private GameScene _initial_scene;
    [SerializeField]
    private GameMenu _initial_menu;

    private LevelManager level_manager;
    private DifficultyManager difficulty_manager;

    private bool player_alive = true;

    public void set_player_status(bool status){
        player_alive = status;
    }

    private void Start()
    {
        SceneController.Instance.init(_initial_scene, _initial_menu);
        _state = _initial_state;

        level_manager = GetComponentInChildren<LevelManager>();
        difficulty_manager = GetComponentInChildren<DifficultyManager>();
    }
        
    private void Update()
    {
        if (_state == GameState.RUNNING && Input.GetKeyDown("p"))
        {
            set_state(GameState.PAUSED);
        }

        if(!player_alive)
        {
            Debug.Log("DEAD");
            set_state(GameState.FAIL_MENU);
        }
    }

    public void set_state(GameState new_state)
    {
        switch (new_state)
        {
            case GameState.TITLE_MENU:
                handle_title_menu();
                break;
            case GameState.MAIN_MENU:
                handle_main_menu();
                break;
            case GameState.SHOP_MENU:
                handle_shop_menu();
                break;
            case GameState.GEARS_MENU:
                handle_gears_menu();
                break;
            case GameState.SCORES_MENU:
                handle_scores_menu();
                break;
            case GameState.LOADING:
                handle_loading();
                break;
            case GameState.RUNNING:
                handle_running();
                break;
            case GameState.PAUSED:
                handle_paused();
                break;
            case GameState.UNPAUSED:
                handle_unpaused();
                break;
            case GameState.QUITTING:
                handle_quitting();
                break;
            case GameState.FAIL_MENU:
                handle_fail_menu();
                break;
            case GameState.VICTORY_MENU:
                handle_victory_menu();
                break;
            case GameState.STATS_MENU:
                handle_stats_menu();
                break;
            case GameState.REGISTER_MENU:
                handle_register_menu();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(new_state), new_state, null);
        }
    }

    private void handle_title_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.TITLE);
        _state = GameState.TITLE_MENU;
    }

    private void handle_main_menu()
    {
        // HANDLE QUIT
        if(_state == GameState.SHOP_MENU)
        {
            SaveSystem.Instance.Save();
        }

        // HANDLE ENTER
        SceneController.Instance.set_current_menu(GameMenu.MAIN);
        _state = GameState.MAIN_MENU;
    }

    private void handle_shop_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.SHOP);
        _state = GameState.SHOP_MENU;
    }

    private void handle_gears_menu()
    {
        // HANDLE QUIT
        if (_state == GameState.SHOP_MENU)
        {
            SaveSystem.Instance.Save();
        }

        // HANDLE ENTER
        SceneController.Instance.set_current_menu(GameMenu.GEARS);
        _state = GameState.GEARS_MENU;
    }

    private void handle_scores_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.SCORES);
        _state = GameState.SCORES_MENU;
    }

    private void handle_loading()
    {
        GameScene level_to_load = GameScene.HOME;

        switch(level_manager.current_level)
        {
            case 0:
                level_to_load = GameScene.LEVEL_1;
                break;
            case 1:
                level_to_load = GameScene.LEVEL_2;
                break;
            case 2:
                level_to_load = GameScene.LEVEL_3;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level_manager.current_level), level_manager.current_level, null);
        }

        SceneController.Instance.load_level(level_to_load);
        set_state(GameState.RUNNING);
    }

    private void handle_running()
    {
        Time.timeScale = 1.0f;
        _state = GameState.RUNNING;
    }

    private void handle_paused()
    {
        SceneController.Instance.set_current_menu(GameMenu.PAUSE);
        /* Stops the game */
        Time.timeScale = 0.0f;

        _state = GameState.PAUSED;
    }

    private void handle_unpaused()
    {
        SceneController.Instance.set_current_menu(GameMenu.NONE);
        Time.timeScale = 1.0f;
        set_state(GameState.RUNNING);
    }

    private void handle_quitting()
    {
        SceneController.Instance.load_main_menu();
        _state = GameState.MAIN_MENU;
    }

    private void handle_fail_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.FAIL);
        _state = GameState.FAIL_MENU;
    }

    private void handle_victory_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.VICTORY);
        _state = GameState.VICTORY_MENU;
    }

    private void handle_stats_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.STATS);
        _state = GameState.STATS_MENU;
    }

    private void handle_register_menu()
    {
        SceneController.Instance.set_current_menu(GameMenu.REGISTER);
        _state = GameState.REGISTER_MENU;
    }
}
