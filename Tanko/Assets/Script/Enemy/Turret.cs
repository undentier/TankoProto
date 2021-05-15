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
    public bool canShoot;

    [Header("FeedBack")]
    public GameObject deathParticule;


    private float fireCoolDown;

    private void Start()
    {
        fireCoolDown = fireRate;
    }

    void Update()
    {
        if (canShoot)
        {
            ChoseTarget();
            if (target != null)
            {
                ShootingSysteme();
            }
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

    void ChoseTarget()
    {
        foreach (GameObject player in LevelManager.instance.playerList)
        {
            float playerDistance = Vector2.Distance(transform.position, player.transform.position);
            float minDist = Mathf.Infinity;

            if (playerDistance < minDist)
            {
                minDist = playerDistance;
                target = player.transform;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) // PlayerBullet layer = 10
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
