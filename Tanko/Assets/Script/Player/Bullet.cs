using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject destructionParticule;
    public LayerMask layerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((layerMask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            GameObject deathParticule = Instantiate(destructionParticule, transform.position, transform.rotation);
            Destroy(deathParticule, 5f);
            Destroy(gameObject);
        }
    }
}
