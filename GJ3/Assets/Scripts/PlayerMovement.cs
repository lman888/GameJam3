using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public GameObject GameWorld;
    public RotationScript rotationScript;

    //jumping
    public float moveSpeed;
    public float jumpForce;
    public float jumpSpeed;
    private bool isJumping = false;
    private float jumpT = 0.0f;
    private float jumpOffset = 0.0f;
    private Vector3 groundOffset;



    /* health system */
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;
    public GameObject Health4;
    public GameObject Health5;
    public bool Invincibility;
    public float ImmunityTime = 0.02f;
    public bool isAlive = false;
    public int Health;
    public bool HealthIncreasetimerLimit;
    public float HealthlimitTime;

    //How fast/slow the player falls
    public float gravityScale;
    public CharacterController controller;
    private Vector3 moveDirection;
    private Transform pivotTransform;
    //rotation lock
    public float MaxRotationAngle = 8.0f;




    private void Start()
    {
        rotationScript.GetComponent("Stop");

        pivotTransform = transform.parent;

        HealthIncreasetimerLimit = false;
        Invincibility = false;
        //controller = GetComponent<CharacterController>();
        Health = 5;
        Health1.SetActive(true);
        Health2.SetActive(true);
        Health3.SetActive(true);
        Health4.SetActive(true);
        Health5.SetActive(true);
        isAlive = true;

        Debug.Log("health1 Active");
    }

    // Update is called once per frame
    void Update()
    {
        //player detection set on at all times
        GetComponent<Rigidbody>().WakeUp();

        pivotTransform.Rotate(Vector3.right, -Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Space.World);
        Vector3 rot = pivotTransform.rotation.eulerAngles;
        if (rot.x < 180)
        {
            rot.x = Mathf.Min(rot.x, MaxRotationAngle);
        }
        if (rot.x > 180)
        {
            rot.x = Mathf.Max(rot.x, 360 - MaxRotationAngle);
        }

        Quaternion qRot = Quaternion.Euler(rot);
        pivotTransform.rotation = qRot;


        //Arrow keys to move
        //moveDirection = new Vector3(Input.GetAxis("Horizontal")  * moveSpeed * Time.deltaTime, 0f);
        //
        ////Space to jump
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            isJumping = true;
            groundOffset = transform.localPosition;
            jumpT = 0.0f;
        }

        if (isJumping == true)
        {
            //times buy Mathf.PI somewhere here for designer sanity!
            jumpT += (Time.deltaTime * Mathf.PI) / jumpSpeed;
            if (jumpT >= Mathf.PI)
            {
                isJumping = false;
                transform.localPosition = groundOffset;
            }
            else
            {
                transform.localPosition = groundOffset + new Vector3(0, jumpForce * Mathf.Sin(jumpT), 0);
            }
        }

        HealthCheck();

        if (isAlive == false)
        {
            GAMEOVER();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" && Invincibility == false)
        {
            Immunity();
            Debug.Log("ouch: " + other.name);
            Health = Health - 1;
        }

        if (other.tag == "Health" && HealthIncreasetimerLimit == false)
        {
            OneHealthUp();
            if (Health < 5)
            {
                Debug.Log("Health up");
                Health = Health + 1;
            }

            if (Health >= 5)
            {
                Debug.Log("Health cannot go any higher");
            }
        }

        if (other.tag == "Boost")
        {
            GameWorld.GetComponent<RotationScript>().Boost();
        }

    }
    //Immunity
    public void Immunity()
    {
        //on pickup of basket, speed *2
        Invincibility = true;
        StartCoroutine(Immunityframe());
    }

    private IEnumerator Immunityframe()
    {
        yield return new WaitForSeconds(ImmunityTime);
        Invincibility = false;
    }

    public void OneHealthUp()
    {
        //on pickup of basket, speed *2
        HealthIncreasetimerLimit = true;
        StartCoroutine(HealthIncreasetimer());
    }

    private IEnumerator HealthIncreasetimer()
    {
        yield return new WaitForSeconds(HealthlimitTime);
        HealthIncreasetimerLimit = false;
    }

    public void HealthCheck()
    {
        Debug.Log(Health);
        if (Health == 5)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(true);
            Health4.SetActive(true);
            Health5.SetActive(true);
        }

        if (Health == 4)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(true);
            Health4.SetActive(true);
            Health5.SetActive(false);
        }

        if (Health == 3)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(true);
            Health4.SetActive(false);
            Health5.SetActive(false);
        }

        if (Health == 2)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(false);
            Health4.SetActive(false);
            Health5.SetActive(false);
            Debug.Log(Health);
        }

        if (Health == 1)
        {
            Health1.SetActive(true);
            Health2.SetActive(false);
            Health3.SetActive(false);
            Health4.SetActive(false);
            Health5.SetActive(false);
        }

        if (Health <= 0)
        {
            Health1.SetActive(false);
            Health2.SetActive(false);
            Health3.SetActive(false);
            Health4.SetActive(false);
            Health5.SetActive(false);
            Health = 0;
            isAlive = false;
        }
    }

    public void GAMEOVER()
    {
        rotationScript.Stop();
    }
}


