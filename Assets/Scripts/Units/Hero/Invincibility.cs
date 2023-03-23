using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    private float _invincibleTime;
    private Renderer _renderer;
    private Color _color;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
        _invincibleTime = GetComponent<PlayerData>().invincibleTime;
    }


    public void get_invulnerable(){
        StartCoroutine("invulnerable");
    }

//The layer 8 is the player, the 9th is the enemy
    IEnumerator invulnerable()
    {
        Physics2D.IgnoreLayerCollision(8,9,true);
        _color.a= 0.5f;
        _renderer.material.color= _color;

        yield return new WaitForSeconds(_invincibleTime);

        Physics2D.IgnoreLayerCollision(8,9,false);
        _color.a= 1f;
        _renderer.material.color= _color;

    }
}
