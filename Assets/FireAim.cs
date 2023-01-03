using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAim : MonoBehaviour
{
   void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            gameObject.transform.Translate(0.0f,0.5f,0,Space.Self);
            gameObject.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);            
        }

        gameObject.transform.Translate(0.0f,0.0f,0,Space.Self);
        gameObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);  

    }
}
