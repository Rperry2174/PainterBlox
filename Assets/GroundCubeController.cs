using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCubeController : MonoBehaviour {
	public Color originalBoxColor = new Color(140f, 101f, 51f);
	public Color triggerBoxColor = new Color(0f, 0f, 255f);
	public BoxCollider triggerCollider;
    public BoxCollider groundCollider;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = originalBoxColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeColor (Color newColor) {
		gameObject.GetComponent<Renderer>().material.color = newColor;
	}

	private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("entered");
		ChangeColor(triggerBoxColor);
    }

	private void OnTriggerExit(Collider other)
    {
		//Debug.Log("entered");
		ChangeColor(originalBoxColor);
    }


}
