using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float rotationSpeed;

    [Header ("Unity Setup")]
    public Rigidbody2D playerRb;
    public Transform jumpDirection;
    public LayerMask groundLayer;

    [Header("Raycast info")]
    public float rayDistance;
    public Transform leftStartRaycast;
    public Transform rightStartRaycast;

    [Header("Info")]
    public bool inTheAir;
    [SerializeField]
    private float horizontaleAxe;
    [SerializeField]
    private float verticalAxe;
    private RaycastHit2D hit;

    void FixedUpdate()
    {
        horizontaleAxe = Input.GetAxis("Horizontal");
        verticalAxe = Input.GetAxis("Vertical");

        Movement();
        Jump();
        AirVerification();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !inTheAir)
        {
            Vector2 jumpDir = (jumpDirection.position - transform.position).normalized;
            playerRb.AddForce(jumpDir * jumpForce);
        }
    }
    void Movement()
    {
        if (!inTheAir)
        {
            playerRb.AddForce(new Vector2(horizontaleAxe, 0) * Time.deltaTime * speed);
        }
        else
        {
            if (horizontaleAxe > 0.1f)
            {
                playerRb.rotation -= rotationSpeed;
            }
            else if (horizontaleAxe < -0.1f)
            {
                playerRb.rotation += rotationSpeed;
            }
        }
    }
  
    void AirVerification()
    {
        hit = Physics2D.Raycast(leftStartRaycast.position, leftStartRaycast.up, rayDistance, groundLayer);

        if (hit.collider != null) // Si on touche
        {
            inTheAir = false;
        }
        else
        {
            inTheAir = true;
        }
    }
}
