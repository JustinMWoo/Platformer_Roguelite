using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float baseSpeed = 10f;
    float speedMulti;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.right * baseSpeed* speedMulti;
    }

    public void SetSpeedMulti(float value)
    {
        speedMulti = value;
    }
}
