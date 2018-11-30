using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public int damage;
    public float attackSpeed;
    public float uses;
    private float currentUses;
    private float attackTimer = 0f;
    

    // Use this for initialization
    void Start () {
        currentUses = uses;
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                other.gameObject.GetComponent<ZombieHealth>().HurtEnemy(damage);
                attackTimer = attackSpeed;
                currentUses -= 1f;
            }
        }

        if (currentUses <= 0)
        {
            Destroy(gameObject);
        }

    }
}
