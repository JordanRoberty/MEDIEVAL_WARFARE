using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.GameFoundation;
using static GameFoundationUtils;

public class DifficultyManager : Singleton<DifficultyManager>
{
    public List<string> available_difficulties { get; private set; }
    public int selected_difficulty;

    private InventoryItem _difficulties;


    protected override void Awake() {
        base.Awake();

        List<InventoryItem> difficulties_container = get_inventory_items_from_tag("DIFFICULTIES");
        Assert.IsTrue(difficulties_container.Count == 1);

        _difficulties = difficulties_container[0];
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
