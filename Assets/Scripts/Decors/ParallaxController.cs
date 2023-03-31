using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float lenght;
    private float start_pos;
    private GameObject cam;
    [SerializeField] private float parallax_effect;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        start_pos = transform.position.x;
        lenght = GetComponent<MeshRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallax_effect));
        float distance = (cam.transform.position.x * parallax_effect);
        
        transform.position = new Vector3(start_pos + distance, transform.position.y, transform.position.z);    

        if(temp > start_pos + lenght){
            start_pos += lenght;
        }
    }
}
