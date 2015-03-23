using UnityEngine;
using System.Collections;

// Classe utilisee pour gerer la collision entre le projectile et le sol
public class GestionCollisionSol : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{

	}

	// Si le projectile touche le sol
	void OnCollisionEnter2D (Collision2D collision) 
	{
		// La cible est donc manquee
		Conclure();
	}
	
	void Conclure() 
	{
		Debug.Log("La balle a touche le sol !");
		// La cible est donc manquee
		GameController.Jeu.Cible_Manquee = true;
	}
}
