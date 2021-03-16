using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToCamera : MonoBehaviour
{
    // Work because of orthographic camera
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
