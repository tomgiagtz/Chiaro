using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyClasses {
        Ground,
        Aerial,
        All
}

public enum EnemyTypes {
    Grunt,
    Flying
}
public class Enemy : MonoBehaviour
{
    EnemyValues enemyValues;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage() {
        Debug.Log("Ouch");
    }
}
