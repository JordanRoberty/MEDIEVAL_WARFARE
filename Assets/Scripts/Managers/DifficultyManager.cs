using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
    public int current_difficulty;
    public List<string> available_difficulties { get; private set; }

    private List<string> difficulties = new List<string>()
    {
        "NORMAL",
        "DIFFICULT",
        "HARDCORE"
    };


    protected override void Awake() {
        base.Awake();

        available_difficulties = new List<string>();
        available_difficulties.Add(difficulties[0]);
        available_difficulties.Add(difficulties[1]);
        current_difficulty = 0;
    }
}
