using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    public EnemyFlying()
    {
        pv = 100.0f;
        speed = 5.0f;
        damage = 80.0f;
        max_droppable_quantity = 7;
    }

    // Amplitude de la sinusoïde
    public float amplitude = 4.0f;

    // Fréquence de la sinusoïde
    public float frequency = 5.0f;

    public LayerMask _edge_layer;

    // Temps écoulé depuis le début du mouvement
    private float time = 0.0f;

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
}
