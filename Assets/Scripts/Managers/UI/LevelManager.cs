using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLevel
{
    LEVEL_1,
    LEVEL_2,
    LEVEL_3,
    NONE,
}

public class LevelManager : Singleton<LevelManager>
{
    public int selected_level;
    public List<string> available_levels { get; private set; }

    private List<string> levels = new List<string>()
    {
        "LEVEL 1",
        "LEVEL 2",
        "LEVEL 3"
    };


    protected override void Awake() {
        base.Awake();

        available_levels = new List<string>();
        available_levels.Add(levels[0]);
        selected_level = 0;
    }

    public GameLevel get_selected_level()
    {
        GameLevel current_level = GameLevel.NONE;

        switch (selected_level)
        {
            case 0:
                current_level = GameLevel.LEVEL_1;
                break;
            case 1:
                current_level = GameLevel.LEVEL_2;
                break;
            case 2:
                current_level = GameLevel.LEVEL_3;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(current_level), current_level, null);
        }

        return current_level;
    }
}
