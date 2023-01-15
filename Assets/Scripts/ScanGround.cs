using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanGround : MonoBehaviour
{
    void Start()
    {
        Invoke("scan_ground", 1.0f);
    }

    void scan_ground()
    {
        AstarPath.active.Scan();
    }
}
