using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourTwo : MonoBehaviour
{
    public static EnemyBehaviourTwo instance;

    public Vector3 dir;
    private Rigidbody rb;
    public Animator animator;

    public NavMeshAgent AI_Agent;
    public Transform Player;

    public int damage = 10;

    public LayerMask whatIsGround, WhatIsPlayer, Other;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public float attackRange, fireAttackRange;
    public bool playerInAttackRange, fireInAttackRange, fireRange;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public GameObject[] spawnPoints;
    public GameObject objToSpawn;

    void Start()
    {
        instance = this;
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();

        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EnemyBehavior());
    }

    void Update()
    {
        dir = walkPoint - transform.position;

        animator.SetBool("Running", true);
        animator.SetFloat("Horizontal", dir.z);
        animator.SetFloat("Vertical", dir.x);
        
        Patroling();
    }

    IEnumerator EnemyBehavior()
    {
        while (true)
        {          
            yield return new WaitForSeconds(5f);
            
            AttackPlayer();
        }
    }

    public void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) AI_Agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    public void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void AttackPlayer()
    {
        //AI_Agent.SetDestination(transform.position);

        //animator.SetTrigger("Attack");
        AI_Agent.SetDestination(transform.position);

        float num = 0;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnFireBullets(num, i);
            num += 45;
        }
    }

    public void SpawnFireBullets(float num, int i)
    {
        GameObject fire;
        if (spawnPoints != null)
        {
            fire = Instantiate(objToSpawn, spawnPoints[i].transform.position, Quaternion.Euler(0, num, 0));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
