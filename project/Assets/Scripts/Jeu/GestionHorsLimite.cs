using UnityEngine;
using System.Collections;

// Classe utilisee pour gerer les sorties du projectile hors de la zone de jeu
public class GestionHorsLimite : MonoBehaviour 
{
	public Rigidbody2D projectile;			//	The rigidbody of the projectile
	public float resetSpeed = 0.025f;		//	The angular velocity threshold of the projectile, below which our game will reset
	
	private float resetSpeedSqr;			//	The square value of Reset Speed, for efficient calculation
	private SpringJoint2D spring;			//	The SpringJoint2D component which is destroyed when the projectile is launched
	
	void Start ()
	{

	}
	
	void Update () 
	{

	}

	// Detection des objets quittant la zone de jeu
	void OnTriggerExit2D (Collider2D other) 
	{
		// Si l'objet est bien le projectile du joueur
		if (other.rigidbody2D == projectile) 
		{
			Conclure();
		}
	}

	// Le joueur a lance le projectile hors de la zone de jeu
	void Conclure() 
	{
		Debug.Log("Projectile tire hors de la zone de jeu!");
		// La cible est donc manquee
		GameController.Jeu.Cible_Manquee = true;
	}
}
