using UnityEngine;
using System.Collections;
using System;

public class PositionTailleCible : MonoBehaviour {

	public GameObject catapulte;
	private double distance;

	// Use this for initialization
	void Start () {
		// CHANGEMENT DE POSITION
		// On place aléatoirement la cible dans une zone donnée
		/*System.Random rnd = new System.Random();
		int x = rnd.Next(3, 27);
		int y = rnd.Next(-5, 14); 
		Vector3 positionCible = new Vector3( x, y, 0 );
		transform.position = positionCible;

		// On récupère la position de la catapulte
		Vector3 positionCatapulte = catapulte.transform.position;

		// On calcule la distance entre la catapulte et la cible
		distance = Math.Sqrt (Math.Pow(((double)positionCible.x - (double)positionCatapulte.x), 2) + Math.Pow(((double)positionCible.y - (double)positionCatapulte.y), 2));

		// On enregistre la distance dans le tableau des distances
		GameController.Jeu._Une_distance [GameController.Jeu.Tir_courant] = distance;

		// CHANGEMENT DE TAILLE
		// On fait varier la taille entre *-2 et *2
		double taille = rnd.NextDouble() * (2 + 2) - 2; // (maximum - minimum) + minimum;
		transform.localScale = new Vector3((float)taille, (float)taille, transform.localScale.z);

		// On enregistre le coefficient multiplicateur
		GameController.Jeu._Une_tailleCible [GameController.Jeu.Tir_courant] = taille;*/
	}

	// Update is called once per frame
	void Update () {
	
	}
}
