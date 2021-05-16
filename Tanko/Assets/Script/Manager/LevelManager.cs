using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject playerPrefab;
    public Transform startSpawnPoint;
    public List<GameObject> playerList = new List<GameObject>();

    [HideInInspector] public Transform actualSpawnpoint;

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

    private void Start()
    {
        actualSpawnpoint = startSpawnPoint;
    }

    void Update()
    {
        playerList.RemoveAll(list_item => list_item == null);

        if (Input.GetButtonDown("Respawn") && playerList.Count < 1)
        {
            Respawn();
        }
    }


    public void Respawn()
    {
        GameObject actualspawn = Instantiate(playerPrefab, actualSpawnpoint.position, actualSpawnpoint.rotation);
        playerList.Add(actualspawn);
    }
}
