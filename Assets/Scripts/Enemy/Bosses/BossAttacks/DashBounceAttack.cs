using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBounceAttack : BossAttack
{
    private LineRenderer lineRenderer;
    private Transform bossTrans;
    private Rigidbody2D rb;
    private Transform playerTrans;
    private int bounces = 5;

    private Ray2D ray;
    private RaycastHit2D[] hits;

    private const int MAX_LENGTH = 50;

    private bool start = true;
    private bool tracking = true;
    private bool attack = false;

    Gradient attackGradient;
    Gradient startGradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKeyTransparent;
    GradientAlphaKey[] alphaKeySolid;

    private float gravity;

    private int damage = 10;

    //public DashBounceAttack(LineRenderer lr, Transform bossTransform, Transform playerTransform)
    //{
    //    lineRenderer = lr;
    //    //lineRenderer.enabled = false;
    //    lineRenderer.useWorldSpace = true;

    //    bossTrans = bossTransform;
    //    playerTrans = playerTransform;
    //}

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        bossTrans = transform;

        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;

        alphaKeyTransparent = new GradientAlphaKey[1];
        alphaKeyTransparent[0].alpha = 0.4f;

        alphaKeySolid = new GradientAlphaKey[1];
        alphaKeySolid[0].alpha = 1.0f;

        startGradient = lineRenderer.colorGradient;
        attackGradient = new Gradient();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void Execute()
    {
        if (start)
        {
            playerTrans = GetComponent<SwordBoss>().Player;
            StartCoroutine(PhaseTimer());
            start = false;
        }
        if (tracking)
            DrawPath();
        else if (attack)
        {
            StartCoroutine(Attack());
        }
    }

    private void DrawPath()
    {
        // Make line transparent
        colorKey[0].color = Color.yellow;
        attackGradient.SetKeys(colorKey, alphaKeyTransparent);
        lineRenderer.colorGradient = attackGradient;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, bossTrans.position);
        lineRenderer.positionCount = 1;

        ray = new Ray2D(bossTrans.position, playerTrans.position - bossTrans.position);

        for (int i = 0; i < bounces; i++)
        {
            // TODO: Add layermask to improve performance!!
            hits = Physics2D.RaycastAll(ray.origin, ray.direction, MAX_LENGTH);
            bool bounce = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                    ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
                    bounce = true;
                    break;
                }
            }
            if (!bounce)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * MAX_LENGTH);
            }
        }
    }


    IEnumerator Attack()
    {
        attack = false;
        float totalLength = 0;
        List<Vector3> points = new List<Vector3>();
        rb.isKinematic = true;
        //gravity = rb.gravityScale;
        while (lineRenderer.positionCount > 1)
        {
            //rb.gravityScale = 0;
            for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            {
                totalLength += Vector2.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
            }

            float segLength = Vector2.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));

            // Change begginning color of line
            colorKey[0].color = Color.red;
            colorKey[1].color = Color.yellow;
            colorKey[1].time = segLength / totalLength;

            //colorKey[2].color = Color.yellow;
            //colorKey[2].time = (segLength / totalLength) + 0.0001f;
            attackGradient.SetKeys(colorKey, alphaKeySolid);
            lineRenderer.colorGradient = attackGradient;

            yield return new WaitForSeconds(1);

            CameraShake.Current.ShakeCamera(5f, 0.1f);
            points.Clear();

            // Check if player hit
            // Debug.DrawRay(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0), Color.magenta, 1);
            RaycastHit2D hit = Physics2D.Raycast(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0), segLength, 1 << LayerMask.NameToLayer("Player"));
            if (hit.collider != null)
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(bossTrans, damage);
            }

            // Remove first line segment
            for (int i = 1; i < lineRenderer.positionCount; i++)
            {
                points.Add(lineRenderer.GetPosition(i));
            }
            lineRenderer.SetPositions(points.ToArray());

            // Move boss
            bossTrans.position = lineRenderer.GetPosition(0);
            lineRenderer.positionCount -= 1;
        }
        rb.isKinematic = false;
        //rb.gravityScale = gravity;

        yield return new WaitForSeconds(3);
        Done();
    }

    protected override void Done()
    {
        base.Done();
        start = true;
        tracking = true;
        attack = false;
    }

    IEnumerator PhaseTimer()
    {
        yield return new WaitForSeconds(3);
        tracking = false;
        attack = true;
    }
}
