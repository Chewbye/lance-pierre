using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Classe utilisee pour limiter le temps que peut mettre le joeur pour tirer
public class GestionTemps : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		GameController.Jeu.Temps_Restant_Courant = GameController.Jeu.Config.Delai_lancer_projectile;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!GameController.Jeu.Tir_Effectue)
		{
			GameController.Jeu.Temps_Restant_Courant -= Time.deltaTime;
			if (GameController.Jeu.Temps_Restant_Courant <= 0.0f)
			{
				Conclure();
			}
		}
		else
		{
			Debug.Log("Arret du compte a rebours : Le joueur a lance le projectile");
		}
	}

	// Le joueur n'a pas joué avant le temps autorise
	void Conclure() 
	{
		Debug.Log("Temps écoulé pour tirer !");
		// La cible est donc manquee
		GameController.Jeu.Cible_Manquee = true;
	}
}
