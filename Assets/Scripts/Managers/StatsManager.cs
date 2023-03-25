using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : Singleton<StatsManager>
{
    public int score { get; private set; }
    
    void Start()
    {
        score = 0;
    }

    public void update_score(int point)
    {
        score += point;
    }
}
