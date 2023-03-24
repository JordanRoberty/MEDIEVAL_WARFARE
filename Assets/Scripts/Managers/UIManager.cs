using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI score_text;

    public int nb_coins = 0;//replace by the nmber of coins in the wallet
    [SerializeField] private TextMeshProUGUI coins_text;
    // Start is called before the first frame update
    void Start()
    {
        score_text.SetText("SCORE : " + score);
        coins_text.SetText("MONEY : " + nb_coins);
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

    public void update_coins(int coins)
    {
        nb_coins += coins;
        coins_text.SetText("MONEY : " + nb_coins);
    }
}
