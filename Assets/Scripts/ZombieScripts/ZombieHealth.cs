using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour {

    public int health;
    public ParticleSystem particle;
    private int currentHealth;

    public float flashLength;
    private float flashCounter;

    public int value;
    private GameObject coinManager;

    public Renderer rend;

    // Use this for initialization
    void Start () {
        coinManager = GameObject.Find("GameManager");
        currentHealth = health;
    }

// Update is called once per frame
void Update () {
		
        if(currentHealth <= 0)
        {
            coinManager.GetComponent<CoinManager>().GetCoins(value);
            Destroy(gameObject);
        }

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", Color.white);
            }
        }
    }

    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.red);
        particle.Play();
    }
}
