using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYController : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * speed * 5);
    }
}
