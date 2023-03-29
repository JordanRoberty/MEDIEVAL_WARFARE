using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rank_text;
    [SerializeField] private TextMeshProUGUI name_text;
    [SerializeField] private TextMeshProUGUI score_text;

    public void init(int rank, Score score)
    {
        rank_text.text = (rank + 1).ToString();
        name_text.text = score.name;
        score_text.text = score.score.ToString();
    }
}
