using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public Rigidbody2D playerRb;
    public float horizontaleAxe;
    public float verticalAxe;

    void Update()
    {
        horizontaleAxe = Input.GetAxis("Horizontal");
        verticalAxe = Input.GetAxis("Vertical");

        if (horizontaleAxe >= 0.1f)
        {
            playerRb.velocity = new Vector2(horizontaleAxe, 0) * Time.deltaTime * speed;
        }
        
        if (horizontaleAxe <= -0.1f)
        {
            playerRb.velocity = new Vector2(horizontaleAxe, 0) * Time.deltaTime * speed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerRb.AddForce(Vector2.up * jumpForce);
        }
    }
}
