using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : Tower, ITower
{
    public Transform upRotationPoint, forwardRotationPoint;
    
    float rotationSpeed = 4f;
    void FixedUpdate()
    {
        if (target != null) {
            RotateToTarget();
        }
    }

    void RotateToTarget() {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion rotationTarget = Quaternion.LookRotation(dir);
        Vector3 newUpRotation = Quaternion.Lerp(upRotationPoint.rotation, rotationTarget, Time.deltaTime * rotationSpeed ).eulerAngles;
        Vector3 newFowardRotation = Quaternion.Lerp(forwardRotationPoint.rotation, rotationTarget, Time.deltaTime * rotationSpeed ).eulerAngles;

        
        upRotationPoint.rotation = Quaternion.Euler(0f, newUpRotation.y, 0f);
        forwardRotationPoint.localRotation = Quaternion.Euler(newFowardRotation.x, 0f, 0f );
    }

    override public void Fire() {
        base.Fire();
        // TODO: Ballista Animation
    }
}
