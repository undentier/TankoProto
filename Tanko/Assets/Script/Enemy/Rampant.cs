using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampant : MonoBehaviour
{
    public float speed;
    public float rayDistance;

    public Transform[] raycastStarts;
    public Rigidbody2D rampantRb;
    public LayerMask groundLayer;

    private Vector2 dir;
    private RaycastHit2D hit;

    private void Start()
    {
        dir = Vector2.right;
    }

    void Update()
    {
        HitDetection();
        Mouvement();
    }

    void Mouvement()
    {
        rampantRb.velocity = dir * speed;
    }

    void HitDetection()
    {
        for (int i = 0; i < raycastStarts.Length; i++)
        {
            hit = Physics2D.Raycast(raycastStarts[i].position, raycastStarts[i].right, rayDistance, groundLayer);

            if (hit.collider != null)
            {
                dir = -dir;
                Debug.DrawRay(raycastStarts[i].position, raycastStarts[i].right * rayDistance, Color.green);
            }
            else
            {
                Debug.DrawRay(raycastStarts[i].position, raycastStarts[i].right * rayDistance, Color.red);
            }
        }
    }
}
