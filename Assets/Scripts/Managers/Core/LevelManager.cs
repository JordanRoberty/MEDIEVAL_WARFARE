using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
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
    public int nb_levels;

    private InventoryItem _levels;
    
    protected override void Awake() {
        base.Awake();

        List<InventoryItem> levels_container = get_inventory_items_from_tag("LEVELS");
        Assert.IsTrue(levels_container.Count == 1);

        _levels = levels_container[0];
        available_levels = new List<string>();

        selected_level = 0;
        nb_levels = _levels.GetMutableProperty("nb_levels");

        update_available_levels();
    }

    private void update_available_levels()
    {
        available_levels.Clear();

        for (int i = 1; i < nb_levels; ++i)
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

    public void update_levels()
    {
        int current_level = selected_level + 1;
        if (current_level < nb_levels)
        {
            _levels.SetMutableProperty("level_available_" + (current_level + 1), true);
            update_available_levels();
        }
    }
}
