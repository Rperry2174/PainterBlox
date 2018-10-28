using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

	public GameObject[,] objects;
	public GameObject groundCube;
    
	// Use this for initialization
	void Start () {
		objects = new GameObject[8, 8];

		for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				objects[i, j] = (GameObject)Instantiate(groundCube, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
