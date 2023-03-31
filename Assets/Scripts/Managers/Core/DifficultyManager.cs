using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using static GameFoundationUtils;

public class DifficultyManager : Singleton<DifficultyManager>
{
    public List<string> available_difficulties { get; private set; }
    public int selected_difficulty;

    private InventoryItem _difficulties;


    protected override void Awake() {
        base.Awake();

        _difficulties = get_inventory_items_from_tag("DIFFICULTIES")[0];
        available_difficulties = new List<string>();

        for (int i = 1; i < _difficulties.GetMutableProperty("nb_difficulties"); ++i)
        {
            if (_difficulties.GetMutableProperty("difficulty_available_" + i))
            {
                available_difficulties.Add(_difficulties.GetMutableProperty("difficulty_label_" + i));
            }
            else
            {
                break;
            }
        }

        selected_difficulty = 0;
    }
}
