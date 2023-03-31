using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.GameFoundation;
using static GameFoundationUtils;

public enum GameState
{
    TITLE_MENU,
    MAIN_MENU,
    SHOP_MENU,
    GEARS_MENU,
    SCORES_MENU,
    LOADING,
    RUNNING,
    PAUSED,
    UNPAUSED,
    QUITTING,
    FAIL_MENU,
    VICTORY_MENU,
    STATS_MENU,
    REGISTER_MENU
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private LevelManager level_manager;

    [SerializeField]
    private DifficultyManager difficulty_manager;

    public GameState _state { get; private set; }

    [SerializeField]
    private Texture2D cursor_sprite;

    private void Start()
    {
        SceneController.Instance.init(GameMenu.TITLE);
        _state = GameState.TITLE_MENU;
        Cursor.SetCursor(cursor_sprite, new Vector2(cursor_sprite.width/2, cursor_sprite.height/2), CursorMode.Auto);
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
        switch (_state)
        {
            case GameState.SHOP_MENU:
                SaveSystem.Instance.Save();
                break;
            case GameState.GEARS_MENU:
                SaveSystem.Instance.Save();
                break;
            default:
                break;
        }

        // HANDLE ENTER
        SceneController.Instance.set_current_menu(GameMenu.MAIN);
        _state = GameState.MAIN_MENU;
    }

    private void handle_shop_menu()
    {
        // HANDLE QUIT
        switch (_state)
        {
            case GameState.GEARS_MENU:
                SaveSystem.Instance.Save();
                break;

            default:
                break;
        }

        // HANDLE ENTER
        SceneController.Instance.set_current_menu(GameMenu.SHOP);
        _state = GameState.SHOP_MENU;
    }

    private void handle_gears_menu()
    {
        // HANDLE QUIT
        switch (_state)
        {
            case GameState.SHOP_MENU:
                SaveSystem.Instance.Save();
                break;

            default:
                break;
        }

        // HANDLE ENTER
        SceneController.Instance.set_current_menu(GameMenu.GEARS);
        _state = GameState.GEARS_MENU;
    }

    private void handle_scores_menu()
    {
        // HANDLE QUIT
        switch (_state)
        {
            case GameState.REGISTER_MENU:
                SaveSystem.Instance.Save();
                break;

            default:
                break;
        }

        SceneController.Instance.set_current_menu(GameMenu.SCORES);
        _state = GameState.SCORES_MENU;
    }

    private void handle_loading()
    {
        GameLevel level_to_load = level_manager.get_selected_level();

        SceneController.Instance.load_level(level_to_load);
    }

    private void handle_running()
    {
        _state = GameState.RUNNING;
    }

    private void handle_paused()
    {
        SaveSystem.Instance.Save();
        AudioSystem.Instance.pause_music();

        /* Stops the game */
        Time.timeScale = 0.0f;
        SceneController.Instance.set_current_menu(GameMenu.PAUSE);
        _state = GameState.PAUSED;
    }

    private void handle_unpaused()
    {
        AudioSystem.Instance.unpause_music();

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
        AudioSystem.Instance.stop_music();

        SaveSystem.Instance.Save();

        SceneController.Instance.set_current_menu(GameMenu.FAIL);
        Time.timeScale = 0.0f;
        _state = GameState.FAIL_MENU;
    }

    private void handle_victory_menu()
    {
        AudioSystem.Instance.stop_music();

        SaveSystem.Instance.Save();

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
        int current_level = (int)level_manager.get_selected_level();
        int score_to_change = -1;
        InventoryItem level_scores = get_inventory_items_from_tag("SCORE")[current_level];

        for (int score = 0; score < 3; ++score)
        {
            if (level_scores.GetMutableProperty("score_" + score) < StatsManager.Instance.score)
            {
                score_to_change = score;
                break;
            }
        }

        if (score_to_change != -1)
        {
            SceneController.Instance.set_current_menu(GameMenu.REGISTER);
            _state = GameState.REGISTER_MENU;
        }
        else
        {
            set_state(GameState.MAIN_MENU);
        }
    }
}
