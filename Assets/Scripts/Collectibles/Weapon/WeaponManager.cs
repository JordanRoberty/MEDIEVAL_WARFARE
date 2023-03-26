using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.GameFoundation;
using UnityEngine.Assertions;
using UnityEngine.GameFoundation.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

public class WeaponManager : MonoBehaviour
{
    private Camera _main_cam;
    private Vector3 mouse_position;

    private InventoryItem _weapon_infos;
    private GameObject _weapon_prefab;

    private Weapon _current_weapon;
    private float shot_freq;
    private GameObject bullet_prefab;
    [SerializeField] private Transform _bullet_container;

    public void init(Camera main_cam)
    {
        _main_cam = main_cam;
        _weapon_infos = PlayerInfosManager.Instance.equiped_weapon;
        shot_freq = (float)_weapon_infos.definition.GetStaticProperty("firing_rate");
        shot_freq *= RuneManager.Instance.firing_rate_rune;

        load_weapon();
    }

    private void load_weapon()
    {
        AsyncOperationHandle<GameObject> load_weapon_prefab = _weapon_infos.definition.GetStaticProperty("weapon_prefab").AsAddressable<GameObject>();
        load_weapon_prefab.Completed += (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Assert.IsNotNull(handle.Result);
                _weapon_prefab = handle.Result;

                _current_weapon = Instantiate(
                    _weapon_prefab,
                    transform.position,
                    Quaternion.identity,
                    transform
                ).GetComponent<Weapon>();

                Assert.IsNotNull(_current_weapon);

                load_bullet();
            }
        };
    }

    private void load_bullet()
    {
        AsyncOperationHandle<GameObject> load_bullet_prefab = _weapon_infos.definition.GetStaticProperty("bullet_prefab").AsAddressable<GameObject>();
        load_bullet_prefab.Completed += (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Assert.IsNotNull(handle.Result);
                bullet_prefab = handle.Result;
                init_shoot();
            }
        };
    }

    private void init_shoot()
    {
        InvokeRepeating("Shoot", 0f, shot_freq);
    }

    void Update()
    {
        if(GameManager.Instance._state == GameState.RUNNING)
        {
            mouse_position = _main_cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 aim_direction = mouse_position - transform.position;
            float shoulder_rotation = Mathf.Atan2(aim_direction.y, aim_direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, shoulder_rotation);
        }
    }

    void Shoot()
    {
        GameObject bullet =Instantiate(
            bullet_prefab,
            _current_weapon.cannon_end.position,
            _current_weapon.cannon_end.rotation,
            _bullet_container
        );

        bullet.transform.localScale *= RuneManager.Instance.projectile_size_rune;
    }
}
