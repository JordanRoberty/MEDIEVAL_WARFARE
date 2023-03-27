using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameLevel
{
    NONE,
    LEVEL_1,
    BOSS_1,
    LEVEL_2,
    LEVEL_3,
}

public enum GameMenu
{
    TITLE,
    MAIN,
    SHOP,
    GEARS,
    SCORES,
    PAUSE,
    FAIL,
    VICTORY,
    STATS,
    REGISTER,
    NONE
}

public class SceneController : Singleton<SceneController>
{
    private GameLevel _current_level;
    private GameMenu _current_menu;

    private Dictionary<GameLevel, string> _levels = new Dictionary<GameLevel, string>()
    {
        { GameLevel.LEVEL_1,     "level_1"   },
        { GameLevel.BOSS_1,     "boss_level_1"   },
        { GameLevel.LEVEL_2,     "level_2"   },
        { GameLevel.LEVEL_3,     "level_3"   },
    };

    private Dictionary<GameMenu, string> _menus = new Dictionary<GameMenu, string>()
    {
        { GameMenu.TITLE,       "title_menu"        },
        { GameMenu.MAIN,        "main_menu"         },
        { GameMenu.SHOP,        "shop_menu"         },
        { GameMenu.GEARS,       "gears_menu"        },
        { GameMenu.SCORES,      "scores_menu"       },
        { GameMenu.PAUSE,       "pause_menu"        },
        { GameMenu.FAIL,        "fail_menu"         },
        { GameMenu.VICTORY,     "victory_menu"      },
        { GameMenu.STATS,       "statistics_menu"   },
        { GameMenu.REGISTER,    "register_menu"     },
    };

    public void init(GameMenu initial_menu)
    {
        SceneManager.LoadScene(_menus[initial_menu], LoadSceneMode.Additive);
        _current_menu = initial_menu;
        _current_level = GameLevel.NONE;
    }

    public void set_current_menu(GameMenu new_menu)
    {
        if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);

        LoadSceneMode load_mode = _current_level != GameLevel.NONE ? LoadSceneMode.Additive : LoadSceneMode.Single;

        if (new_menu != GameMenu.NONE)
        {
            SceneManager.LoadScene(_menus[new_menu], load_mode);
            _current_menu = new_menu;   
        }
        else
        {
            _current_menu = GameMenu.NONE;
        }
    }

    public IEnumerator load_level(GameLevel level)
    {
        // In case of restart, destroy current level scenes
        if (SceneManager.GetSceneByName("Player").isLoaded) yield return SceneManager.UnloadSceneAsync("Player");
        if(_current_level != GameLevel.NONE) SceneManager.UnloadSceneAsync(_levels[_current_level]);

        // Load player
        Time.timeScale = 0.0f;
        AsyncOperation load_Player = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        load_Player.completed += (AsyncOperation result) =>
        {
            // Load Level
            AsyncOperation load_level = SceneManager.LoadSceneAsync(_levels[level], LoadSceneMode.Additive);
            load_level.completed += (AsyncOperation result) =>
            {
                _current_level = level;

                LevelLoader.Instance.init();

                // Manage Menu
                if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);
                _current_menu = GameMenu.NONE;

                Time.timeScale = 1.0f;
                GameManager.Instance.set_state(GameState.RUNNING);
            };
        };
    }

    public void load_boss_level(GameLevel boss_level)
    {
        // Unload level scene
        SceneManager.UnloadSceneAsync(_levels[_current_level]);

        // Load Boss Level
        Time.timeScale = 0.0f;
        AsyncOperation load_level = SceneManager.LoadSceneAsync(_levels[boss_level], LoadSceneMode.Additive);
        load_level.completed += (AsyncOperation result) =>
        {
            _current_level = boss_level;

            LevelLoader.Instance.init();

            // Manage Menu
            if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);
            _current_menu = GameMenu.NONE;

            Time.timeScale = 1.0f;
        };
    }

    public void load_victory_menu()
    {
        // Unload level scenes
        AudioSystem.Instance.stop_music();
        SceneManager.UnloadSceneAsync("Player");
        SceneManager.UnloadSceneAsync(_levels[_current_level]);
        _current_level = GameLevel.NONE;

        GameManager.Instance.set_state(GameState.VICTORY_MENU);
    }

    public void load_main_menu()
    {
        // Unload level
        AudioSystem.Instance.stop_music();
        SceneManager.UnloadSceneAsync("Player");
        SceneManager.UnloadSceneAsync(_levels[_current_level]);
        _current_level = GameLevel.NONE;

        // Unload Menu
        if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);

        // Load Main Menu
        SceneManager.LoadScene(_menus[GameMenu.MAIN]);
        _current_menu = GameMenu.MAIN;
        Time.timeScale = 1.0f;
    }
}
