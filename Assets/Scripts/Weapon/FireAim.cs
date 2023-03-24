using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.GameFoundation;
using UnityEngine.Assertions;
using UnityEngine.GameFoundation.Components;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FireAim : MonoBehaviour
{
    public GameObject Player;
    private Vector3 mousePos;
    private Camera mainCam;
    private float shot_freq;
    private GameObject bullet_prefab;
    private SpriteRenderer renderer;
    private GameObject bulletContainer;

    void Start()
    {
        bulletContainer = new GameObject("BulletContainer");
        renderer = gameObject.GetComponent<SpriteRenderer>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        InventoryItem weapon = PlayerInfosManager.Instance.equiped_weapon;
        AsyncOperationHandle<Sprite> load_sprite = weapon.definition.GetStaticProperty("sprite").AsAddressable<Sprite>();
        load_sprite.Completed += (AsyncOperationHandle<Sprite> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = handle.Result;
                Assert.IsNotNull(sprite);
                renderer.sprite = sprite;
            }
        };

        shot_freq = weapon.definition.GetStaticProperty("shot_freq");

        AsyncOperationHandle<GameObject> load_bullet = weapon.definition.GetStaticProperty("bullet_prefab").AsAddressable<GameObject>();
        load_bullet.Completed += (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject bullet = handle.Result;
                Assert.IsNotNull(bullet);
                bullet_prefab = bullet;
                InvokeRepeating("Shoot", 0f, shot_freq);
            }
        };

        
    }

    void Update()
    {
        Cursor.visible = false;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = mousePos - Player.transform.position;
        float rotZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet_prefab, gameObject.transform.position, gameObject.transform.rotation);
        bulletInstance.transform.parent = bulletContainer.transform;
    }
}
