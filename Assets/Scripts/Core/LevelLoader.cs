using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelLoader : Singleton<LevelLoader>
{
    [SerializeField] protected Camera _main_camera;

    protected PlayerManager _player_manager;

    public abstract void init();
}
