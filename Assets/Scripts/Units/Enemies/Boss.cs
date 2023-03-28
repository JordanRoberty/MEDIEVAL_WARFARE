using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Boss()
    {
        pv = 10000.0f;
        speed = 2.0f;
        score_value = 1000;
    }

    public GameObject bottle_prefab;
    public float timer_before_action = 1.0f;
    public float time_in_air = 1.0f;
    public LayerMask wall;
    public LayerMask ground;
    public Animator animator;

    private float bottle_range = 12.0f;
    private float flail_range = 9.0f;
    private Transform _player;
    private bool is_facing = true;
    private Vector3 look_at = Vector3.left;
    private float moveSpeedThreshold = 0.1f;

    public void init(Transform player)
    {
        _player = player;
    }

    private void Update()
    {
        if (_player != null)
        {
            is_facing = transform.position.x > _player.position.x ? false : true;
            transform.rotation = Quaternion.Euler(0f, is_facing ? 180f : 0f, 0f);
            if (transform.rotation.eulerAngles.y == 180f)
            {
                look_at = Vector3.right;
            }
            else
            {
                look_at = Vector3.left;
            }

            // on calcule la distance entre le boss et le joueur
            float distance_to_player = Vector3.Distance(
                transform.position,
                _player.position
            );

            if (timer_before_action > 0.0f)
            {
                timer_before_action -= Time.deltaTime;
            }

            if (
                Physics2D.Raycast(transform.position, look_at, 8f, wall)
                && Physics2D.Raycast(transform.position, Vector3.down, 4f, ground)
                && timer_before_action < 0.5f
            )
            {
                timer_before_action = Random.Range(2f, 4f);

                GetComponent<Rigidbody2D>()
                    .AddForce(new Vector3(6000f * -look_at.x, 8000f, 0f), ForceMode2D.Impulse);
            }

            // on est assez proche pour attaquer, on commence l'attaque
            if (distance_to_player <= flail_range && timer_before_action < 0.0f)
            {
                timer_before_action = Random.Range(2f, 4f);
                if (Random.value < 0.5f)
                {
                    animator.SetBool("Horizontal_attack", true);
                    StartCoroutine(DisableHorizontalAttackAfterAnimation());
                }
                else
                {
                    animator.SetBool("Vertical_attack", true);
                    StartCoroutine(DisableVerticalAttack());
                }
            }

            if (distance_to_player <= bottle_range && timer_before_action< 0.0f)
            {
                animator.SetBool("Going_forward", false);
                timer_before_action = Random.Range(2f, 4f);
                if (Random.value< 0.5f)
                {
                    throw_bottle();
            }
                else
            {
                jump_on_player();
                animator.SetBool("IsAiborn", false);
            }
            }
            else if (distance_to_player > bottle_range)
            {
                // On n'est pas assez proche pour attaquer, on se déplace vers le joueur
                Vector3 direction_to_player = (_player.position - transform.position).normalized;
                direction_to_player = new Vector3(direction_to_player.x, 0.0f, 0.0f);

                if (direction_to_player.magnitude > moveSpeedThreshold)
                {
                    // Le personnage est en train de se déplacer, on met à jour le booléen Going_forward
                    animator.SetBool("Going_forward", true);

                    transform.position += direction_to_player * speed * Time.deltaTime;
                    if (distance_to_player <= bottle_range)
                    {
                        animator.SetBool("Going_forward", false);
                    }
                    if (Physics2D.Raycast(transform.position, Vector3.down, 2.5f, ground))
                    {
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        animator.SetBool("Going_forward", false);
                    }
                }
                else
                {
                    // Le personnage est immobile, on met à jour le booléen Going_forward
                    animator.SetBool("Going_forward", false);
                }
            }

        }
    }

    void throw_bottle()
    {
        animator.SetBool("Bottle_toss", true);
        StartCoroutine(ThrowBottle());
    }

    void jump_on_player()
    {
        animator.SetBool("IsAirborn", true);
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        // la position actuelle du personnage
        Vector3 currentPosition = transform.position;

        // la position cible de l'atterrissage
        Vector3 targetPosition = _player.position;

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
            2 * horizontalSpeed * (targetPosition.x - currentPosition.x) / horizontalDistance,
            jumpSpeed,
            0.0f
        );

        // appliquer la force à votre personnage
        rigidbody.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(Squash(timeToReachMaxHeight / 1000f));
    }

    public override void die()
    {
        StatsManager.Instance.update_score(score_value);

        int coin_reward = 100;

        // Spawn the correct number of coin(s) at the enemy's position
        for (int i = 0; i < coin_reward; i++)
        {
            Transform coin_parent = GameObject.Find("/Environment/Coins").transform;

            // Add a random offset to the coin's position
            GameObject coin = Instantiate(
                coinPrefab,
                transform.position + new Vector3(Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f), 0),
                Quaternion.identity,
                coin_parent
            );
        }

        BossFightManager.Instance.boss_died();

        // Destroy the enemy
        transform.destroy();
    }

    IEnumerator Squash(float timeToReachMaxHeight)
    {
        yield return new WaitForSeconds(timeToReachMaxHeight);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.down * 50000f, ForceMode2D.Impulse); // Ajouter une force vers le bas
        }
        animator.SetBool("IsAirborn", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le joueur entre en collision avec le boss
        if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager player_manager))
        {
            // Calcul de la direction de la poussée
            Vector3 position_dif = player_manager.transform.position - transform.position;
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
            Rigidbody2D _playerRb = player_manager.transform.GetComponent<Rigidbody2D>();
            if (collision.transform.position.x > -8f && collision.transform.position.x < 3f)
            {
                collision.transform.position += direction * 3f;
            }
            _playerRb.AddForce(direction * 2f, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.name == "Ground")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    IEnumerator DisableHorizontalAttackAfterAnimation()
    {
        // Attendre la fin de l'animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Désactiver le paramètre "Horizontal_attack"
        animator.SetBool("Horizontal_attack", false);
    }

    IEnumerator ThrowBottle()
    {
        // Attendre la fin de l'animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // On récupère la position actuelle du joueur et du boss
        Vector2 player_position = _player.position;
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
        bottle_rigidbody.AddForce(bottle_direction * 10f, ForceMode2D.Impulse);
        animator.SetBool("Bottle_toss", false);
    }

    IEnumerator DisableVerticalAttack()
    {
        // Attendre la fin de l'animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Désactiver le paramètre "Horizontal_attack"
        animator.SetBool("Vertical_attack", false);
    }
    IEnumerator DisableWalk()
    {
        // Attendre la fin de l'animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Désactiver le paramètre "Horizontal_attack"
        animator.SetBool("Going_forward", false);
    }
}
