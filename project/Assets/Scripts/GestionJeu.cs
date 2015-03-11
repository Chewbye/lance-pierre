using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestionJeu : MonoBehaviour 
{
	public GameObject projectile;
	public GameObject cible;
	public GameObject catapulte;


	// Use this for initialization
	void Start () 
	{
		// INITIALISATION DES ATTRIBUTS DE JEU
		if(GameController.Jeu.Tirs_A_Realiser.Count == 0 && GameController.Jeu.Tirs_Realises.Count == 0)// Si les triplets n'ont pas déjà été générés
		{
			Debug.Log("Génération des combinaisons des tirs ...");
			GameController.Jeu.Nb_lancers = GameController.Jeu.Config.Nb_lancers;
			int nbCombinaisonsGenerees = 0;
			// Calcul de toutes les combinaisons possibles de Position de Cible, Taille de Cible et Taille de Projectile
			for(int i = 0; i < GameController.Jeu.Config.NB_series; i++)
			{
				for(int j = 0; j <GameController.Jeu.Config.Positions_Cibles.Count; j++)
				{
					for( int k = 0; k < GameController.Jeu.Config.Tailles_Cibles.Count; k++)
					{
						for(int l = 0; l < GameController.Jeu.Config.Projectiles.Count; l++)
						{
							GameController.Jeu.Tirs_A_Realiser.Add(new TripletTirs(GameController.Jeu.Config.Projectiles[l],
							                                                       GameController.Jeu.Config.Positions_Cibles[j],
							                                                       GameController.Jeu.Config.Tailles_Cibles[k]));
							nbCombinaisonsGenerees++;
						}
					}
				}
			}
			Debug.Log("Nombre de combinaisons générées : " + nbCombinaisonsGenerees);
		}

		if(GameController.Jeu.Tirs_Realises.Count < GameController.Jeu.Nb_lancers) // Si nous ne sommes pas en fin de partie
		{
			// CHOIX D'UN TIR A REALISER
			// Choix d'un tir
			System.Random rnd = new System.Random();
			int rang = rnd.Next(0, GameController.Jeu.Tirs_A_Realiser.Count);
			TripletTirs tirAFaire = GameController.Jeu.Tirs_A_Realiser[rang];
			Debug.Log("Tir choisi (DistanceX=" + tirAFaire.Position_Cible.DistanceX + ", DistanceY=" + tirAFaire.Position_Cible.DistanceY
			          + ", TailleCible=" + tirAFaire.Taille_Cible + ", TailleProjectile=" + tirAFaire.Projectile.Taille + ")");
			
			// Suppression du tir dans la liste des tirs à réaliser
			GameController.Jeu.Tirs_A_Realiser.Remove(tirAFaire);
			
			// Ajout du tir dans la liste des tirs effecutés
			GameController.Jeu.Tirs_Realises.Add(tirAFaire);
			
			//CALCUL POSITION DE LA CATAPULTE
			Vector3 positionCatapulte = catapulte.transform.position;
			
			// CHANGEMENT DE LA POSITION ET DE LA TAILLE DE LA CIBLE
			/*float positionXCible = catapulte.transform.position.x + (positionCible.DistanceX * (float)GameController.Jeu.Config.Ratio_echelle);
		float positionYCible = catapulte.transform.position.y + (positionCible.DistanceY * (float)GameController.Jeu.Config.Ratio_echelle);
		float positionZCible = cible.transform.position.z * (float)GameController.Jeu.Config.Ratio_echelle;
		cible.transform.position = new Vector3(positionXCible, positionYCible, positionZCible);*/
			float tailleXYZCible = tirAFaire.Taille_Cible * (float)GameController.Jeu.Config.Ratio_echelle * cible.transform.localScale.x; // la cible doit avoir la meme taille --VISUELLEMENT-- que la balle dans l'IDE Unity
			cible.transform.localScale = new Vector3(tailleXYZCible, tailleXYZCible, tailleXYZCible);
			
			// CHANGEMENT DE LA TAILLE ET DU POIDS DU PROJECTILE
			float tailleXYZProjectile = tirAFaire.Projectile.Taille * (float)GameController.Jeu.Config.Ratio_echelle;
			projectile.transform.localScale = new Vector3(tailleXYZProjectile, tailleXYZProjectile, tailleXYZProjectile);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
