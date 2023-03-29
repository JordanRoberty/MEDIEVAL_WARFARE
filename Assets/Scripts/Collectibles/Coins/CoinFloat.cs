using UnityEngine;

public class CoinFloat : MonoBehaviour
{
    public float float_speed = 1.0f; // Controls the speed of the up-and-down motion
    public float float_amplitude = 0.3f; // Controls the height of the up-and-down motion

    private Vector3 initial_position;
    private float float_timer;
    private float time_offset;

    void Start()
    {
        initial_position = transform.position;
        float_timer = 0;

        // Add a random offset to the time, to get a slight desynchronization between the coins movement
        time_offset = Random.Range(0f, 2 * Mathf.PI);
    }

    void Update()
    {
        float_timer += Time.deltaTime * float_speed;
        transform.position =
            initial_position
            + new Vector3(0, Mathf.Sin(float_timer + time_offset) * float_amplitude, 0);
    }
}
