using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampant : MonoBehaviour
{
    [Header ("Stats")]
    public float speed;
    public float explosionRadius;
    public float explosionForce;
    public float rayDistance;

    [Header ("Unity Setup")]
    public Transform[] raycastStarts;
    public Rigidbody2D rampantRb;
    public LayerMask mouvementLayer;
    public LayerMask explosionLayer;
    public BoxCollider2D deadlyBox;
    public bool canMove;

    [Header("FeedBack")]
    public GameObject deathParticule;

    private Vector2 dir;
    private RaycastHit2D hit;

    private void Start()
    {
        dir = Vector2.right;
    }

    void Update()
    {
        HitDetection();

        if (canMove)
        {
            Mouvement();
        }
    }

    void Mouvement()
    {
        rampantRb.velocity = dir * speed;
    }

    void HitDetection()
    {
        for (int i = 0; i < raycastStarts.Length; i++)
        {
            hit = Physics2D.Raycast(raycastStarts[i].position, raycastStarts[i].right, rayDistance, mouvementLayer);

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

    void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayer);

        foreach (Collider2D obj in colliders)
        {
            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

            if (objRb != null)
            {
                objRb.AddForce(Vector2.up * explosionForce);
                //objRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f);
            }
        }

    }

    void Dead()
    {
        GameObject deathEffect = Instantiate(deathParticule, transform.position, transform.rotation);
        Destroy(deathEffect, 5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) // Layer 11 = Player layer
        {
            deadlyBox.enabled = false;
            Explosion();
            Dead();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
