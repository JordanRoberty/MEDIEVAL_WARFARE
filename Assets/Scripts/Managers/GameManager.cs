using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    LOADING,
    RUNNING,
    PAUSED,
    UNPAUSED,
    RELOADING,
    QUITTING,
}

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
            default:
                throw new ArgumentOutOfRangeException(nameof(new_state), new_state, null);
        }
    }

    private void handle_loading()
    {
        MenuManager.Instance.set_state((ushort)MenuState.GAME_OVERLAY);
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

        MenuManager.Instance.set_state((ushort) MenuState.PAUSE_MENU);
        _state = GameState.PAUSED;
    }

    private void handle_unpaused()
    {
        Time.timeScale = 1.0f;
        MenuManager.Instance.set_state((ushort)MenuState.GAME_OVERLAY);
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
}
