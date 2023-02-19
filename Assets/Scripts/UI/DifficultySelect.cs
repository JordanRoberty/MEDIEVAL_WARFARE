using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultySelect : Select
{
    protected override void Awake()
    {
        base.Awake();
        update_options();
        set_current_option(0);
    }

    private void update_options()
    {
        dropdown.options.Clear();
        foreach (string option in DifficultyManager.Instance.available_difficulties)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(option));
        }
    }

    private void set_current_option(int index)
    {
        dropdown.value = index;
        DifficultyManager.Instance.current_difficulty = index;
    }

    public override void previous_option()
    {
        if (dropdown.value > 0)
        {
            set_current_option(dropdown.value - 1);
        }
    }

    public override void next_option()
    {
        if (dropdown.value < dropdown.options.Count)
        {
            set_current_option(dropdown.value + 1);
        }
    }
}