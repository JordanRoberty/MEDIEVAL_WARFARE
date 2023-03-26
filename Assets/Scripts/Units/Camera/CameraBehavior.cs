using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody _rigid_body;
    
    private void Awake()
    {
        _rigid_body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigid_body.velocity = new Vector2(speed, 0f);
    }
}
