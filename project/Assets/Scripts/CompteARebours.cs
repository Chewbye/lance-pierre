using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompteARebours : MonoBehaviour {

	public float tempsRestant = 15.0f; // En seconde
	Text txt;
	
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text = "Temps restant : " + tempsRestant + " secondes";
	}
	
	// Update is called once per frame
	void Update () {

		tempsRestant -= Time.deltaTime;
		txt.text= "Temps restant : " + tempsRestant + " secondes";
		if (tempsRestant <= 0.0f)
		{
			Reset ();
		}; 
	}

	void Reset () {
		// On incrémente le nombre de cibles manquées
		GameController.Jeu.Nb_cible_manquees ++;
		
		// On indique que le tir courant est manqué
		GameController.Jeu._Un_tir[GameController.Jeu.Tir_courant] = false;
		
		// On incrémente le tir courant
		GameController.Jeu.Tir_courant++;
		
		Debug.Log(GameController.Jeu);
		
		//	On recharge la meme scène
		Application.LoadLevel (Application.loadedLevel);
	}
}
