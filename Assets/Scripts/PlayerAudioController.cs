using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource[] running;
    public AudioSource[] attack;
    public GameObject[] smilAttack;
    private int R;

    public void Running()
    {
        running[Random.Range(0, running.Length)].Play();
    }

    public void Attacking()
    {
        R = Random.Range(0, attack.Length);
        attack[R].Play();
        smilAttack[R].SetActive(true);
    }

    public void AttackSmil()
    {
        smilAttack[R].SetActive(false);
    }
}
