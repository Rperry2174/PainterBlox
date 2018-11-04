using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCubeController : MonoBehaviour {
	public Vector3 originalBoxPosition;
	public Color originalBoxColor;
	public Color triggerBoxColor = new Color(0f, 0f, 255f);
	public Vector2 index;
	public bool isFalling = false;
	public bool hasPlayer = false;

	// Use this for initialization
	void Start () {
		//gameObject.GetComponent<Renderer>().material.color = originalBoxColor;
		originalBoxColor = gameObject.GetComponent<Renderer>().material.color;
		originalBoxPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFalling)
		{
			gameObject.transform.Translate(Vector3.down * Time.deltaTime);
		}
	}

	public void ChangeColor () {
		if (hasPlayer)
		{
			Debug.Log("GroundCubeController::hasPLayer" + hasPlayer);
			gameObject.GetComponent<Renderer>().material.color = triggerBoxColor;	
		}
		else
		{
			gameObject.GetComponent<Renderer>().material.color = originalBoxColor;            
		}
	}

	public IEnumerator Respawn()
	{
		//print("start: " + Time.time);
        yield return new WaitForSeconds(5);
		isFalling = false;
		gameObject.transform.position = originalBoxPosition;
	}

}
