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
    public EnemyValues enemyValues;
    public Animator animator;
    public float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Run Forward", true);
        currentHealth = enemyValues.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage(float damageAmount) {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) {
            // Destroy(gameObject);
        }
    }

    public void Death() {
        Destroy(this.gameObject);
    }
}
