using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float jumpForce = 300f;
    public float nextWaypointDistance = 3f;
    public float stopDistance = 5f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.

    bool patrolling = true;
    bool movingRight = true;
    bool init = false;
    public Transform patrolGroundDetection;
    public float patrolSpeed = 200f;

    public EnemyVisionDetection visionDetection;

    bool pathFinding;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        CheckVision();

        if (patrolling)
        {
            Patrol();
        }
        else if (pathFinding)
        {
            PathFind();
        }
    }

    private void Patrol()
    {
        // divide by 100 to try to normalize between speeds
        transform.Translate(Vector2.right * patrolSpeed / 100 * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(patrolGroundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            Flip();
        }
    }

    private void PathFind()
    {
        if (path == null)
            return;

        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        // if current is greater than the total number of waypoints in the path
        // Not being used atm but maybe make the end of the path attack
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }
        else
        {
            reachEndOfPath = false;
        }

        Vector2 dir = new Vector2(((Vector2)path.vectorPath[currentWaypoint] - rb.position).x, 0f).normalized;

        // Remove the y component for grounded enemies
        Vector2 force = new Vector2(dir.x * speed * Time.deltaTime, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, transform.right, 2f);

        if (isGrounded && hit.collider != null && hit.collider.CompareTag("Terrain")) //TODO: Or if enemy reaches end of platform and the player is above
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // If the distance to the player is greater than the stop distance then keep moving the enemy
        if (path.GetTotalLength() > stopDistance)
        {
            rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        // Flip the transform without using Flip
        // TODO: maybe figure out why flip doenst work for this
        if (!movingRight && rb.velocity.x > 0)
        {
            Flip();
        }
        else if (movingRight && rb.velocity.x < 0)
        {
            Flip();
        }
    }

    private void CheckVision()
    {
        target = visionDetection.PlayerInSight();
        if (target != null)
        {
            patrolling = false;
            pathFinding = true;
            if (!init)
            {
                InvokeRepeating("UpdatePath", 0f, .5f);
                init = true;
            }
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);

        movingRight = !movingRight;

        //if (movingRight == true)
        //{
        //    transform.eulerAngles = new Vector3(0, -180, 0);
        //    movingRight = false;
        //}
        //else
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //    movingRight = true;
        //}
    }
}
