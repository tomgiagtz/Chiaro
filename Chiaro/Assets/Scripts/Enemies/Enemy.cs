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
    public float currentHealth;
    public bool willDestroy = false;
    private float DEATH_TIME = 1f;
    public Animator animator;
    public float invulnTime = 1f;
    
    // Start is called before the first frame update
    float timeAtStart;
    void Start()
    {
        timeAtStart = Time.timeSinceLevelLoad;
        animator.SetBool("Run Forward", true);
        currentHealth = enemyValues.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage(float damageAmount) {
        if (Time.timeSinceLevelLoad - timeAtStart < invulnTime)
            return;
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
        animator.SetBool("Run Forward", false);
        animator.SetTrigger("Die");
        GameController.Instance.AddCoins(enemyValues.coinValue);
        yield return new WaitForSeconds(DEATH_TIME);
        Destroy(gameObject);
    }
}
