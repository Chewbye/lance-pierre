using UnityEngine;
using System.Collections;
using System;

public class PositionCible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		System.Random rnd = new System.Random();
		int x = rnd.Next(3, 27);
		int y = rnd.Next(-5, 14); 
		Vector3 position = new Vector3( x, y, 0 );
		transform.position = position;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
