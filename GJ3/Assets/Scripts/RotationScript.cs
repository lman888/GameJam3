﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotationScript : MonoBehaviour {
    public GameObject Capsule;
    private bool stopworld;
    [Range(0.0f, 100.0f)] [Tooltip("Used to control how fast the game world rotates normally (In degrees per second)")]
    public float normalSpeed = 10.0f;

    [Tooltip("How much does the world speed up per second (in degrees per second per second")]
    public float speedIncrementPerSecond = 0.01f;

    [Range(0.0f, 100.0f)]
    [Tooltip("Used to control the max speed cap of the world")]
    public float maxspeed = 10.0f;

    [Range(0.0f, 100.0f)]
    [Tooltip("Used to control how fast the game world rotates when boosting (In degrees per second)")]
    public float boostSpeed = 10.0f;

    [Tooltip("Specifies how long boost stays active for once collected (In seconds)")]
    public float boostTime = 2.0f;

    bool BoostActive;
    public int health = 5;

    private float bonusSpeed = 0.0f;

    public float StopSpeed = 0.0f;
    // capsule movement
	// Use this for initialization
	void Start ()
    {
        stopworld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Boost();
        }

        float speed = normalSpeed;
        if (BoostActive) speed = boostSpeed;


        bonusSpeed += speedIncrementPerSecond * Time.deltaTime;
        speed += bonusSpeed;
        if (speed >= maxspeed)
        {
            speed = maxspeed;
            Debug.Log("world stop");
        }

        if (stopworld == true)
        {
            speed = StopSpeed;
        }
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * speed);
	}

    public void Boost()
    {
        //on pickup of basket, speed *2
        BoostActive = true;
        StartCoroutine(StopBoost());
    }

    private IEnumerator StopBoost()
    {
        yield return new WaitForSeconds(boostTime);
        BoostActive = false;
    }

    public void Stop()
    {
        stopworld = true;
    }
}