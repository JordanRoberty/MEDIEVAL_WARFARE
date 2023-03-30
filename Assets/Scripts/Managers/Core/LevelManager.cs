using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using static GameFoundationUtils;

public enum GameLevel
{
    LEVEL_1,
    LEVEL_2,
    LEVEL_3,
    NONE,
}

public class LevelManager : Singleton<LevelManager>
{
    public List<string> available_levels { get; private set; }
    public int selected_level;

    private InventoryItem _levels;
    
    protected override void Awake() {
        base.Awake();

        _levels = get_inventory_items_from_tag("LEVELS")[0];
        available_levels = new List<string>();

        for(int i = 1; i < _levels.GetMutableProperty("nb_levels"); ++i)
        {
            if (_levels.GetMutableProperty("level_available_" + i))
            {
                available_levels.Add(_levels.GetMutableProperty("level_label_" + i));
            }
            else
            {
                break;
            }
        }

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
