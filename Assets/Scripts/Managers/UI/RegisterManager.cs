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

        string rank = add_rank_extention(_score_to_change + 1);

        _explanation_text.text = "Congratulations ! You just made the " + rank + " best score of this level !";
    }

    private string add_rank_extention(int rank)
    {
        switch (rank)
        {
            case 1:
                return rank.ToString() + "st";

            case 2:
                return rank.ToString() + "nd";

            case 3:
                return rank.ToString() + "rd";

            default:
                return rank.ToString() + "th";
        }
    }
}
