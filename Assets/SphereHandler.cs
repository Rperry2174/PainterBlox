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
		rb.velocity = new Vector3(joystick.Horizontal * velocityFactor,
										 rb.velocity.y,
		                          joystick.Vertical * velocityFactor);

		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if(!jump && joybutton.Pressed)
		{
			jump = true;
			rb.velocity += Vector3.up * velocityFactor;
		}

		if(jump && !joybutton.Pressed)
		{
			jump = false;
		}
	}
}
