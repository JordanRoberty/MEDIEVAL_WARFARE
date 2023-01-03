using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform subject;

    Vector2 start_position;
    float start_z;

    Vector2 travel => (Vector2)cam.transform.position - start_position;

    float distance_from_subject => transform.position.z - subject.position.z;
    float clipping_plane => (cam.transform.position.z + (distance_from_subject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallax_factor => Mathf.Abs(distance_from_subject) / clipping_plane;

    // Start is called before the first frame update
    private void Start()
    {
        start_position = transform.position;
        start_z = transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 new_position = start_position + travel;
        transform.position = new Vector3(new_position.x, new_position.y, start_z);

        Vector2 new_texture_offset = -(travel * parallax_factor * 0.1f);
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(new_texture_offset.x, new_texture_offset.y));
    }
}
