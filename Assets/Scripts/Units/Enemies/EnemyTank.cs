using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// réduire sa vitesse pour le rendre plus lourd, fix les gigas sauts aléatoires (empêcher de cumuler les forces de saut (ne pas sauter pendant qu'il est en train de sauter))

public class EnemyTank : Enemy
{
    public EnemyTank()
    {
        pv = 1000.0f;
        speed = 100.0f;
        max_droppable_quantity = 10;
        score_value = 3;
    }

    [Header("Pathfinding")]
    private Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;
    public LayerMask _edge_layer;

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private Vector3 last_position;
    private float timer = 1.0f;
    private float jump_cooldown = 0.0f;

    public void Start()
    {
        target = GameObject.Find("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, _edge_layer);

        if (collider != null)
        {
            Destroy(gameObject);
        }
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
        if (Vector3.Distance(last_position, transform.position) < 0.001f)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                transform.position = new Vector3(
                    transform.position.x - 0.5f,
                    transform.position.y,
                    transform.position.z
                );
                // rb.AddForce(new Vector2(500.0f, 0.0f));
                timer = 1.0f;
            }
        }
        else
        {
            timer = 1.0f;
        }
        last_position = transform.position;
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        Vector3 startOffset =
            transform.position
            - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        jump_cooldown -= Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded && jump_cooldown <= 0.0f)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                Vector2 jump_force = Vector2.up * speed * jumpModifier;
                rb.AddForce(jump_force);
                jump_cooldown = 1.0f;
            }
        }

        // Movement
        // force = new Vector2(force.x, 0.0f); // Mathf.Clamp(force.y, 0f, 10.0f)
        // Debug.Log(force);
        rb.AddForce(force);
        Debug.DrawRay(transform.position, force, Color.yellow);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(
                    -1f * Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet en collision a le tag spécifié
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Désactiver la collision avec l'objet ayant le tag spécifié
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider, true);
        }
    }
}
