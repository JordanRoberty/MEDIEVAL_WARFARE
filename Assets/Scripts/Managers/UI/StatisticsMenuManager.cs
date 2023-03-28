using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticsMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _total_score;

    private void Start()
    {
        _total_score.text += StatsManager.Instance.score.ToString();
    }
}
