using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffichageTirs : MonoBehaviour {
	
	Text txt;
	
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text= "Tir(s) réussi(s) : " +GameController.Jeu.Nb_cible_touchees + "\t\tTir(s) ratés : " + GameController.Jeu.Nb_cible_manquees;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "Tir(s) réussi(s) : " + GameController.Jeu.Nb_cible_touchees + "\t\tTir(s) raté(s) : " + GameController.Jeu.Nb_cible_manquees;
	}
}