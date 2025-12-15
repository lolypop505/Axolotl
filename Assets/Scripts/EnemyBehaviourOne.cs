using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehaviourOne : MonoBehaviour
{
    public static EnemyBehaviourOne instance;

    public Vector3 dir;
    private Rigidbody rb;

    private Animator animator;

    public NavMeshAgent AI_Agent;
    public Transform Player;

    public int damage = 10;

    public LayerMask WhatIsPlayer;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public float sightRange, attackRange;
    public bool playerInSighRange, playerInAttackRange;


    void Start()
    {
        instance = this;
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();

        Player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerInSighRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        dir = Player.position - transform.position;
        if (playerInSighRange && !playerInAttackRange)
        {
            ChasePlayer();
            animator.SetBool("Running", true);
            animator.SetFloat("Horizontal", dir.z);
            animator.SetFloat("Vertical", dir.x);
        } 
        if (playerInSighRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        AI_Agent.SetDestination(Player.transform.position);
    }

    void AttackPlayer()
    {
        //AI_Agent.SetDestination(transform.position);

        if (timeBtwAttack <= 0)
        {
            //anim.SetTrigger("Attack");
            //AI_Agent.SetDestination(transform.position);
            timeBtwAttack = startTimeBtwAttack;
            PlayerController.TakeDamagePlayer(damage);
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
