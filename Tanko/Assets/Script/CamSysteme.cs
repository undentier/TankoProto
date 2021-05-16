using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSysteme : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    void Update()
    {
        if (cam.Follow == null)
        {
            if (LevelManager.instance.playerList.Count > 0)
            {
                cam.Follow = LevelManager.instance.playerList[0].transform;
            }
        }
    }
}
