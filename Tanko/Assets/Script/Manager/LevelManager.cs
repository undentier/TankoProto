using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject playerPrefab;
    public Transform spawnPoint;

    public List<GameObject> playerList = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple LevelManager in scene");
        }
    }

    void Update()
    {
        playerList.RemoveAll(list_item => list_item == null);

        if (Input.GetButtonDown("Respawn"))
        {
            Respawn();
        }
    }


    public void Respawn()
    {
        GameObject actualspawn = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        playerList.Add(actualspawn);
    }
}
