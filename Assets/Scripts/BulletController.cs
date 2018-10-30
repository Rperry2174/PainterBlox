using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
