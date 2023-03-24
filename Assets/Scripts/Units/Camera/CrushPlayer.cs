using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPlayer : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private float _raycast_length = 0.3f;
    [SerializeField] private LayerMask _player_layer;
    

    void FixedUpdate()
    {
        if(is_crushed() && GameManager.Instance._state == GameState.RUNNING)
        {
            _player.die();
        }
    }


    private bool is_crushed()
    {
        return Physics2D.Raycast(new Vector3(transform.position.x, _player.transform.position.y, transform.position.z), -Vector2.left, _raycast_length , _player_layer);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Vector3 position = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        {
            Gizmos.DrawLine(position, position  - Vector3.left * _raycast_length);
        }
    }
}
