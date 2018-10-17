using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class SphereHandler : MonoBehaviour {

	protected Joystick joystick;
	protected Joybutton joybutton;
    
	protected bool jump;
	public float velocityFactor = 2.5f;
	public float speed;
	
	// Use this for initialization
	void Start () {
		joystick = FindObjectOfType<Joystick>();
		joybutton = FindObjectOfType<Joybutton>();
	}
	
	// Update is called once per frame
	void Update () {
		var rb = GetComponent<Rigidbody>();
        Vector3 dir = new Vector3(joystick.Horizontal * velocityFactor,
                                  rb.velocity.y,
                                     joystick.Vertical * velocityFactor);
		
		rb.velocity = dir;

		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);        
		rb.AddForce(movement * speed);
        
		Debug.Log("gameobject" + dir);
		transform.rotation = Quaternion.LookRotation(dir);

        if(!jump && joybutton.Pressed)
		{
			jump = true;
			rb.velocity += Vector3.up * velocityFactor;
		}

		if(jump && !joybutton.Pressed)
		{
			jump = false;
		}

		//Twist();
	}

	void Twist()
    {
		float h1 = Input.GetAxis("Horizontal");
        float v1 = Input.GetAxis("Vertical");

        if (h1 == 0f && v1 == 0f)
        { // this statement allows it to recenter once the inputs are at zero 
			Vector3 curRot = gameObject.transform.localEulerAngles; // the object you are rotating
            Vector3 homeRot;
            if (curRot.y > 180f)
            { // this section determines the direction it returns home 
                Debug.Log(curRot.y);
                homeRot = new Vector3(0f, 359.999f, 0f); //it doesnt return to perfect zero 
            }
            else
            {                                                                      // otherwise it rotates wrong direction 
                homeRot = Vector3.zero;
            }
			gameObject.transform.localEulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);
        }
        else
        {
			Debug.Log("gameobject" + gameObject.transform.rotation);         
			gameObject.transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
        }
    }
}
