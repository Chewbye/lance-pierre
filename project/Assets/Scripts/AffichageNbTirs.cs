using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffichageNbTirs : MonoBehaviour {
	
	Text txt;
	
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text= GameController.Jeu.Tir_courant + " tir(s) effectué(s) sur " + GameController.Jeu.Config.Nb_lancers;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text= GameController.Jeu.Tir_courant + " tir(s) effectué(s) sur " + GameController.Jeu.Config.Nb_lancers; 
	}
}