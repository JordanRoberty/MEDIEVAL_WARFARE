using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI score_text;
    
    void Start()
    {
        score_text.SetText("SCORE : " + score);
    }

    public void update_score(int point)
    {
        score += point;
        score_text.SetText("SCORE : " + score);
    }
}
