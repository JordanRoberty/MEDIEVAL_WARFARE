using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private Heart _heart_prefab;

    private int _max_health;
    private int _local_health;
    private int _shield;

    private List<Heart> _hearts;

    public void Start()
    {
        _max_health = _player.max_health;
        _local_health = _player.health;

        transform.destroy_children();
        _hearts = new List<Heart>();

        for(int i = 0; i < _max_health; ++i)
        {
            Heart heart = Instantiate(
                _heart_prefab,
                Vector3.zero,
                Quaternion.identity,
                transform
            ).GetComponent<Heart>();

            _hearts.Add(heart);
        }
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
        _local_health = _player.health;
        
        for (int i = 0; i < _hearts.Count; ++i)
        {
            if (i < _local_health)
            {
                _hearts[i].set_full();

            }
            else
            {
                _hearts[i].set_empty();
            }
        }
    }
}
