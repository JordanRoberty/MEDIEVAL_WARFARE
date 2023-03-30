using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCrusher : MonoBehaviour
{
    [SerializeField] private float _raycast_length = 0.3f;
    [SerializeField] private LayerMask _player_layer;
    [SerializeField] private PlayerManager _player_manager;

    private bool initialized;
    
    private void Awake()
    {
        initialized = false;
    }

    public void init(PlayerManager player_manager)
    {
        _player_manager = player_manager;
        initialized = true;
    }

    private void FixedUpdate()
    {
        if(initialized && is_crushed() && GameManager.Instance._state == GameState.RUNNING)
        {
            _player_manager.die();
        }
    }


    private bool is_crushed()
    {
        return Physics2D.Raycast(new Vector3(transform.position.x, _player_manager.transform.position.y - 0.5f, transform.position.z), -Vector2.left, _raycast_length , _player_layer);
    }

    private void OnDrawGizmos() 
    {
        if(initialized)
        {
            Gizmos.color = Color.red;
            Vector3 position = new Vector3(transform.position.x, _player_manager.transform.position.y - 0.5f, transform.position.z);
            {
                Gizmos.DrawLine(position, position - Vector3.left * _raycast_length);
            }
        }
    }
}
