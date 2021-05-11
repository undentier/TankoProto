using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float airRotationSpeed;
    public float canonRotationSpeed;
    public float rocketJumpForce;
    public float bulletForce;

    [Header("Unity Setup")]
    public GameObject bulletPrefab;
    public Rigidbody2D playerRb;
    public Transform jumpDirection;
    public Transform canonJumpDirection;
    public Transform firePoint;
    public Transform partToRotate;
    public LayerMask groundLayer;

    [Header("Raycast info")]
    public float rayDistance;
    public Transform[] rayStarts;

    [Header("Info")]
    public bool inTheAir;

    private float horizontaleAxe;
    private float verticalAxe;

    private float horizontalRight;
    private float verticalRight;

    private RaycastHit2D hit;
    private int rayTouchIndex;

    void Update()
    {
        #region Axis
        horizontaleAxe = Input.GetAxis("Horizontal");
        verticalAxe = Input.GetAxis("Vertical");

        horizontalRight = Input.GetAxis("Joystick Right Horizontal");
        verticalRight = Input.GetAxis("Joystick Right Vertical");
        #endregion

        Jump();
        Movement();
        AirVerification();
        AimCanon();
        Fire();
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
                playerRb.rotation -= airRotationSpeed;
            }
            else if (horizontaleAxe < -0.1f)
            {
                playerRb.rotation += airRotationSpeed;
            }
        }
    }
    void AirVerification()
    {
        for (int i = 0; i < rayStarts.Length; i++)
        {
            hit = Physics2D.Raycast(rayStarts[i].position, rayStarts[i].up, rayDistance, groundLayer);

            if (hit.collider != null)
            {
                rayTouchIndex += 1;

                Debug.DrawRay(rayStarts[i].position, rayStarts[i].up * rayDistance, Color.green);
            }
            else
            {
                Debug.DrawRay(rayStarts[i].position, rayStarts[i].up * rayDistance, Color.red);
            }
        }

        if (rayTouchIndex > 0)
        {
            inTheAir = false;
        }
        else
        {
            inTheAir = true;
        }

        rayTouchIndex = 0;
    }
    void AimCanon()
    {
        if (!inTheAir)
        {
            if (horizontalRight > 0.1f && partToRotate.localEulerAngles.z > 5f) 
            {
                partToRotate.eulerAngles -= Vector3.forward * canonRotationSpeed;
            }
            else if (horizontalRight < -0.1f && partToRotate.localEulerAngles.z < 178f)
            {
                partToRotate.eulerAngles += Vector3.forward * canonRotationSpeed;
            }
        }
        else
        {
            Vector3 lerpRotation = Quaternion.Lerp(partToRotate.rotation, canonJumpDirection.rotation, Time.deltaTime * 7f).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(lerpRotation);
        }
    }

    void Fire()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

            if (!inTheAir)
            {
            }
            else
            {
                Vector2 direction = (firePoint.position - transform.position).normalized;
                playerRb.AddForce(-direction * rocketJumpForce);
            }
        }
    }
}
