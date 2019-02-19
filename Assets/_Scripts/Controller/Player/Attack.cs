﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject bulletPrefab;
    private Transform bulletSpawn;
	private float attackSpeed;
	private int bulletSpeed;
	private float lastShotTime;
	private float lifeTime;

    // Use this for initialization
    void Start () {
		lastShotTime = 0.0f;
        attackSpeed = 10;
        bulletSpeed = 15;
        lifeTime = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) //LMB
		{

			Vector2 target = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));

			Vector2 myPosition = new Vector2 (transform.position.x, transform.position.y);

			Vector2 direction = target - myPosition;
			direction.Normalize ();

			//rotates based obn the direction the bullet is flying
			Quaternion rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg-45);

			
			if (Time.time > (attackSpeed * Time.deltaTime) + lastShotTime) {
				// Create the Bullet from the Bullet Prefab
				GameObject bullet = (GameObject)Instantiate (
					bulletPrefab,
					myPosition, rotation);
				
				Debug.Log("Bullet Fired");


				// Add velocity to the bullet
				bullet.GetComponent<Rigidbody2D> ().velocity = direction/*Input.mousePosition*/ * bulletSpeed;
				Debug.Log("Bullet leaving towards direction");




			// Destroy the bullet after 2 seconds

			Destroy (bullet, lifeTime);
			lastShotTime = Time.time;

        	}
    	}
    }

    
}
