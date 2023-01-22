using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData player;
    [SerializeField] private float _raycast_length = 0.3f;
    [SerializeField] private LayerMask _player_layer;

    private bool is_crush = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.transform.position);
        is_crushed();
        if(is_crush){
            player.health = 0;
        }
    }


    private void is_crushed()
    {
        is_crush = Physics2D.Raycast(new Vector3(transform.position.x, player.transform.position.y, transform.position.z), -Vector2.left, _raycast_length , _player_layer);
        //Debug.Log(Physics2D.Raycast(new Vector2(transform.position.x, player.transform.position.y, transform.position.z), -Vector2.left, _raycast_length , _player_layer) );
        
        
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Vector3 position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        {
            Gizmos.DrawLine(position, position  - Vector3.left * _raycast_length);
 
        }
         

    }
}
