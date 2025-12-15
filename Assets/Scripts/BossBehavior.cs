using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    private GameObject Player;
    public GameObject Boss;
    public Vector3 dir;

    public NavMeshAgent AI_Agent;

    public LayerMask whatIsGround;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float jumpForce, upwardForce, speed;

    void Start()
    {
        //присваевание компонентров
        //bossRB = Boss.GetComponent<Rigidbody>();
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        StartCoroutine(TheFirstPhase());//первая фаза включается
    }

    void FixedUpdate()
    {
        //Patroling();
    }

    void Update()
    {
        //Patroling();
        //проверка на состояние здороровья
        //будет три фазы значит здоровье делим на 3 и проверяем не ниже ли оно 
        //1\3  вторая фаза  StartCoroutine(TheSecondPhase());
        //2\3  третья фаза  StartCoroutine(TheThirdPhase());
        //и тогда включается IEnumerator
    }

    IEnumerator TheFirstPhase()
    {
        while (true)
        {
            Patroling();
            yield return new WaitForSeconds(5f);
            Jump();
            //ходит по рандомным точкам
            //yield return new WaitForSeconds(5f);
            //один раз прыгает
        }

    }

    public void Jump()
    {
        Vector3 direction = Player.transform.position - Boss.transform.position;
        Boss.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);
        Boss.GetComponent<Rigidbody>().AddForce(direction * jumpForce, ForceMode.Impulse);
    }

    public void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) transform.position = Vector3.MoveTowards(transform.position, walkPoint, speed * Time.fixedDeltaTime);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    public void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    //IEnumerator TheSecondPhase()
    //{

    //}

    //IEnumerator TheThirdPhase()
    //{

    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
