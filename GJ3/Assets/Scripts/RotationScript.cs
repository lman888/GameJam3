using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {
    
    [Range(10.0f, 100.0f)]
    [Tooltip("Used to control how fast the game world rotates normally (In degrees per second)")]
    public float normalSpeed = 10.0f;

    [Tooltip("How much does the world speed up per second (in degrees per second per second")]
    public float speedIncrementPerSecond = 0.01f;

    [Range(10.0f, 100.0f)]
    [Tooltip("Used to control how fast the game world rotates when boosting (In degrees per second)")]
    public float maxspeed = 10.0f;

    [Range(10.0f, 100.0f)]
    [Tooltip("Used to control how fast the game world rotates when boosting (In degrees per second)")]
    public float boostSpeed = 10.0f;

    [Tooltip("Specifies how long boost stays active for once collected (In seconds)")]
    public float boostTime = 2.0f;

    bool BoostActive = false;


    private float bonusSpeed = 0.0f;

	// Use this for initialization
	void Start ()
    {
        //speed = 10;
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
}

