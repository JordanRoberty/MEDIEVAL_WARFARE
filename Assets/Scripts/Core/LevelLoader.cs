using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelLoader : Singleton<LevelLoader>
{
    [SerializeField] protected Camera _main_camera;
    [SerializeField] protected Transform _initial_player;

    protected PlayerManager _player_manager;

    public virtual void init()
    {
        _player_manager = FindObjectOfType<PlayerManager>();
        _player_manager.transform.position = _initial_player.position;
    }
}
