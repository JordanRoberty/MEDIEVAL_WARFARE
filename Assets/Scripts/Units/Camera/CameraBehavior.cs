using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        string sceneName = SceneManager.GetActiveScene().name;
         if (sceneName.Contains("boss_level_")){
            speed = 0f;
         }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0f);

    }
}
