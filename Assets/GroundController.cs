using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

	public GameObject[,] objects;
	public GameObject groundCube;
	private Color oddColor = new Color(0.0f, 0.0f, 0.0f);
	private Color evenColor = new Color(255.0f, 255.0f, 255.0f);
    
	// Use this for initialization
	void Start () {
		objects = new GameObject[8, 8];
		int colorCounter = 0;

		for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				GameObject gc = (GameObject)Instantiate(groundCube, new Vector3(i, 0, j), Quaternion.identity);
				gc.GetComponent<Renderer>().material.color = GetColor(i, j);
				gc.GetComponent<GroundCubeController>().index = new Vector2(i, j);

				objects[i, j] = gc;
				colorCounter += 1;
            }
        }
	}

    Color GetColor(int row, int col)
	{
		bool rowIsEven = row % 2 == 0;
		bool colIsEven = col % 2 == 0;

		if((rowIsEven && colIsEven) || (!rowIsEven && !colIsEven))
		{
			return evenColor;
		}
		else
		{
			return oddColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
