using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string area_to_load;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerManager>(out PlayerManager _player))
        {
            SceneManager.LoadScene(area_to_load);
        }
    }
}
