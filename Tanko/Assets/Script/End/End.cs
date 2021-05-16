using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public static End instance;

    [Header ("Unity Setup")]
    public SpriteRenderer filler;
    public float timeToStay;
    public bool victory;
    public Transform[] particuleSpawnPoint;

    [Header("FeedBack")]
    public GameObject victoryParticule;

    private bool playerIn;
    private float actualTime;
    private float fillerMultiplier;
    private float fillAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        fillerMultiplier = filler.size.y / timeToStay;

        filler.size = new Vector2(filler.size.x, 0);

    }

    private void Update()
    {
        VictoryDetection();
    }

    void Victory()
    {
        victory = true;

        for (int i = 0; i < particuleSpawnPoint.Length; i++)
        {
            GameObject actualParticule = Instantiate(victoryParticule, particuleSpawnPoint[i].position, particuleSpawnPoint[i].rotation);
            Destroy(actualParticule, 5f);
        }
    }

    void VictoryDetection()
    {
        if (!victory)
        {
            if (playerIn)
            {
                if (actualTime < timeToStay)
                {
                    actualTime += Time.deltaTime;
                    fillAmount = actualTime * fillerMultiplier;
                    filler.size = new Vector2(filler.size.x, fillAmount);
                }

                if (actualTime >= timeToStay)
                {
                    Victory();
                }
            }
            else if (actualTime > 0 && !playerIn)
            {
                actualTime -= Time.deltaTime;
                fillAmount = actualTime * fillerMultiplier;
                filler.size = new Vector2(filler.size.x, fillAmount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) // Layer 11 = Player layer
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) // Layer 11 = Player layer
        {
            playerIn = false;
        }
    }
}
