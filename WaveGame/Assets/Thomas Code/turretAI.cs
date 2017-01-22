﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAI : MonoBehaviour {
    public GameObject target;
    public GameObject bullet;
    public GameObject TurretHead;
    public Transform[] Muzzels;
    public int ammo, maxAmmo;
    public float bulletSpeed;
    public int bulletDamage;
    // Use this for initialization
    public int currentLevel;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetTurretRotation();
    }

    void SetTurretRotation()
    {
        if(target == null)
        {
            TurretHead.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            TurretHead.transform.LookAt(target.transform);
            TurretHead.transform.Rotate(0, 90, 0);
            Fire();
            
        }
    }


    public float timer, rof;
    public int firedRounds;
    void Fire()
    {
        if(timer <= 0 && ammo >0)
        {
          //  print("Fire");
            timer += rof;

            if(currentLevel == 2)
            {
             //   print("Level 2 Firing");
                   GameObject temp = Instantiate(bullet, Muzzels[firedRounds%3].position, Muzzels[firedRounds%3].rotation) as GameObject;
                    temp.GetComponent<Bullet>().damage = bulletDamage;
                    temp.GetComponent<Rigidbody>().velocity = temp.transform.forward * bulletSpeed;
                firedRounds++;
            }
            else
            {
                GameObject temp = Instantiate(bullet, Muzzels[0].position, Muzzels[0].rotation) as GameObject;
                temp.GetComponent<Bullet>().damage = bulletDamage;
                temp.GetComponent<Rigidbody>().velocity = temp.transform.forward * bulletSpeed;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bad Guy" && target ==null)
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(target == null && other.tag == "Bad Guy")
        {
            target = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            target = null;
        }
    }
}
