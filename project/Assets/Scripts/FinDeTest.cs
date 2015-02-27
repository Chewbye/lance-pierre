using UnityEngine;
using System.Collections;

public class FinDeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onMenu() {
		Application.LoadLevel ("mainMenu");
	}

	public void onResultats() {
		Application.LoadLevel ("results");
	}
}
