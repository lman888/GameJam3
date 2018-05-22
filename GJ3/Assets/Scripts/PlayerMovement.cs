using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    //How fast/slow the player falls
    public float gravityScale;
    public CharacterController controller;

    private Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Arrow keys to move
        moveDirection = new Vector3(Input.GetAxis("Horizontal")  * moveSpeed * Time.deltaTime, 0f);

        //Space to jump
        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        //Adds gravity as the player falls
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
	}
}
