using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public bool isFiring;

    public BulletContoller bullet;
    public int damage;
    public float bulletSpeed;
    public float fireRate;
    public ParticleSystem particle;
    private float shotCounter;

    public Transform firePoint;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isFiring == true)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = fireRate;
                BulletContoller newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletContoller;
                newBullet.speed = bulletSpeed;
                newBullet.damage = damage;
                particle.Play();
            }
        }
        else
        {
            shotCounter = 0;
        }
	}
}
