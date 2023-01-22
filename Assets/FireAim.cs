using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireAim : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        /*if (Input.GetKeyDown("up"))
        {
            gameObject.transform.Translate(0.0f,0.5f,0,Space.Self);
            gameObject.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
        }

        if (Input.GetKeyUp("up"))
        {
            gameObject.transform.Translate(0.0f, -0.5f, 0, Space.Self);
            gameObject.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
        }*/

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = mousePos - transform.position;
        float rotZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

    }
}
