using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    public EnemyFlying()
    {
        pv = 100.0f;
        speed = 5.0f;
        max_droppable_quantity = 7;
        score_value = 2;
    }

    // Amplitude de la sinusoïde
    public float amplitude = 4.0f;

    // Fréquence de la sinusoïde
    public float frequency = 5.0f;

    public LayerMask _edge_layer;

    // Temps écoulé depuis le début du mouvement
    private float time = 0.0f;
    public AudioClip death_sound;

    private void Start()
    {
        switch (DifficultyManager.Instance.current_difficulty)
        {
            case 0:
                pv = 1500f;
                break;
            case 1:
                pv = 2000f;
                break;
            case 2:
                pv = 2500f;
                break;
            default:
                pv = 3500f;
                break;
        }
    }

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, _edge_layer);

        if (collider != null)
        {
            Destroy(gameObject);
        }
        // Déplacement du personnage sur le côté
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Incrémentation du temps
        time += Time.deltaTime;

        // Calcul de la nouvelle position horizontale
        float y = amplitude * Mathf.Sin(time * frequency) + 0.25f;

        // Déplacement du personnage vers la nouvelle position
        transform.position = new Vector2(transform.position.x, y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet en collision a le tag spécifié
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            // Désactiver la collision avec l'objet ayant le tag spécifié
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider, true);
        }
    }

    private void OnDestroy()
    {
        AudioSystem.Instance.play_sound(death_sound, 0.8f);
    }
}
