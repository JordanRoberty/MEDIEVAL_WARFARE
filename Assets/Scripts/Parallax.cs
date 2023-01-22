using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform subject;

    Vector3 start_position;

    float travel => cam.transform.position.x - start_position.x;

    float distance_from_subject => transform.position.z - subject.position.z;
    float clipping_plane => (cam.transform.position.z + (distance_from_subject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallax_factor => (clipping_plane - Mathf.Abs(distance_from_subject)) / clipping_plane;

    // Start is called before the first frame update
    private void Start()
    {
        start_position = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(start_position.x + travel, start_position.y, start_position.z);

        float new_texture_offset = -(travel * parallax_factor) * 0.01f;
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(new_texture_offset, 0));
    }
}
