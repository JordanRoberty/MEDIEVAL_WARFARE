using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShieldGauge : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private Transform _shield_gauge;
    [SerializeField] private Shield _shield_prefab;

    private int _local_shield;
    private int _shield;

    private List<Shield> _shields;

    public void Start()
    {
        // Temporary functionning waiting for _max_shield in PlayerManager
        _local_shield = 0;
        _shields = new List<Shield>();
    }

    void Update()
    {
        if(_local_shield != _player.shield)
        {
            update_shield_gauge();
        }
    }

    private void update_shield_gauge()
    {
        // Temporary functionning waiting for _max_shield in PlayerManager
        _local_shield = _player.shield;
        
        _shield_gauge.destroy_children();
        _shields.Clear();

        for (int i = 0; i < _local_shield; ++i)
        {
            Shield shield = Instantiate(
                _shield_prefab,
                Vector3.zero,
                Quaternion.identity,
                _shield_gauge
            ).GetComponent<Shield>();

            if (i < _local_shield)
            {
                shield.set_full();

            }
            else
            {
                shield.set_empty();
            }

            _shields.Add(shield);
        }
    }
}
