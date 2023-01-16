using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState _state { get; private set; }

    [SerializeField]
    private GameState _initial_state;

    private void Start()
    {
        SceneController.Instance.load_scene(_initial_state);
        _state = _initial_state;
    }
        
    private void Update()
    {
        if (_state == GameState.RUNNING && Input.GetKeyDown("p"))
        {
            set_state(GameState.PAUSED);
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
            case GameState.RELOADING:
                handle_reloading();
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
        SceneController.Instance.set_current_scene(GameState.TITLE_MENU);
    }

    private void handle_main_menu()
    {
        SceneController.Instance.set_current_scene(GameState.MAIN_MENU);
    }

    private void handle_shop_menu()
    {
        SceneController.Instance.set_current_scene(GameState.SHOP_MENU);
    }

    private void handle_gears_menu()
    {
        SceneController.Instance.set_current_scene(GameState.GEARS_MENU);
    }

    private void handle_scores_menu()
    {
        SceneController.Instance.set_current_scene(GameState.SCORES_MENU);
    }

    private void handle_loading()
    {
        Time.timeScale = 1.0f;
        set_state(GameState.RUNNING);
    }

    private void handle_running()
    {
        _state = GameState.RUNNING;
    }

    private void handle_paused()
    {
        /* Stops the game */
        Time.timeScale = 0.0f;

        _state = GameState.PAUSED;
    }

    private void handle_unpaused()
    {
        Time.timeScale = 1.0f;
        set_state(GameState.RUNNING);
    }

    private void handle_reloading()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void handle_quitting()
    {
        SceneController.Instance.set_current_scene(GameState.MAIN_MENU);
    }

    private void handle_fail_menu()
    {
        SceneController.Instance.set_current_scene(GameState.FAIL_MENU);
    }

    private void handle_victory_menu()
    {
        SceneController.Instance.set_current_scene(GameState.VICTORY_MENU);
    }

    private void handle_stats_menu()
    {
        SceneController.Instance.set_current_scene(GameState.STATS_MENU);
    }

    private void handle_register_menu()
    {
        SceneController.Instance.set_current_scene(GameState.REGISTER_MENU);
    }
}
