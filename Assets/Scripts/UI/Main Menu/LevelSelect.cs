using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelect : Select
{
    protected override void Awake()
    {
        base.Awake();
        update_options();
        set_current_option(LevelManager.Instance.selected_level);
    }

    public void update_options()
    {
        dropdown.options.Clear();
        foreach (string option in LevelManager.Instance.available_levels)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(option));
        }
    }

    private void set_current_option(int index)
    {
        dropdown.value = index;
        LevelManager.Instance.selected_level = index;
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
        if (dropdown.value < (dropdown.options.Count - 1))
        {
            set_current_option(dropdown.value + 1);
        }
    }
}
