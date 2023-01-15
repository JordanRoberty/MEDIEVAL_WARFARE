using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState _state { get; private set; }

    private void Start() => set_state(GameState.LOADING);
        

    private void Update()
    {
        if (Input.GetKeyDown("p") && _state == GameState.RUNNING)
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

    }

    private void handle_main_menu()
    {

    }

    private void handle_shop_menu()
    {

    }

    private void handle_gears_menu()
    {

    }

    private void handle_scores_menu()
    {

    }

    private void handle_loading()
    {
        GameUIManager.Instance.set_state(MenuState.GAME_OVERLAY);
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

        GameUIManager.Instance.set_state(MenuState.PAUSE_MENU);
        _state = GameState.PAUSED;
    }

    private void handle_unpaused()
    {
        Time.timeScale = 1.0f;
        GameUIManager.Instance.set_state(MenuState.GAME_OVERLAY);
        set_state(GameState.RUNNING);
    }

    private void handle_reloading()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void handle_quitting()
    {
        SceneManager.LoadSceneAsync("menu");
    }

    private void handle_fail_menu()
    {

    }

    private void handle_victory_menu()
    {

    }

    private void handle_stats_menu()
    {

    }

    private void handle_register_menu()
    {

    }
}
