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
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Run Forward", true);
        enemyValues.currentHealth = enemyValues.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage(int damageAmount) {
        Debug.Log("Ouch");
    }
}
