using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private GameState _current_scene;

    private Dictionary<GameState, string> _scenes = new Dictionary<GameState, string>()
    {
        { GameState.TITLE_MENU,     "title_menu"    },
        { GameState.MAIN_MENU,      "main_menu"     },
        { GameState.SHOP_MENU,      "shop_menu"     },
        { GameState.GEARS_MENU,     "gears_menu"    },
        { GameState.SCORES_MENU,    "scores_menu"   },
        { GameState.PAUSED,         "paused_menu"   },
        { GameState.FAIL_MENU,      "shop_menu"     },
        { GameState.VICTORY_MENU,   "victory_menu"  },
        { GameState.STATS_MENU,     "stats_menu"    },
        { GameState.REGISTER_MENU,  "register_menu" },
    };

    public void load_scene(GameState scene)
    {
        SceneManager.LoadScene(_scenes[scene], LoadSceneMode.Additive);
        _current_scene = scene;
    }

    public void set_current_scene(GameState new_scene)
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
}
