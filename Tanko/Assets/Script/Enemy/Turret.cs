using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header ("Stats")]
    public float bulletSpeed;
    public float fireRate;
    public int health;
    [Space]
    public float rotationSpeed;
    public float shotingDistance;

    [Header ("Unity Setup")]
    public Transform partToRotate;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public Transform target;
    public LayerMask playerBulletLayer;

    [Header("FeedBack")]
    public GameObject deathParticule;


    private float fireCoolDown;

    void Update()
    {
        if (target != null)
        {
            ShootingSysteme();
        }
    }

    void ShootingSysteme()
    {
        if (Vector2.Distance(transform.position, target.position) <= shotingDistance)
        {
            AimSyteme();

            if (fireCoolDown <= 0)
            {
                Shoot();
                fireCoolDown = fireRate;
            }
            else
            {
                fireCoolDown -= Time.deltaTime;
            }
        }
    }

    void AimSyteme()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, q, Time.deltaTime * rotationSpeed);
    }

    void Shoot()
    {
        GameObject actualBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        actualBullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerBulletLayer)
        {
            Damage(1);
        }
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        GameObject deathEffect = Instantiate(deathParticule, transform.position, transform.rotation);
        Destroy(deathEffect, 5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shotingDistance);
    }

}
