using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public int damage;
    public Animator animator;

    public float timeDown;
    public float timeUp;

    void Start()
    {
        StartCoroutine(Thorn());
    }

    IEnumerator Thorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeDown);
            animator.SetTrigger("Up");
            yield return new WaitForSeconds(timeUp);
            animator.SetTrigger("Down");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.TakeDamagePlayer(damage);
        }
    }
}
