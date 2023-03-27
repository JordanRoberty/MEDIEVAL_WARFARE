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
    [SerializeField] private Transform _error_text;

    private InventoryItem level_scores;
    private int _score_to_change;
    private List<Score> _scores;

    public void Start()
    {
        int current_level = (int) LevelManager.Instance.get_selected_level();
        level_scores = get_inventory_items_from_tag("SCORE")[current_level];

        _scores = new List<Score>();
        _score_to_change = -1;

        for (int score = 0; score < 3; ++score)
        {
            if (_score_to_change == -1 && level_scores.GetMutableProperty("score_" + score) < StatsManager.Instance.score)
            {
                _score_to_change = score;
            }

            _scores.Add(new Score(
                level_scores.GetMutableProperty("name_" + score),
                level_scores.GetMutableProperty("score_" + score)
            ));
        }

        string rank = add_rank_extention(_score_to_change + 1);

        _explanation_text.text =
            "Congratulations ! You just made the " + rank + " best score of this level!" +
            "\nPlease enter your pseudo (3 letters max) to engrave your name into history :";
    }

    public void on_pseudo_submitted()
    {
        if(pseudo_is_valid())
        {
            Score new_score = new Score(_input_text.text, StatsManager.Instance.score);

            _scores.Insert(_score_to_change, new_score);

            for(int i = 0; i < 3; ++i)
            {
                level_scores.SetMutableProperty("name_" + i, _scores[i].name);
                level_scores.SetMutableProperty("score_" + i, _scores[i].score);
            }

            GameManager.Instance.set_state(GameState.SCORES_MENU);
        }
        else
        {
            _error_text.gameObject.SetActive(true);
        }
    }

    private bool pseudo_is_valid()
    {
        return _input_text.text.Length == 3 && _input_text.text != "___";
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
