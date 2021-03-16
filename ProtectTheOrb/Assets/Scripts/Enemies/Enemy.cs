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
    public float deathTime = 1f;
    public bool willDestroy = false;
    
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
            Death();
        }
    }

    public void Death() {
        StartCoroutine("OnDeath");
    }

    IEnumerator OnDeath() {
        willDestroy = true;
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
