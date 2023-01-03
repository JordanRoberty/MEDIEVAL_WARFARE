using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAime : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            gameObject.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        }
        if (Input.GetKey("up") && Input.GetKey("right"))
        {
            gameObject.transform.Rotate(0.0f, 45.0f, 0.0f, Space.Self);
        }
    }
}
