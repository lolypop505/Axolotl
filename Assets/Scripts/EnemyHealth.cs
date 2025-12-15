using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject gameObj;

    public GameObject minusOneEnemy;

    void Start()
    {
        health = maxHealth;
        gameObj.GetComponent<HealseBar>().UpdateHealthBar(maxHealth, health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameObj.GetComponent<HealseBar>().UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    public void DestroyEnemy()
    {
        minusOneEnemy.GetComponent<BeginningOfBattle>().minusOneEnemy();
        Destroy(gameObj);
    }
}
