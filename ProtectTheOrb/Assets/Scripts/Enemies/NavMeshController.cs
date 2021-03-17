using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    Enemy enemy;
    // public Transform[] wayPoints;
    // public int currentWayPoint = 0;
    // public float leaveRange = 30f;
    public Transform spawnPoint, exitPoint;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.destination = wayPoints[currentWayPoint].position;
        enemy = GetComponent<Enemy>();
        agent.speed = enemy.enemyValues.speed;
        agent.destination = exitPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.willDestroy) {
            agent.isStopped = true;
            agent.radius = 0.1f;
        }
    }



    // public void OnEnterWayPoint() {
    //     // if (enemyController.engaged) return;
    //     //dont change target if chasing
    //     //loop points
    //     currentWayPoint++;
    //     currentWayPoint =  currentWayPoint >= wayPoints.Length ? 0 : currentWayPoint;
    //     //set destination
    //     agent.destination = wayPoints[currentWayPoint].position;
    // }

    // public void OnStartDetect(Vector3 target) {
    //     // enemyController.Engage();
    //     agent.destination = target;
    // }
    // public void OnEndDetect() {
    //     // enemyController.Disengage();
    //     agent.destination = wayPoints[currentWayPoint].position;
    // }
}

