using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Makes privates public
    /// </summary>
    public float speed = 0.5f;

	// Update is called once per frame
	void Update ()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    Vector3 target = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        //
        //    transform.Translate(Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime) - Bear.transform.position);
        //}

        if(Input.GetMouseButton(0) == true)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 mouseViewPos = Camera.main.ScreenToViewportPoint(new Vector3(mousePos.x, 0, 10));
           //if(mouseViewPos.x < 0.4 || mouseViewPos.x > 0.6)
           //{
                Vector3 pos = transform.position;
                float speedScale = Mathf.Abs(mouseViewPos.x - 0.5f) * 2.0f;
                speedScale *= speedScale;
                if (mouseViewPos.x < 0.5f)
                {
                    pos.z -= 5.0f * speedScale * Time.deltaTime;
                }
                else
                {
                    pos.z += 5.0f * speedScale * Time.deltaTime;

                }
                transform.position = pos;
            //}
        }
	}
}
