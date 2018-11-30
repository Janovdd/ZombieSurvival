using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public float cameraX;
    public float cameraY;
    public float cameraZ;

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.x += cameraX;
        pos.z += cameraY;
        pos.y += cameraZ;
        transform.position = pos;
    }
}
