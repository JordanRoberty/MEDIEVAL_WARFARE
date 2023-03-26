using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : Singleton<LevelLoader>
{
    [SerializeField] private EnemySpawner _enemy_spawner;

    void Start()
    {
        // INIT PLAYER SCENE COMPONENTS
        FindObjectOfType<RuneManager>().init();
        FindObjectOfType<PlayerManager>().init();

        // INIT LEVEL SCENE COMPONENTS
        //_enemy_spawner.init();
    }
}
