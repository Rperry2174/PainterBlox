using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class PlayerController : MonoBehaviour {

	protected Joystick joystick;
	protected Joybutton joybutton;
    
	protected bool jump;
	public GameObject bulletPrefab;
	public float velocityFactor = 2.5f;
	public float speed;
	public Vector3 lastPlayerDirection;
	public Rigidbody rb;
	public Animator animator;
	public float bufferFactor = 1.5f;
	public Vector3 buffer;

	// Use this for initialization
	void Start () {
		joystick = FindObjectOfType<Joystick>();
		joybutton = FindObjectOfType<Joybutton>();
        rb = GetComponent<Rigidbody>();
		lastPlayerDirection = new Vector3(joystick.Horizontal * velocityFactor,
                                          rb.velocity.y,
                                          joystick.Vertical * velocityFactor);
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if ( !gameObject.GetComponentInParent<PlayerUnitController>().isLocalPlayer )
		{
			return;
		}


        Vector3 dir = new Vector3(joystick.Horizontal * velocityFactor,
                                  rb.velocity.y,
                                  joystick.Vertical * velocityFactor);

		Vector3 adjustedDir = SnapJoystickDirection(dir);

		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);        
		rb.AddForce(movement * speed);
        
		//Debug.Log("gameobject" + dir);
		if(dir.x != 0 && dir.z != 0)
		{
			rb.velocity = adjustedDir;
			transform.rotation = Quaternion.LookRotation(adjustedDir);
			lastPlayerDirection = dir;
			animator.SetTrigger("Walk");
		}
		else
		{
			rb.velocity = Vector3.zero;
			animator.SetTrigger("Idle");
		}

        if(!jump && joybutton.Pressed)
		{
			jump = true;
			//rb.velocity += Vector3.up * velocityFactor;
			Vector3 updatedPosition = new Vector3(gameObject.transform.position.x, 
			                                      0.50f,
			                                      gameObject.transform.position.z);
			Fire(updatedPosition, gameObject.transform.rotation, SnapJoystickDirection(lastPlayerDirection));
		}

		if(jump && !joybutton.Pressed)
		{
			jump = false;
		}
	}

	public Vector3 SnapJoystickDirection(Vector3 currentDir)
	{
		Vector3 baseAngle = new Vector3(1.0f * velocityFactor, 0.0f, 0.0f);
		float currentAngle = Vector3.SignedAngle(baseAngle, currentDir, Vector3.down);
                
		if (Mathf.Abs(currentAngle) <= 45.0f && Mathf.Abs(currentAngle) >= 0)
		{
			buffer = new Vector3(bufferFactor, 0.0f, 0.0f);
			return new Vector3(1.0f * velocityFactor, 0.0f, 0.0f);
		}
		else if (currentAngle > 45.0f && currentAngle <= 135.0f)
		{
			buffer = new Vector3(0.0f, 0.0f, bufferFactor);
			return new Vector3(0.0f, 0.0f, 1.0f * velocityFactor);
		}
		else if (Mathf.Abs(currentAngle) >= 135.0f && Mathf.Abs(currentAngle) <= 180.0f)
		{
			buffer = new Vector3(-bufferFactor, 0.0f, 0.0f);         
			return new Vector3(-1.0f * velocityFactor, 0.0f, 0.0f);
		} 
		else if (currentAngle > -135.0f && currentAngle <= -45.0f)
		{
			buffer = new Vector3(0.0f, 0.0f, -bufferFactor);                  
			return new Vector3(0.0f, 0.0f, -1.0f * velocityFactor);
		}
		else
		{
            // Figure out how to do this the right way in Unity
			Debug.Log("ERROR SOME CASE IS UNHANDLED" + baseAngle);
			return baseAngle;
		}
	}

	void Fire(Vector3 pos, Quaternion rot, Vector3 dir)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
			pos + buffer,
			rot);

		bullet.transform.parent = gameObject.transform.parent;

        // Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = dir * 6;

        // Destroy the bullet after 4 seconds
        Destroy(bullet, 4.0f);
    }
}
