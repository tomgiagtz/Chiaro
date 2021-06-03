using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ValidPathController : MonoSingleton<ValidPathController>
{
    public Transform spawnPoint, exitPoint;
    private NavMeshPath path;
    private float elapsed = 0.0f;

    public bool isValidPath = true;
    void Start()
    {
        path = new NavMeshPath();

        // Update the way to the goal every second.
        InvokeRepeating("UpdateIsValidPath", 0f, 0.5f);
    }

    void Update()
    {
        
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    void UpdateIsValidPath() {
        NavMesh.CalculatePath(spawnPoint.position, exitPoint.position, NavMesh.AllAreas, path);
        // Debug.Log(path.status);

        isValidPath = path.status == NavMeshPathStatus.PathComplete;
    }
}
