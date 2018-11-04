using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCubeController : MonoBehaviour
{
    public Vector3 originalBoxPosition;
    public Color originalBoxColor;
    public Color triggerBoxColor = new Color(0f, 0f, 255f);
    private BoxCollider triggerCollider;
    private BoxCollider groundCollider;
    public GameObject groundBox;
    public Vector2 index;

    void Start()
    {        
		triggerCollider = gameObject.GetComponent<BoxCollider>();

		groundCollider = groundBox.GetComponent<BoxCollider>();
        originalBoxColor = groundBox.GetComponent<Renderer>().material.color;
        originalBoxPosition = groundBox.transform.position;
    }

    void Update()
    {
		// todo: move to start
		GroundCubeController groundCubeController = gameObject.transform.parent.GetComponentInChildren<GroundCubeController>();
		if (groundCubeController.isFalling)
		{
			//triggerCollider.isTrigger = false;
		}
		else
		{
			//triggerCollider.isTrigger = true;
		}
	}

    private void OnTriggerEnter(Collider col)
    {
		triggerCollider = gameObject.GetComponent<BoxCollider>();
		GroundCubeController groundCubeController = gameObject.transform.parent.GetComponentInChildren<GroundCubeController>();
        
        if (col.gameObject.tag == "Player")
        {
			//Debug.Log("AirCubeController::OnTriggerEnter::Player" + col.gameObject);
			//Debug.Log("AirCubeController::OnTriggerEnter::Player.position" + col.gameObject.transform.position + Vector3.up);
			//Debug.Log("AirCubeController::OnTriggerEnter::Player.triggerCollider.bounds" + triggerCollider.bounds);
			groundCubeController.hasPlayer = triggerCollider.bounds.Contains(col.gameObject.transform.position + Vector3.up);
			groundCubeController.ChangeColor();
        }
        

		if (col.gameObject.tag == "Bullet" && !groundCubeController.hasPlayer)
        {

			groundCubeController.hasBullet = triggerCollider.bounds.Contains(col.gameObject.transform.position);         
			if (groundCubeController.hasBullet)
			{
				groundCubeController.isFalling = true;
				groundCubeController.StartCoroutine(groundCubeController.Respawn());
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
		GroundCubeController groundCubeController = gameObject.transform.parent.GetComponentInChildren<GroundCubeController>();

        if (col.gameObject.tag == "Player")
        {
			groundCubeController.hasPlayer = triggerCollider.bounds.Contains(col.gameObject.transform.position + Vector3.up);
			groundCubeController.ChangeColor();
        }
    }

    private void OnTriggerExit(Collider col)
    {
		GroundCubeController groundCubeController = gameObject.transform.parent.GetComponentInChildren<GroundCubeController>();

        if (col.gameObject.tag == "Player")
        {
			groundCubeController.hasPlayer = false;
			groundCubeController.ChangeColor();
        }
        //Debug.Log("entered");
    }
}
