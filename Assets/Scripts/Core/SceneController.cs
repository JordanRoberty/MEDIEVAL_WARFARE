using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BossLevel
{
    BOSS_1,
    BOSS_2,
    BOSS_3,
    NONE,
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
    private BossLevel _current_boss_level;
    private GameMenu _current_menu;

    private Dictionary<GameLevel, string> _levels = new Dictionary<GameLevel, string>()
    {
        { GameLevel.LEVEL_1,     "level_1"   },
        { GameLevel.LEVEL_2,     "level_2"   },
        { GameLevel.LEVEL_3,     "level_3"   },
    };

    private Dictionary<BossLevel, string> _boss_levels = new Dictionary<BossLevel, string>()
    {
        { BossLevel.BOSS_1,     "boss_level_1"   },
        { BossLevel.BOSS_2,     "boss_level_2"   },
        { BossLevel.BOSS_3,     "boss_level_3"   },
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
        _current_boss_level = BossLevel.NONE;
    }

    private void unload_current_level()
    {   
        if (_current_level != GameLevel.NONE)
        {
            SceneManager.UnloadSceneAsync(_levels[_current_level]);
            _current_level = GameLevel.NONE;
        }

        if (_current_boss_level != BossLevel.NONE)
        {
            SceneManager.UnloadSceneAsync(_boss_levels[_current_boss_level]);
            _current_boss_level = BossLevel.NONE;
        }

        if (SceneManager.GetSceneByName("Player").isLoaded) SceneManager.UnloadSceneAsync("Player");
    }

    public void set_current_menu(GameMenu new_menu)
    {
        if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);

        bool in_game = _current_level != GameLevel.NONE || _current_boss_level != BossLevel.NONE;
        LoadSceneMode load_mode = in_game ? LoadSceneMode.Additive : LoadSceneMode.Single;

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

    public void load_level(GameLevel level)
    {
        // In case of restart, destroy current level scenes
        unload_current_level();

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

    public void load_boss_level(BossLevel boss_level)
    {
        // Unload level scene
        SceneManager.UnloadSceneAsync(_levels[_current_level]);
        _current_level = GameLevel.NONE;

        // Load Boss Level
        Time.timeScale = 0.0f;
        AsyncOperation load_level = SceneManager.LoadSceneAsync(_boss_levels[boss_level], LoadSceneMode.Additive);
        load_level.completed += (AsyncOperation result) =>
        {
            _current_boss_level = boss_level;

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
        unload_current_level();

        GameManager.Instance.set_state(GameState.VICTORY_MENU);
    }

    public void load_main_menu()
    {
        // Unload level
        AudioSystem.Instance.stop_music();
        unload_current_level();

        // Unload Menu
        if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);

        // Load Main Menu
        SceneManager.LoadScene(_menus[GameMenu.MAIN]);
        _current_menu = GameMenu.MAIN;
        Time.timeScale = 1.0f;
    }
}
