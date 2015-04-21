using UnityEngine;
using System.Collections;

// Classe utilisee pour gerer la collision entre le projectile et la cible
public class GestionCollisionCible : MonoBehaviour 
{
	private bool particuleDejaJouee;

	// Use this for initialization
	void Start () 
	{
		particuleDejaJouee = false;
	}

	// Si le projectile touche la cible
	void OnCollisionEnter2D (Collision2D collision) 
	{
		Conclure();
	}

	void Conclure() 
	{
		// Le tir est donc reussi
		GameController.Jeu.Cible_Touchee = true;

		// Si nous ne sommes pas pendant ou apres une evaluation
		if((GameController.Jeu.Evaluation_En_Cours || GameController.Jeu.Evaluation_Effectuee) 
		   && !GameController.Jeu.Config.Condition_De_Controle)
		{
			particleSystem.Stop();
		}
		else if (!particuleDejaJouee)
		{
			if(GameController.Jeu.Config.Afficher_effet_destruction_cible)
			{
				// Affichage des particules autour de la cible
				particleSystem.Play();
				particuleDejaJouee = true;
			}
		}
	}
}
