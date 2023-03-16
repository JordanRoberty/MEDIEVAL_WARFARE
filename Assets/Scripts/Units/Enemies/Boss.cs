using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Boss()
    {
        pv = 10000.0f;
        speed = 2.0f;
        damage = 50.0f;
    }

    private float bottle_range = 12.0f;
    private float flail_range = 9.0f;
    private GameObject player;
    public GameObject bottle_prefab;
    public float timer_before_action = 1.0f;
    public float time_in_air = 1.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            // on calcule la distance entre le boss et le joueur
            float distance_to_player = Vector3.Distance(
                transform.position,
                player.transform.position
            );

            // on est assez proche pour attaquer, on commence l'attaque
            if (distance_to_player <= flail_range)
            {
                if (timer_before_action > 0.0f)
                {
                    timer_before_action -= Time.deltaTime;
                }
                else
                {
                    timer_before_action = 3.0f;
                    Invoke("start_flail_attack", 0.0f);
                }
            }
            else if (distance_to_player <= bottle_range)
            {
                if (timer_before_action > 0.0f)
                {
                    timer_before_action -= Time.deltaTime;
                }
                else
                {
                    timer_before_action = 3.0f;
                    if (Random.value < 0.5f)
                    {
                        throw_bottle();
                    }
                    else
                    {
                        jump_on_player();
                    }
                }
            }
            else
            {
                // on n'est pas assez proche pour attaquer, on se déplace vers le joueur
                Vector3 direction_to_player = (
                    player.transform.position - transform.position
                ).normalized;
                direction_to_player = new Vector3(direction_to_player.x, 0.0f, 0.0f);
                transform.position += direction_to_player * speed * Time.deltaTime;
            }
        }
    }

    void start_flail_attack()
    {
        GameObject arm = GameObject.Find("FirePoint");

        Quaternion startRotation = arm.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 0f, 360f);
        float duration = 2f;

        StartCoroutine(RotateObject(arm, startRotation, endRotation, duration));
    }

    IEnumerator RotateObject(
        GameObject obj,
        Quaternion startRotation,
        Quaternion endRotation,
        float duration
    )
    {
        // Vérifier que l'objet est toujours valide
        if (obj == null)
        {
            yield break;
        }

        float angle = 0f;
        float totalAngle = 360f; // L'angle total de rotation que l'on souhaite effectuer

        while (angle < totalAngle)
        {
            float deltaAngle = Time.deltaTime * 360f / duration; // La quantité de rotation à appliquer à chaque frame
            angle += deltaAngle;

            // Faire la rotation relative à la rotation actuelle de l'objet
            obj.transform.Rotate(0f, 0f, deltaAngle, Space.Self);
            yield return null;
        }

        // Faire une dernière rotation pour être sûr d'être à la fin de la rotation
        obj.transform.rotation = endRotation;
    }

    void throw_bottle()
    {
        // On récupère la position actuelle du joueur et du boss
        Vector2 player_position = player.transform.position;
        Vector2 boss_position = transform.position;

        // On calcule la direction et la distance entre le boss et le joueur
        Vector2 direction_to_player = player_position - boss_position;
        float distance_to_player = direction_to_player.magnitude;

        // On calcule la position de départ de la bouteille
        Vector2 bottle_start_position = boss_position + direction_to_player.normalized * 2.0f;

        // On instancie la bouteille à la position de départ
        GameObject bottle = Instantiate(bottle_prefab, bottle_start_position, Quaternion.identity);

        // On calcule la direction de lancement de la bouteille en ajoutant une petite variation
        Vector2 bottle_direction =
            direction_to_player.normalized
            + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

        // On applique une force au rigidbody de la bouteille pour la lancer
        Rigidbody2D bottle_rigidbody = bottle.GetComponent<Rigidbody2D>();
        bottle_rigidbody.AddForce(bottle_direction * 20.0f, ForceMode2D.Impulse);
    }

    void jump_on_player()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        // la position actuelle du personnage
        Vector3 currentPosition = transform.position;

        // la position cible de l'atterrissage
        Vector3 targetPosition = player.transform.position;

        // la gravité dans votre scène
        float gravity = Physics.gravity.y;

        // la vitesse de saut du personnage
        float jumpSpeed = 10000f; // changez cette valeur selon la vitesse de saut du personnage

        // la distance horizontale entre la position actuelle et la position cible
        float horizontalDistance = Mathf.Sqrt(
            Mathf.Pow(targetPosition.x - currentPosition.x, 2f)
                + Mathf.Pow(targetPosition.z - currentPosition.z, 2f)
        );

        // la distance verticale entre la position actuelle et la position cible
        float verticalDistance = targetPosition.y - currentPosition.y;

        // le temps nécessaire pour atteindre la hauteur maximale du saut
        float timeToReachMaxHeight = jumpSpeed / Mathf.Abs(gravity);

        // la hauteur maximale atteinte pendant le saut
        float maxJumpHeight =
            jumpSpeed * timeToReachMaxHeight + 0.5f * gravity * Mathf.Pow(timeToReachMaxHeight, 2f);

        // le temps nécessaire pour atterrir en utilisant une formule de mouvement uniformément accéléré
        float timeToLand =
            Mathf.Sqrt(2f * (maxJumpHeight - verticalDistance) / -gravity) + timeToReachMaxHeight;

        // la vitesse horizontale nécessaire pour atterrir à la position cible
        float horizontalSpeed = horizontalDistance / timeToLand * 1000000;

        // la force nécessaire pour ajouter à votre personnage pour atterrir à la position cible
        Vector3 force = new Vector3(
            horizontalSpeed * (targetPosition.x - currentPosition.x) / horizontalDistance,
            jumpSpeed,
            0.0f
        );

        // appliquer la force à votre personnage
        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le joueur entre en collision avec le boss
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calcul de la direction de la poussée
            Vector3 position_dif = collision.transform.position - transform.position;
            Vector3 direction = new Vector3();
            if (position_dif.x > 0f)
            {
                direction = Vector3.right;
            }
            else
            {
                direction = Vector3.left;
            }

            // Application de la force de poussée
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (collision.transform.position.x > -8f)
            {
                collision.transform.position += direction * 3f;
            }
            playerRb.AddForce(direction * 100f, ForceMode2D.Impulse);

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = Vector2.zero;
        }

        if (collision.gameObject.name == "Ground")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
