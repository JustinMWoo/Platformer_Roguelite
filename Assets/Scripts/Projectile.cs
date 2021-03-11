using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed;

    //Vector2 lastPos;

    public Rigidbody2D rb;
    public int damage;

    public bool playerProjectile;

    public GameObject residualAOEPrefab;
    public int residualAOEDamage;
    public float residualAOEDuration;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        //lastPos = transform.position;
        rb.velocity = direction * speed;

        // If the object has not hit anything destroy it in 5 sec
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        //      transform.position += transform.right * speed * Time.deltaTime;

        //    int layerMask;

        //    if (playerProjectile)
        //    {
        //        layerMask = (LayerMask.GetMask("Enemy"));
        //    }
        //    else
        //    {
        //        layerMask = (LayerMask.GetMask("Player"));
        //    }

        //    //Cast a ray back to the previous position
        //    RaycastHit2D hit = Physics2D.Linecast(lastPos, transform.position, layerMask);

        //    if (hit.collider != null)
        //    {
        //        if (playerProjectile)
        //        {
        //            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
        //            if (enemy)
        //            {
        //                enemy.TakeDamage(damage);
        //            }
        //        }
        //        else
        //        {

        //        }

        //        //Debug.Log(hit.collider.gameObject.name);
        //        Destroy(gameObject);
        //    }
        //    lastPos = transform.position;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
    //Too slow for fast moving projectiles

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerProjectile)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);

        }
        else
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
                player.TakeDamage(transform, damage);
        }

        //Debug.Log(hit.collider.gameObject.name);
        if (!collision.isTrigger)
        {
            if (residualAOEPrefab != null)
            {
                GameObject AOE = Instantiate(residualAOEPrefab);
                AOE.transform.position = transform.position;
                ResidualAOE AOEScript = AOE.GetComponent<ResidualAOE>();
                AOEScript.damage = residualAOEDamage;
                AOEScript.duration = residualAOEDuration;
            }
            Destroy(gameObject);
        }
    }
}
