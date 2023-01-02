using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] public float seconds;
    [SerializeField] public bool Is_Equipped;
    // Update is called once per frame
    void Start()
    {   
        InvokeRepeating("Shoot",1.0f, seconds);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}