using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    private bool preventMovement;
    private bool dashing;

    // Update is called once per frame
    void Update()
    {
        if (!preventMovement && !dashing)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        if (Input.GetButtonDown("Jump") && !preventMovement)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (!preventMovement)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    public void SetVelocityXForTime(float newVel, float sec)
    {
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction == 0)
        {
            // If no direction is being pressed, change direction to the one being faced
            direction = transform.right.x;
        }

        horizontalMove = newVel * direction;

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

    public IEnumerator LoseControl(float sec)
    {
        preventMovement = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        animator.SetFloat("Speed", 0f);
        yield return new WaitForSeconds(sec);
        preventMovement = false;
    }
}
