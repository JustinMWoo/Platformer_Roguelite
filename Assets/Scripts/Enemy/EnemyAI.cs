using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target;

    public float speed = 200f;
    public float jumpForce = 300f;
    public float nextWaypointDistance = 3f;
    //public float stopDistance = 5f;

    public Transform enemyGFX;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.

    private bool patrolling = true;
    private bool facingRight = true;
    private bool init = false;
    public Transform patrolGroundDetection;
    public float patrolSpeed = 200f;

    public EnemyVisionDetection visionDetection;

    private bool pathFinding;

    [SerializeField] private float attackRange = 1;
    [SerializeField] private LayerMask playerUnitLayer;
    private bool attacking = false;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (patrolling)
        {
            CheckVision();
            Patrol();
        }
        else if (pathFinding)
        {
            CheckRange();
            PathFind();
        }
        else if (attacking)
        {
            CheckRange();
            Attack();
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

        if (isGrounded && hit.collider != null && hit.collider.CompareTag("Terrain")) //TODO: Or if enemy reaches end of platform and the player is above?
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // If the distance to the player is greater than the stop distance then keep moving the enemy (Replaced by attackrange)
        //if (path.GetTotalLength() > stopDistance)
        //{
        rb.AddForce(force);
        //}

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        // TODO: sometimes flips direction for a frame (rb.velocity is another way to do this, but will face direction it is moving)
        //if (!facingRight && dir.x > 0)
        //{
        //    Flip();
        //}
        //else if (facingRight && dir.x < 0)
        //{
        //    Flip();
        //}
        if (target.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (target.transform.position.x < transform.position.x && facingRight)
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

        facingRight = !facingRight;

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
    private void Attack()
    {
        // Face player
        if (target.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (target.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }

        // Class for enemy attacks (similar to triggerable) and then call attacktype.attack and attacktype holds the code for animation, attacking and dealing damage
    }
    private void CheckRange()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, attackRange, playerUnitLayer);

        if (collider != null)
        {
            attacking = true;
            pathFinding = false;
        }
        else
        {
            pathFinding = true;
            attacking = false;
        }
    }

}
