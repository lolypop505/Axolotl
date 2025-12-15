using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningOfBattle : MonoBehaviour
{
    public GameObject partitions;

    public int theNumberOfEnemies;
    public int initialNumberOfNnemies;

    void Start()
    {
        theNumberOfEnemies = initialNumberOfNnemies;
    }

    void Update()
    {
        if(theNumberOfEnemies == 0)
        {
            partitions.SetActive(false);
        }
    }    
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            partitions.SetActive(true);
        }
        
    }

    public void minusOneEnemy()
    {
        theNumberOfEnemies -= 1;
    }
}
