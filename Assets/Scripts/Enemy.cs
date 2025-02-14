using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public static float minDistance = 50;
    public static float attackTime = 5;
    public float distanceFromPlayer, speedTowardPlayer, timeToFire;
    public GameController controller;

    void Start()
    {
        distanceFromPlayer = 200;
        speedTowardPlayer = 5;
        timeToFire = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        //reduce distance to player based on speed, up to minimum
        if (distanceFromPlayer > minDistance)
        {
            distanceFromPlayer = Math.Max(distanceFromPlayer - Time.deltaTime * speedTowardPlayer, minDistance);
        }

        //reduce time to fire; if at or below 0, fire and reset to attackTime
        timeToFire -= Time.deltaTime;
        if (timeToFire <= 0)
        {
            Fire();
            timeToFire = attackTime;
        }
    }

    private void Fire()
    {
        //Damge player based on proximity
        Debug.Log("Fire!");
        if (distanceFromPlayer < 60)
        {
            controller.DamagePlayer(3);
        }
        else if (distanceFromPlayer < 100)
        {
            controller.DamagePlayer(2);
        }
        else if(distanceFromPlayer < 150)
        {
            controller.DamagePlayer(1);
        }
    }  
}
