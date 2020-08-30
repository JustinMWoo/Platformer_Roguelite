using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.right * speed;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
}
