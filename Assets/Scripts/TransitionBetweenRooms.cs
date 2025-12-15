using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBetweenRooms : MonoBehaviour
{
    public Transform point;

    public GameObject thisRoom;
    public GameObject nextRoom;

    public Animator animator;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Transitions(collision));
        }
    }

    IEnumerator Transitions(Collider collision)
    {
        animator.SetTrigger("Start");
        nextRoom.SetActive(true);

        yield return new WaitForSeconds(0.8f);

        collision.transform.position = point.transform.position;
        thisRoom.SetActive(false);
    }
}
