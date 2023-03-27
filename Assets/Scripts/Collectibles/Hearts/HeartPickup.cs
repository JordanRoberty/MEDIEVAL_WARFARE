using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;

    public int heart_value = 1; // The value of the heart

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Player moves onto the Coin
        if (collision.CompareTag("Player"))
        //if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.destroy();
            
            if (_player.health < _player.max_health)
            {
                _player.health += heart_value;
            }
        }
    }
}
