using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float health;
    private float currentHealth;

    public GunController gun;

    public ParticleSystem particle;

    public Slider healthBar;

    public float flashLength;
    private float flashCounter;

    public Renderer rend;

    public GameObject deathMenu;

    private int finId1 = -1; //id finger for cancel touch event


    // Use this for initialization
    void Start () {
        currentHealth = health;
        deathMenu.SetActive(false);
        Input.multiTouchEnabled = true; //enabled Multitouch


    }

    // Update is called once per frame
    void Update () {

        IsFiring();
        
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            deathMenu.SetActive(true);
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
        rend.material.SetColor("_Color", Color.red);
        particle.Play();
    }

    public void IsFiring()
    {

        int i = 0;
        //loop over every touch found
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //move right
                    gun.isFiring = true;
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    //move right
                    gun.isFiring = false;
                }
            }
            else if (Input.GetTouch(1).position.x > Screen.width / 2)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //move right
                    gun.isFiring = true;
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    //move right
                    gun.isFiring = false;
                }
            }
            else
            {
                gun.isFiring = false;
            }

            ++i;
        }

       
    }
}
