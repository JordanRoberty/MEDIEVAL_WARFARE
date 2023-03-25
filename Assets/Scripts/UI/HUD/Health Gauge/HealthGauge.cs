using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthGauge : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private Transform _heart_gauge;
    [SerializeField] private Heart _heart_prefab;

    private int _max_health;
    private int _local_health;
    private int _shield;

    private List<Heart> _hearts;

    public void Start()
    {
        _max_health = 0;
        _local_health = 0;
        _hearts = new List<Heart>();
    }

    void Update()
    {
        if(_local_health != _player.health)
        {
            update_health();
        }
    }

    private void update_health()
    {
        _max_health = _player.max_health;
        _local_health = _player.health;

        _heart_gauge.destroy_children();
        _hearts.Clear();

        for (int i = 0; i < _max_health; ++i)
        {
            Heart heart = Instantiate(
                _heart_prefab,
                Vector3.zero,
                Quaternion.identity,
                _heart_gauge
            ).GetComponent<Heart>();

            if (i < _local_health)
            {
                heart.set_full();

            }
            else
            {
                heart.set_empty();
            }

            _hearts.Add(heart);
        }
    }
}
