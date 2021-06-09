using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITower {
    void Fire();
    void GetTarget();
    void ClearTarget();
    void Upgrade();
}
public class Tower : MonoBehaviour, ITower
{
    [SerializeField] public GameObject target;
    
    public TowerValues towerValues;
    [SerializeField] LayerMask targetMask;
    List<GameObject> validTargets = new List<GameObject>();
    EnemyClasses damageableEnemyClasses = EnemyClasses.All;
    float fireCooldown = 0;
    Enemy targetEnemy;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = towerValues.range;
    }

    // Update is called once per frame
    public void Update()
    {
        GetTarget();

        if (target != null) {
            IsFiring();
        }
    }

    void IsFiring() {
        if (fireCooldown <= 0f) {
            Fire();
            fireCooldown = 1f / towerValues.rateOfFire;
        } 
        fireCooldown -= Time.deltaTime;
    }

    virtual public void Fire() {
        if (targetEnemy != null) {
            targetEnemy.OnDamage(towerValues.damage);
            if (targetEnemy.currentHealth <= 0) {
                targetEnemy.Death();
                ClearTarget();
            }
        } else {
            ClearTarget();
        }
    }

    public void Upgrade() {

    }

    public void GetTarget() {
        if (target != null && targetEnemy.willDestroy) ClearTarget();
        if (target == null && validTargets.Count != 0) {
            if (validTargets[0] == null) {
                validTargets.RemoveAt(0);
            } else {
                target = validTargets[0];
                targetEnemy = target.GetComponent<Enemy>();
            }
            
        }
    }

    public void ClearTarget() {
        validTargets.Remove(target);
        target = null;
        targetEnemy = null;
    }

    
    private void OnTriggerEnter(Collider other) {
        if (IsInLayerMask(other.gameObject, targetMask)){
            validTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (IsInLayerMask(other.gameObject, targetMask)){
            if (other.gameObject == target) {
                ClearTarget();
            }
            validTargets.Remove(other.gameObject);
            
        }
    }

    static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
	    return ((layerMask.value & (1 << obj.layer)) > 0);
    }


}
