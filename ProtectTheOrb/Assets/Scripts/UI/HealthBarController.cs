using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy enemy;
    public Image healthBar;
    public float pollRate = 0.1f;

    void UpdateHealthBar() {
        healthBar.fillAmount = enemy.currentHealth / enemy.enemyValues.maxHealth;
    }

    private void Start() {
        enemy = GetComponentInParent<Enemy>();
        InvokeRepeating("UpdateHealthBar",0, pollRate);
    }
}
