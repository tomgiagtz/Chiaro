using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerValues", menuName="Values/TowerValues")]
public class TowerValues : ScriptableObject
{
    public int cost;
    public float range;
    public float damage;
    //shots per second
    public float rateOfFire;
    public GameObject prefab;
    // public TowerValues upgrade;
    public GameObject upgrade;
}


