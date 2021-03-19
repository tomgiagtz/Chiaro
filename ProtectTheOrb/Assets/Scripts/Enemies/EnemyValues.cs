using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyValues", menuName="Values/EnemyValues")]
public class EnemyValues : ScriptableObject
{
    public float maxHealth;
    public float speed = 1;
    public bool isFlying = false;
    public EnemyTypes enemyType = EnemyTypes.Grunt;
    public int coinValue = 1;
    public int damageValue = 1;
    public GameObject prefab;
}
