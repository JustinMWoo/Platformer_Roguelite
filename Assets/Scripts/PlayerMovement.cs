using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    private bool dashing;

    // Update is called once per frame
    void Update()
    {
        if (!dashing)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void SetVelocityXForTime(float newVel, float sec)
    {
        horizontalMove = newVel;

        StartCoroutine(DashForSec(sec));
    }

    IEnumerator DashForSec(float sec)
    {
        dashing = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x,0f);
        rb.angularVelocity = 0f;

        yield return new WaitForSeconds(sec);

        rb.gravityScale = gravity;
        dashing = false;
    }
}
