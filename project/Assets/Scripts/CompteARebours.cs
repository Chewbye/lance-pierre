using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompteARebours : MonoBehaviour {

	float tempsRestant; // En seconde
	
	// Use this for initialization
	void Start () 
	{
		tempsRestant = GameController.Jeu.Config.Delai_lancer_projectile;
	}
	
	// Update is called once per frame
	void Update () 
	{
		tempsRestant -= Time.deltaTime;
		if (tempsRestant <= 0.0f)
		{
			Debug.Log("Temps écoulé !");
			Reset ();
		}
	}

	void Reset () 
	{
		// On indique que le tir courant est manqué
		GameController.Jeu.Reussiste_Tirs.Add(false);
		
		// On incrémente le tir courant
		GameController.Jeu.Tir_courant++;
		
		//	On recharge la meme scène
		Application.LoadLevel (Application.loadedLevel);
	}
}
