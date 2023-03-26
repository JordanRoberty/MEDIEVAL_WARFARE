using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : Singleton<LevelLoader>
{
    [SerializeField] private Camera _main_camera;
    [SerializeField] private EnemySpawner _enemy_spawner;

    private RuneManager _rune_manager;
    private PlayerManager _player_manager;

    void Start()
    {
        // INIT PLAYER SCENE COMPONENTS
        _rune_manager = FindObjectOfType<RuneManager>();
        _player_manager = FindObjectOfType<PlayerManager>();

        _rune_manager.init();
        _player_manager.init(_main_camera);

        // INIT LEVEL SCENE COMPONENTS
        //_enemy_spawner.init();
    }
}
