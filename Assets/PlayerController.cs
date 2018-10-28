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
	
	// Use this for initialization
	void Start () {
		joystick = FindObjectOfType<Joystick>();
		joybutton = FindObjectOfType<Joybutton>();
        rb = GetComponent<Rigidbody>();
		lastPlayerDirection = new Vector3(joystick.Horizontal * velocityFactor,
                                          rb.velocity.y,
                                          joystick.Vertical * velocityFactor);
	}
	
	// Update is called once per frame
	void Update () {


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
		}
		else
		{
			rb.velocity = Vector3.zero;
		}

        if(!jump && joybutton.Pressed)
		{
			jump = true;
			//rb.velocity += Vector3.up * velocityFactor;
			Vector3 updatedPosition = new Vector3(gameObject.transform.position.x, 
			                                      0.0f,
			                                      gameObject.transform.position.z);
			Fire(updatedPosition, gameObject.transform.rotation, SnapJoystickDirection(lastPlayerDirection));
		}

		if(jump && !joybutton.Pressed)
		{
			jump = false;
		}

		//Twist();
	}

	public Vector3 SnapJoystickDirection(Vector3 currentDir)
	{
		Vector3 baseAngle = new Vector3(1.0f * velocityFactor, 0.0f, 0.0f);
		float currentAngle = Vector3.SignedAngle(baseAngle, currentDir, Vector3.down);
                
		if (Mathf.Abs(currentAngle) <= 45.0f && Mathf.Abs(currentAngle) >= 0)
		{
			return new Vector3(1.0f * velocityFactor, 0.0f, 0.0f);
		}
		else if (currentAngle > 45.0f && currentAngle <= 135.0f)
		{
			return new Vector3(0.0f, 0.0f, 1.0f * velocityFactor);
		}
		else if (Mathf.Abs(currentAngle) >= 135.0f && Mathf.Abs(currentAngle) <= 180.0f)
		{
			return new Vector3(-1.0f * velocityFactor, 0.0f, 0.0f);
		} 
		else if (currentAngle > -135.0f && currentAngle <= -45.0f)
		{
			return new Vector3(0.0f, 0.0f, -1.0f * velocityFactor);
		}
		else
		{
            // Figure out how to do this the right way in Unity
			Debug.Log("ERRRORRRRRRRR SOME CASE IS UNHANDLED" + baseAngle);
			return baseAngle;
		}
	}

	void Fire(Vector3 pos, Quaternion rot, Vector3 dir)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
			pos,
			rot);

        // Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = dir * 6;

        // Destroy the bullet after 8 seconds
        Destroy(bullet, 8.0f);
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
                //Debug.Log(curRot.y);
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
			//Debug.Log("gameobject" + gameObject.transform.rotation);         
			gameObject.transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
        }
    }
}
