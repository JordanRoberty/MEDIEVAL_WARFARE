using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private PlayerManager _player;

    public int heart_value = 1; // The value of the heart

    void Start()
    {
        // Find the PlayerManager attached to the Player GameObject
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Player moves onto the Coin
        if (collision.CompareTag("Player"))
        {
            if (_player.health < _player.max_health)
            {
                _player.health += heart_value;
            }
            transform.parent.destroy();
        }
    }
}
