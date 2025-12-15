using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private static PlayerCombat instance;

    public Animator animator;
    public ParticleSystem trailParticleSystem;

    void Start()
    {
        instance = this;
    }

    //void Update()
    //{

    //}

    public static void Attack()
    {
        instance.trailParticleSystem.Play();
        instance.animator.SetTrigger("Hit1");
        //if (flag == true)
        //{
        //    //отбавляется здоровье у врага
        //}
    }

}
