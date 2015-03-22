using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestionJeu : MonoBehaviour 
{
	public GameObject cible;
	public GameObject catapulte;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;  
	
	private double diametreProjectile;
	private SpringJoint2D spring;
	private Transform catapult;
	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private float rigidite;
	private float maxStretchSqr;
	private float circleRadius;
	private bool clickedOn;
	private Vector2 prevVelocity;

	private double ratioEchelle;
	private double ratioCalibrage;
	
	void Awake () 
	{
		spring = GetComponent <SpringJoint2D> ();
		catapult = spring.connectedBody.transform;
	}

	void Start () 
	{
		ratioEchelle = GameController.Jeu.Config.Ratio_echelle;
		ratioCalibrage = 4;
		diametreProjectile = renderer.bounds.size.x * GameController.Jeu.Config.Ratio_echelle;
		Vector3 positionCatapulte = catapulte.transform.position;
		
		// CHANGEMENT DE LA TAILLE DE LA CATAPULTE
		double hauteurCatapulteCm = GameController.Jeu.Config.Taille_Hauteur_Catapulte;
		double hauteurCatapulte = GameController.Jeu.Config.Taille_Hauteur_Catapulte 
			* (float)GameController.Jeu.Config.Ratio_echelle 
				* catapulte.transform.localScale.x / ratioCalibrage; // la catapulte doit avoir la meme taille --VISUELLEMENT-- que la balle dans l'IDE Unity
		catapulte.transform.localScale = new Vector3((float) hauteurCatapulte, (float) hauteurCatapulte, (float)hauteurCatapulte);
		
		// CHANGEMENT DE LA POSITION DE LA CATAPULTE
		float positionXCatapulte = (float) (GameController.Jeu.Config.Distance_X_Catapulte * diametreProjectile / ratioCalibrage);
		float positionYCatapulte = (float) (GameController.Jeu.Config.Distance_Y_Catapulte * diametreProjectile / ratioCalibrage);
		float positionZCatapulte = catapulte.transform.position.z;
		catapulte.transform.position = new Vector3(positionXCatapulte, positionYCatapulte, positionZCatapulte);
		
		// CHANGEMENT DE LA POSITION DU PROJECTILE
		float positionXProjectile = (float) ((GameController.Jeu.Config.Distance_X_Catapulte * diametreProjectile + (-1 * diametreProjectile)) / ratioCalibrage);
		float positionYProjectile = (float) ((GameController.Jeu.Config.Distance_Y_Catapulte * diametreProjectile + (-1 * diametreProjectile)) / ratioCalibrage);
		float positionZProjectile = transform.position.z;
		transform.position = new Vector3(positionXProjectile, positionYProjectile, positionZProjectile);
		
		LineRendererSetup ();
		if(GameController.Jeu.Tirs_A_Realiser.Count == 0 && GameController.Jeu.Tirs_Realises.Count == 0)// Si les triplets n'ont pas déjà été générés
		{
			GenererTirs();
		}
		
		/*if(GameController.Jeu.Tirs_Entrainement.Count > 0) // Si nous sommes dans la phase d'entrainement
		{
			ChoisirTirEntrainement();
		}
		else*/ if(GameController.Jeu.Tirs_Realises.Count < GameController.Jeu.Config.Nb_lancers) // Si nous ne sommes pas en fin de partie ni en phase d'entrainement
		{
			GameController.Jeu.isEntrainement = false;
			ChoisirTirJeu();
		}
		else
		{
			//GameController.Jeu.isPretest = false;
			Application.LoadLevel ("finDeTest");
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (clickedOn)
			Dragging ();
		
		if (spring != null) 
		{
			if (!rigidbody2D.isKinematic && prevVelocity.sqrMagnitude > rigidbody2D.velocity.sqrMagnitude) {
				Destroy (spring);
				rigidbody2D.velocity = prevVelocity;
			}
			
			if (!clickedOn)
				prevVelocity = rigidbody2D.velocity;
			
			LineRendererUpdate ();
			
		} 
		else 
		{
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
		}
	}
	
	void GenererTirs()
	{
		Debug.Log("Génération des combinaisons des tirs ...");
		GameController.Jeu.Nb_lancers = GameController.Jeu.Config.Nb_lancers;
		Debug.Log("Nombre de lancers : " + GameController.Jeu.Nb_lancers);
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
						
						/*if(i == 0 && !GameController.Jeu.isPretest) // On enregistre les tirs de la phase d'entrainement
						{
							GameController.Jeu.Tirs_Entrainement.Add(new TripletTirs(GameController.Jeu.Config.Projectiles[l],
							                                                         GameController.Jeu.Config.Positions_Cibles[j],
							                                                         GameController.Jeu.Config.Tailles_Cibles[k]));
						}*/
						nbCombinaisonsGenerees++;
					}
				}
			}
		}
		Debug.Log("Nombre de combinaisons générées : " + nbCombinaisonsGenerees);
	}
	
	void ChoisirTirJeu()
	{
		// CHANGEMENT DE LA RIGIDITE DU LANCE PIERRE
		rigidite = GameController.Jeu.Config.Rigidite_lancepierre;
		
		// CHOIX D'UN TIR A REALISER
		// Choix d'un tir
		int rang = GameController.Jeu.Rang_Aleatoire.Next(0, GameController.Jeu.Tirs_A_Realiser.Count);
		TripletTirs tirAFaire = GameController.Jeu.Tirs_A_Realiser[rang];
		Debug.Log("Tir choisi (DistanceX=" + tirAFaire.Position_Cible.DistanceX + ", DistanceY=" + tirAFaire.Position_Cible.DistanceY
		          + ", TailleCible=" + tirAFaire.Taille_Cible + ", TailleProjectile=" + tirAFaire.Projectile.Taille + ", PoidsProjectile=" + tirAFaire.Projectile.Poids + ")");
		
		// Suppression du tir dans la liste des tirs à réaliser
		GameController.Jeu.Tirs_A_Realiser.Remove(tirAFaire);
		
		// Ajout du tir dans la liste des tirs effecutés
		GameController.Jeu.Tirs_Realises.Add(tirAFaire);
		
		//CALCUL POSITION DE LA CATAPULTE
		Vector3 positionCatapulte = catapulte.transform.position;
		
		// CHANGEMENT DE LA POSITION ET DE LA TAILLE DE LA CIBLE
		float positionXCible = catapulte.transform.position.x + (tirAFaire.Position_Cible.DistanceX * (float)(diametreProjectile / ratioCalibrage));
		float positionYCible = catapulte.transform.position.y + (tirAFaire.Position_Cible.DistanceY * (float)(diametreProjectile / ratioCalibrage));
		float positionZCible = cible.transform.position.z * (float)diametreProjectile / 4;
		cible.transform.position = new Vector3(positionXCible, positionYCible, positionZCible);
		float tailleXYZCible = tirAFaire.Taille_Cible * (float)(GameController.Jeu.Config.Ratio_echelle * cible.transform.localScale.x / ratioCalibrage); // la cible doit avoir la meme taille --VISUELLEMENT-- que la balle dans l'IDE Unity
		cible.transform.localScale = new Vector3(tailleXYZCible, tailleXYZCible, tailleXYZCible);
		
		// CHANGEMENT DE LA TAILLE ET DU POIDS DU PROJECTILE
		float tailleXYZProjectile = (float) (tirAFaire.Projectile.Taille / ratioCalibrage);
		transform.localScale = new Vector3((float) ratioEchelle* tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile);
		LineRendererSetup ();
		rayToMouse = new Ray(catapult.position, Vector3.zero);
		leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
		maxStretchSqr = rigidite * rigidite;
		CircleCollider2D circle = collider2D as CircleCollider2D;
		circleRadius = circle.radius * (float) ratioEchelle * tailleXYZProjectile;
		
		//rigidbody2D.mass = tirAFaire.Projectile.Poids;
		rigidbody2D.gravityScale = tirAFaire.Projectile.Poids * GameController.Jeu.Config.Gravite;
	}
	
	/*void ChoisirTirEntrainement()
	{
		// CHANGEMENT DE LA RIGIDITE DU LANCE PIERRE
		rigidite = GameController.Jeu.Config.Rigidite_lancepierre;
		
		// CHOIX D'UN TIR A REALISER
		// Choix d'un tir
		int rang = GameController.Jeu.Rang_Aleatoire.Next(0, GameController.Jeu.Tirs_Entrainement.Count);
		TripletTirs tirAFaire = GameController.Jeu.Tirs_Entrainement[rang];
		Debug.Log("Tir choisi (DistanceX=" + tirAFaire.Position_Cible.DistanceX + ", DistanceY=" + tirAFaire.Position_Cible.DistanceY
		          + ", TailleCible=" + tirAFaire.Taille_Cible + ", TailleProjectile=" + tirAFaire.Projectile.Taille + ", PoidsProjectile=" + tirAFaire.Projectile.Poids + ")");
		
		// Suppression du tir dans la liste des tirs à réaliser
		GameController.Jeu.Tirs_Entrainement.Remove(tirAFaire);
		
		//CALCUL POSITION DE LA CATAPULTE
		Vector3 positionCatapulte = catapulte.transform.position;
		
		// CHANGEMENT DE LA POSITION ET DE LA TAILLE DE LA CIBLE
		float positionXCible = catapulte.transform.position.x + (tirAFaire.Position_Cible.DistanceX * (float)diametreProjectile);
		float positionYCible = catapulte.transform.position.y + (tirAFaire.Position_Cible.DistanceY * (float)diametreProjectile);
		float positionZCible = cible.transform.position.z * (float)diametreProjectile;
		cible.transform.position = new Vector3(positionXCible, positionYCible, positionZCible);
		float tailleXYZCible = tirAFaire.Taille_Cible * (float)GameController.Jeu.Config.Ratio_echelle * cible.transform.localScale.x; // la cible doit avoir la meme taille --VISUELLEMENT-- que la balle dans l'IDE Unity
		cible.transform.localScale = new Vector3(tailleXYZCible, tailleXYZCible, tailleXYZCible);
		
		// CHANGEMENT DE LA TAILLE ET DU POIDS DU PROJECTILE
		double ratioEchelle = GameController.Jeu.Config.Ratio_echelle;
		float tailleXYZProjectile = tirAFaire.Projectile.Taille;
		transform.localScale = new Vector3((float) ratioEchelle* tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile);
		LineRendererSetup ();
		rayToMouse = new Ray(catapult.position, Vector3.zero);
		leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
		maxStretchSqr = rigidite * rigidite;
		CircleCollider2D circle = collider2D as CircleCollider2D;
		circleRadius = circle.radius * (float) ratioEchelle * tailleXYZProjectile;
		
		//rigidbody2D.mass = tirAFaire.Projectile.Poids;
		rigidbody2D.gravityScale = tirAFaire.Projectile.Poids * GameController.Jeu.Config.Gravite;
	}*/
	
	void LineRendererSetup () 
	{
		catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition(0, catapultLineBack.transform.position);
		
		catapultLineFront.sortingLayerName = "Foreground";
		catapultLineBack.sortingLayerName = "Foreground";
		
		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}
	
	void OnMouseDown () 
	{
		spring.enabled = false;
		clickedOn = true;
	}
	
	void OnMouseUp () 
	{
		spring.enabled = true;
		rigidbody2D.isKinematic = false;
		clickedOn = false;
		
		// On indique le temps mis par le joueur pour tirer
		GameController.Jeu.Temps_Mis_Pour_Tirer.Add(GameController.Jeu.Config.Delai_lancer_projectile - GameController.Jeu.Temps_Restant_Courant);
	}
	
	void Dragging () 
	{
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;
		
		if (catapultToMouse.sqrMagnitude > maxStretchSqr) 
		{
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint(rigidite);
		}
		
		mouseWorldPoint.z = 0f;
		transform.position = mouseWorldPoint;
	}
	
	void LineRendererUpdate () 
	{
		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
		catapultLineFront.SetPosition(1, holdPoint);
		catapultLineBack.SetPosition(1, holdPoint);
	}
}
