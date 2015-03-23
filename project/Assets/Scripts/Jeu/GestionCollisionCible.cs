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

		// Affichage des particules autour de la cible
		particleSystem.Play();
	}
}
