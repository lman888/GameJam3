using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    private Vector3 offset;             ///Private variable to store the offset distance between the player and camera

    private void Start()
    {
        //Calculate and store the offset value by getting the distance beteen the player's position and camera's position
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        //Set the position of the Camera's transform to be the same as the Player's, but offset by the calculated offset distance
        transform.position = Player.transform.position + offset;
	}
}
