using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    
    public float attackRange;
    public LayerMask WhatIsEnemies;

    public bool inAttackRange;

    private Rigidbody rb;
    public Animator animator;
    public ParticleSystem trailParticleSystem;

    public float speed;
    public float activeSpeed;
    public float speedEvasion;

    private float evasionLength = .5f, evasionCooldown = 1f;
    private float evasionCounter;
    private float evasionCoolCounter;

    public float health;
    public float maxHealth;
    public Image healthBarSprite;

    public Animator animatorDiedUI;
    public Animator animatorWinUI;

    public int damage;
    public float attackRate;
    private float nextAttackTime;

    private Vector3 moveVector;

    public int countEnemies;

    void Awake()
    {
        instance = this;

        activeSpeed = speed;
        health = maxHealth;
        UpdateHealthBar(maxHealth, health);

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");
        UdateAnimation();

        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (evasionCoolCounter <= 0 && evasionCounter <= 0)
            {
                //animator.SetTrigger("Evasion");
                activeSpeed = speedEvasion;
                evasionCounter = evasionLength;
            }
        }

        if (evasionCounter > 0)
        {
            evasionCounter -= Time.deltaTime;
            if (evasionCounter <= 0)
            {
                activeSpeed = speed;
                evasionCoolCounter = evasionCooldown;
            }
        }

        if (evasionCoolCounter > 0)
        {
            evasionCoolCounter -= Time.deltaTime;
        }
    }

    void UdateAnimation()
    {
        if (moveVector != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("Horizontal", moveVector.x);
            animator.SetFloat("Vertical", moveVector.z);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Hit1");

        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, WhatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    public static void TakeDamagePlayer(int damage)
    {
        instance.health -= damage;
        instance.UpdateHealthBar(instance.maxHealth, instance.health);

        if (instance.health <= 0)
        {
            Debug.Log("Óלונ");
            instance.Invoke(nameof(Die), 0.5f);
        }
    }

    public void Die()
    {
        animatorDiedUI.SetTrigger("Die");
        Invoke(nameof(Pause), 1f);
    }

    public void EnemiesMinus()
    {
        countEnemies -= 1;
        if (countEnemies <= 0)
        {
            Debug.Log("Âûידנאכ");
            instance.Invoke(nameof(Win), 0.5f);
        }
    }

    public void Win()
    {
        animatorWinUI.SetTrigger("Win");
        Invoke(nameof(Pause), 1f);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void UpdateHealthBar(float maxHealth, float health)
    {
        healthBarSprite.fillAmount = health / maxHealth;
    }

    void MoveCharacter()
    {
        rb.MovePosition(rb.position + moveVector * activeSpeed * Time.deltaTime);
    }

    public void ParticleSysStop()
    {
        trailParticleSystem.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
