using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
