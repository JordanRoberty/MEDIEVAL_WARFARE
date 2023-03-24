using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI score_text;
    // Start is called before the first frame update
    void Start()
    {
        score_text.SetText("SCORE : " + score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void update_score(int point)
    {
        score += point;
        score_text.SetText("SCORE : " + score);
    }
}
