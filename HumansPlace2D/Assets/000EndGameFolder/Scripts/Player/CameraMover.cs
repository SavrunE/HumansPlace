using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public GameObject PlayerPosition;
    public Vector3 CameraPosition;
    void Start()
    {
        CameraPosition = transform.position;
        CameraPosition -= PlayerPosition.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = PlayerPosition.transform.position + CameraPosition;
    }

}
