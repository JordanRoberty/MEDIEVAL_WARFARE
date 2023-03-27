using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using static GameFoundationUtils;
using TMPro;

public class RegisterManager : Singleton<RegisterManager>
{
    [SerializeField] private TextMeshProUGUI _explanation_text;
    [SerializeField] private TMP_InputField _input_text;

    private int _score_to_change;

    public void Start()
    {
        int current_level = (int) LevelManager.Instance.get_selected_level();
        InventoryItem level_scores = get_inventory_items_from_tag("SCORE")[current_level];

        for (int score = 0; score < 3; ++score)
        {
            if (level_scores.GetMutableProperty("score_" + score) < StatsManager.Instance.score)
            {
                _score_to_change = score;
                break;
            }
        }

        _explanation_text.text = "Congratulations ! You just made the " + _score_to_change + " best score of this level !";
    }
}
