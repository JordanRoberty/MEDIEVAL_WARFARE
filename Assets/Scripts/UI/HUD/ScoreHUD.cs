using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score_text;

    private int _local_score; 

    // Start is called before the first frame update
    void Start()
    {
        _local_score = 0;
        score_text.text = _local_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(_local_score != StatsManager.Instance.score)
        {
            _local_score = StatsManager.Instance.score;
            score_text.text = _local_score.ToString();
        }
    }
}
