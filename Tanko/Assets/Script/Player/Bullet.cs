using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject destructionParticule;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject deathParticule = Instantiate(destructionParticule, transform.position, transform.rotation);
        Destroy(deathParticule, 5f);
        Destroy(gameObject);
    }
}
