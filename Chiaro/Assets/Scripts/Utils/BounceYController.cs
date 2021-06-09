using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceYController : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 1f;
    float startY;

    private void Start() {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
    }

    
}
