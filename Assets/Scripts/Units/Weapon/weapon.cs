using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] public float seconds;
    [SerializeField] public bool Is_Equipped;
    // Update is called once per frame
    void Start()
    {
        firePoint.position = firePoint.position + new Vector3(2, 0, 0);
        InvokeRepeating("Shoot",1.0f, seconds);
    }

    public abstract void Shoot();
}