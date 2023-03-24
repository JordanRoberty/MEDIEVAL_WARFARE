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
    private weapon current_weapon;
    private SpriteRenderer renderer;

    void Start()
    {
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

        AsyncOperationHandle<GameObject> load_bullet = weapon.definition.GetStaticProperty("bullet_prefab").AsAddressable<GameObject>();
        double shot_freq = weapon.definition.GetStaticProperty("shot_freq").AsDouble();
        Debug.Log(weapon.definition.GetStaticProperty("bullet_prefab").AsAddressable<GameObject>());

        current_weapon.bulletPrefab = load_bullet.Result;
    }

    void Update()
    {
        Cursor.visible = false;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = mousePos - Player.transform.position;
        float rotZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    IEnumerator Shoot()
    {
        Instantiate(current_weapon.bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(((float)current_weapon.shot_freq));
    }
}
