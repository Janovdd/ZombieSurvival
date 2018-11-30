using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHealth : MonoBehaviour {
    public float health;
    public float currentHealth;

    public Slider healthBar;

    public float flashLength;
    private float flashCounter;

    public Renderer rend;

    // Use this for initialization
    void Start () {
        currentHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.value = currentHealth / health;

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", Color.white);
            }
        }
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.red);    }
}
