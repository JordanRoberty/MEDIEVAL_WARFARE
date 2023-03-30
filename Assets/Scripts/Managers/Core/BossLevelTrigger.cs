using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelTrigger : MonoBehaviour
{
    [SerializeField] private BossLevel _boss_level_to_load;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerManager>(out PlayerManager _player))
        {
            _player.gameObject.SetActive(false);
            SceneController.Instance.load_boss_level(_boss_level_to_load);
        }
    }
}
