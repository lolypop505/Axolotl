using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleSystem : MonoBehaviour
{
    public int damage;
    public float speed;

    public bool moving;

    void FixedUpdate()
    {
        if (moving)
        {
            transform.position += transform.forward * (speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        speed = 0;
        if (other.gameObject.tag == "Player")
        {
            PlayerController.TakeDamagePlayer(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
