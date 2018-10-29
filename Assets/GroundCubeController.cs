using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCubeController : MonoBehaviour {
	public Vector3 originalBoxPosition;
	public Color originalBoxColor = new Color(140f, 101f, 51f);
	public Color triggerBoxColor = new Color(0f, 0f, 255f);
	public BoxCollider triggerCollider;
    public BoxCollider groundCollider;
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

	void ChangeColor () {
		if (hasPlayer)
		{
			gameObject.GetComponent<Renderer>().material.color = triggerBoxColor;	
		}
		else
		{
			gameObject.GetComponent<Renderer>().material.color = originalBoxColor;            
		}
	}

	private void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "Player")
		{
			hasPlayer = triggerCollider.bounds.Contains(col.gameObject.transform.position);
			ChangeColor();            
		}

		if (col.gameObject.tag == "Bullet" && !hasPlayer)
        {
            isFalling = true;
            StartCoroutine(Respawn());
        }
    }

	private void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Player")
        {
            hasPlayer = triggerCollider.bounds.Contains(col.gameObject.transform.position);
            ChangeColor();
        }          
		//print("hasPlayer?" + hasPlayer + "\nIndex " + index + "_"+ col.gameObject.transform.position);
	}

	private void OnTriggerExit(Collider col)
    {
		if (col.gameObject.tag == "Player")
        {
            hasPlayer = false;
			ChangeColor();
        }
		//Debug.Log("entered");
    }

	IEnumerator Respawn()
	{
		//print("start: " + Time.time);
        yield return new WaitForSeconds(5);
		isFalling = false;
		gameObject.transform.position = originalBoxPosition;
	}

}
