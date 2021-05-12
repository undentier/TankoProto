using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDetection : MonoBehaviour
{
    public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)  // Layer 9 = Deadly layer
        {
            playerController.PlayerDeath();
        }
    }
}
