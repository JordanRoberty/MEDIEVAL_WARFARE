using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    HOME,
    LEVEL_1,
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
    private GameScene _current_scene;
    private GameMenu _current_menu;

    private Dictionary<GameScene, string> _scenes = new Dictionary<GameScene, string>()
    {
        { GameScene.HOME,        "home"      },
        { GameScene.LEVEL_1,     "level_1"   },
        { GameScene.LEVEL_2,     "level_2"   },
        { GameScene.LEVEL_3,     "level_3"   },
    };

    private Dictionary<GameMenu, string> _menus = new Dictionary<GameMenu, string>()
    {
        { GameMenu.TITLE,       "title_menu"    },
        { GameMenu.MAIN,        "main_menu"     },
        { GameMenu.SHOP,        "shop_menu"     },
        { GameMenu.GEARS,       "gears_menu"    },
        { GameMenu.SCORES,      "scores_menu"   },
        { GameMenu.PAUSE,       "pause_menu"    },
        { GameMenu.FAIL,        "fail_menu"     },
        { GameMenu.VICTORY,     "victory_menu"  },
        { GameMenu.STATS,       "stats_menu"    },
        { GameMenu.REGISTER,    "register_menu" },
    };

    public void init(GameScene initial_scene, GameMenu initial_menu)
    {
        SceneManager.LoadScene(_scenes[initial_scene], LoadSceneMode.Additive);
        _current_scene = initial_scene;
        SceneManager.LoadScene(_menus[initial_menu], LoadSceneMode.Additive);
        _current_menu = initial_menu;

    }

    public void set_current_scene(GameScene new_scene)
    {
        SceneManager.LoadScene("loading_menu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_scenes[_current_scene]);
        
        AsyncOperation load_new_scene = SceneManager.LoadSceneAsync(_scenes[new_scene], LoadSceneMode.Additive);
        load_new_scene.completed += (AsyncOperation result) =>
        {
            _current_scene = new_scene;
            SceneManager.UnloadSceneAsync("loading_menu");
        };
    }

    public void set_current_menu(GameMenu new_menu)
    {
        if(_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);

        if(new_menu != GameMenu.NONE)
        {
            AsyncOperation load_new_menu = SceneManager.LoadSceneAsync(_menus[new_menu], LoadSceneMode.Additive);
            load_new_menu.completed += (AsyncOperation result) =>
            {
                _current_menu = new_menu;
            };
        }
        else
        {
            _current_menu = GameMenu.NONE;
        }
    }

    public void load_level(GameScene level)
    {
        SceneManager.LoadScene("loading_menu", LoadSceneMode.Additive);
        
        // Manage Scene
        SceneManager.UnloadSceneAsync(_scenes[_current_scene]);
        AsyncOperation load_level = SceneManager.LoadSceneAsync(_scenes[level], LoadSceneMode.Additive);
        load_level.completed += (AsyncOperation result) =>
        {
            _current_scene = level;

            // Manage Menu
            if (_current_menu != GameMenu.NONE)  SceneManager.UnloadSceneAsync(_menus[_current_menu]);
            _current_menu = GameMenu.NONE;
            SceneManager.UnloadSceneAsync("loading_menu");
        };
    }

    public void load_main_menu()
    {
        SceneManager.LoadScene("loading_menu", LoadSceneMode.Additive);

        // Manage Scene
        SceneManager.UnloadSceneAsync(_scenes[_current_scene]);
        AsyncOperation load_scene = SceneManager.LoadSceneAsync(_scenes[GameScene.HOME], LoadSceneMode.Additive);
        load_scene.completed += (AsyncOperation result) =>
        {
            _current_scene = GameScene.HOME;

            // Manage Menu
            if (_current_menu != GameMenu.NONE) SceneManager.UnloadSceneAsync(_menus[_current_menu]);
            AsyncOperation load_menu = SceneManager.LoadSceneAsync(_menus[GameMenu.MAIN], LoadSceneMode.Additive);
            load_menu.completed += (AsyncOperation result) =>
            {
                _current_menu = GameMenu.MAIN;
                SceneManager.UnloadSceneAsync("loading_menu");
            };
        };
    }
}
