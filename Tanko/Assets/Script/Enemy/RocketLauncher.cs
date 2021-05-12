using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [Header ("Stats")]
    public float fireRate;
    public float rocketSpeed;

    [Header ("Unity setup")]
    public Transform shootPoint;
    public GameObject rocketPrefab;

    private float fireRateTimer;

    void Update()
    {
        if (fireRateTimer <= 0)
        {
            fireRateTimer = fireRate;
            Fire();
        }
        else
        {
            fireRateTimer -= Time.deltaTime;
        }
    }

    void Fire()
    {
        GameObject actualRocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
        actualRocket.GetComponent<Rigidbody2D>().AddForce((Vector2.left) * rocketSpeed);
    }
}
