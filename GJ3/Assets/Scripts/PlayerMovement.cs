using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Makes privates public
    /// </summary>
    private Vector3 speed;

	// Update is called once per frame
	void Update ()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    Vector3 target = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        //
        //    transform.Translate(Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime) - Bear.transform.position);
        //}

        Vector3 pos = transform.position;
       
        if (Input.GetKeyDown("left"))
        {
            pos.z -= 5.0f * Time.deltaTime;
        }

        if (Input.GetKeyDown("right"))
        {
            pos.z += 5.0f * Time.deltaTime;
        }

        if (Input.GetKeyDown("space"))
        {
            
        }

	}
}
