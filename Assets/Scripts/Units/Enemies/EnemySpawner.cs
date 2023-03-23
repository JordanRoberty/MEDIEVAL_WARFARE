using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy_basic;
    public GameObject enemy_tank; 
    public GameObject enemy_flying;

    public float spawn_rate = 1.0f; // taux de spawn en secondes
    public float enemy_basic_probability = 0.5f; // probabilité d'apparition de enemy_basic
    public float enemy_tank_probability = 0.2f; // probabilité d'apparition de enemy_tank
    public float enemy_flying_probability = 0.3f; // probabilité d'apparition de enemy_flying

    public float scroll_speed = 0.2f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0.0f, spawn_rate); // lancement du spawn toutes les "spawn_rate" secondes
    }

    void Update()
    {
        transform.position += Vector3.right * scroll_speed * Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        float random_number = Random.Range(0.0f, 1.0f); // tirage aléatoire entre 0 et 1

        if (random_number < enemy_basic_probability) // si le nombre tiré est inférieur à la probabilité d'apparition de enemy_basic
        {
            // on fait spawn enemy_basic
            Instantiate(enemy_basic, transform.position, Quaternion.identity);
        }
        else if (random_number < enemy_basic_probability + enemy_tank_probability) // si le nombre tiré est entre la probabilité d'apparition de enemy_basic et celle de enemy_basic + enemy_tank
        {
            // on fait spawn enemy_tank
            Instantiate(enemy_tank, transform.position, Quaternion.identity);
        }
        else // sinon, cela signifie que le nombre tiré est supérieur à la probabilité d'apparition de enemy_basic + enemy_tank, donc on fait spawn enemy_flying
        {
            Instantiate(enemy_flying, transform.position, Quaternion.identity);
        }
    }
}
