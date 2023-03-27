using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : Singleton<BossFightManager>
{
    [SerializeField] private Transform _right_wall;
    [SerializeField] private Transform _end_level_trigger;

    public void boss_died()
    {
        _end_level_trigger.gameObject.SetActive(true);
        _right_wall.gameObject.SetActive(false);
    }
}
