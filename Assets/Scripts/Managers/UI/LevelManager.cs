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
    public int current_level;
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
        current_level = 0;
    }
}
