using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {

    public int damage;
    public float attackSpeed;
    private float attackTimer = 0f;

    void Start()
    {

    }

    public void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Building")
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                other.gameObject.GetComponent<ObjectHealth>().DamagePlayer(damage);
                attackTimer = attackSpeed;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                other.gameObject.GetComponent<PlayerController>().DamagePlayer(damage);
                attackTimer = attackSpeed;
            }
        }
        else if (other.gameObject.tag == "Attackable")
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                other.gameObject.GetComponent<ObjectHealth>().DamagePlayer(damage);
                attackTimer = attackSpeed;
            }
        }
    }
}
