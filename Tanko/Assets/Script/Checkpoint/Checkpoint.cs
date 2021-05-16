using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer rend;
    public GameObject checkpointParticule;
    public Sprite fullSprite;

    private bool lockTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 && !lockTrigger) // Layer 11 = Player layer
        {
            lockTrigger = true;
            rend.sprite = fullSprite;
            LevelManager.instance.actualSpawnpoint = gameObject.transform;

            GameObject actualEffect = Instantiate(checkpointParticule, transform.position, transform.rotation);
            Destroy(actualEffect, 5f);
        }
    }



}
