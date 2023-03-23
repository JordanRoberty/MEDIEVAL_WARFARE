using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] public float shot_speed;
    [SerializeField] public bool Is_Equipped;

    // Update is called once per frame
    void Start()
    {
        firePoint.position = firePoint.position + new Vector3(2, 0, 0);
        shot_speed *= RuneManager.Instance.firing_rate_rune;
        InvokeRepeating("Shoot",0f, shot_speed);
    }

    public abstract void Shoot();
}