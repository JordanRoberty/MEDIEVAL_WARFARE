using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicLevelLoader : LevelLoader
{
    [SerializeField] private CameraCrusher _camera_crusher;
    [SerializeField] private EnemySpawner _enemy_spawner;

    private RuneManager _rune_manager;

    public override void init()
    {
        // Set player position in parent
        base.init();

        // INIT PLAYER SCENE COMPONENTS
        _rune_manager = FindObjectOfType<RuneManager>();

        _rune_manager.init();
        _player_manager.init(_main_camera);

        // INIT LEVEL SCENE COMPONENTS
        _camera_crusher.init(_player_manager);
        _enemy_spawner.init();
    }
}
