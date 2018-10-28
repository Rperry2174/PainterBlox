using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCubeController : MonoBehaviour {
	public Vector3 originalBoxPosition;
	public Color originalBoxColor = new Color(140f, 101f, 51f);
	public Color triggerBoxColor = new Color(0f, 0f, 255f);
	public BoxCollider triggerCollider;
    public BoxCollider groundCollider;
	public bool isFalling = false;

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

	void ChangeColor (Color newColor) {
		gameObject.GetComponent<Renderer>().material.color = newColor;
	}

	private void OnTriggerEnter(Collider col)
    {
		if(col.gameObject.tag == "Bullet")
		{
			isFalling = true;
			StartCoroutine(Respawn());
		}
		ChangeColor(triggerBoxColor);
    }

	private void OnTriggerExit(Collider other)
    {
		//Debug.Log("entered");
		ChangeColor(originalBoxColor);
    }

	IEnumerator Respawn()
	{
		//print("start: " + Time.time);
        yield return new WaitForSeconds(5);
		isFalling = false;
		gameObject.transform.position = originalBoxPosition;
	}

}
