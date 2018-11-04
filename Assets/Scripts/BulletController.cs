using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour {
	public Rigidbody rb;
	public Vector3 dir;
       
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = new Color(255f, 0f, 0f);
		rb.velocity = dir;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
