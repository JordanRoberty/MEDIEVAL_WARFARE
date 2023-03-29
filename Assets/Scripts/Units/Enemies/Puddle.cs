using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : Enemy
{
    public Puddle()
    {
        pv = 5000.0f;
        speed = 0.0f;
    }

    float life_time = 3f;
    public AudioClip puddle_sound;

    private void Start()
    {
        AudioSystem.Instance.play_sound(puddle_sound, 2f);
    }

    private void Update()
    {
        life_time -= Time.deltaTime;
        if (life_time <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
