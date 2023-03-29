using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticsMenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _total_score;

    [SerializeField]
    private TextMeshProUGUI _gold;

    [SerializeField]
    private TextMeshProUGUI _kills;

    [SerializeField]
    private TextMeshProUGUI _total_enemies;

    [SerializeField]
    private TextMeshProUGUI _hearts;

    [SerializeField]
    private TextMeshProUGUI _damage_taken;

    [SerializeField]
    private TextMeshProUGUI _damage_done;

    [SerializeField]
    private TextMeshProUGUI _jumps;

    private void Start()
    {
        _total_score.text += StatsManager.Instance.score.ToString();
        _gold.text += StatsManager.Instance.pieces.ToString();
        _kills.text += StatsManager.Instance.kills.ToString();
        _total_enemies.text += StatsManager.Instance.total_enemies.ToString();
        _hearts.text += StatsManager.Instance.hearts.ToString();
        _damage_taken.text += StatsManager.Instance.damage_taken.ToString();
        _damage_done.text += StatsManager.Instance.damage_done.ToString();
        _jumps.text += StatsManager.Instance.jumps.ToString();
    }
}
