using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public int ScenesIndex;
    public Animator animator;

    private bool flag;

    void start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            flag = true;
            animator.SetBool("Open", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        flag = false;
        animator.SetBool("Open", false);
    }

    void Update()
    {
        if (flag == true)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                animator.SetBool("Open", false);
                ScenesTransition.SwitchToScenes(ScenesIndex);
            }
        }
    }
}