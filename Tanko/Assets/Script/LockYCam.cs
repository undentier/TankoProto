using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockYCam : MonoBehaviour
{
    public Transform cam;
    
    void LateUpdate()
    {
        cam.position = new Vector2(cam.position.x, 0);
    }
}
