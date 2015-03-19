using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffichagePhase : MonoBehaviour {
	
	Text txt;
	
	// Use this for initialization
	void Start () 
	{

		if(GameController.Jeu.isPretest)
		{
			txt = gameObject.GetComponent<Text>(); 
			txt.text= "PHASE DE TEST";
		}
		else if(GameController.Jeu.isPretest)
		{
			txt = gameObject.GetComponent<Text>(); 
			txt.text= "PHASE D'ENTRAINEMENT";
		}
		else
		{
			txt = gameObject.GetComponent<Text>(); 
			txt.text= "";
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}