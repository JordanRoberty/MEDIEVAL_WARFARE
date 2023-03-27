using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : Singleton<StatsManager>
{
    public int score { get; private set; }
    
    void Start()
    {
        init();
    }

    public void init()
    {
        score = 0;
    }

    public void update_score(int point)
    {
        score += point;
    }
}
