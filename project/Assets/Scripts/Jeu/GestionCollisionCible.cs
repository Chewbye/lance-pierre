using UnityEngine;
using System.Collections;

// Classe utilisee pour gerer la collision entre le projectile et la cible
public class GestionCollisionCible : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{

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
		if((GameController.Jeu.Evaluation_En_Cours || GameController.Jeu.Evaluation_Effectuee) && !GameController.Jeu.Config.Condition_De_Controle)
		{
			particleSystem.Stop();
		}
		else
		{
			// Affichage des particules autour de la cible
			particleSystem.Play();
		}
	}
}
