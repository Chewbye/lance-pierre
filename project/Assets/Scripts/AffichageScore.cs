using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffichageScore : MonoBehaviour {
	
	Text txt;
	
	// Use this for initialization
	void Start () 
	{
		if(GameController.Jeu.Config.Afficher_le_score)
		{
			txt = gameObject.GetComponent<Text>(); 
			txt.text= "Score : " + GameController.Jeu.Score;
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
		if(GameController.Jeu.Config.Afficher_le_score)
		{
			txt = gameObject.GetComponent<Text>(); 
			txt.text= "Score : " + GameController.Jeu.Score;
		}
		else
		{
			txt = gameObject.GetComponent<Text>(); 
			txt.text= "";
		}
	}
}