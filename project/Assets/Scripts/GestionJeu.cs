using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestionJeu : MonoBehaviour 
{
	public GameObject projectile;
	public GameObject cible;
	public GameObject catapulte;
	public float maxStretch = 3.0f;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;  
	
	private SpringJoint2D spring;
	private Transform catapult;
	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private float maxStretchSqr;
	private float circleRadius;
	private bool clickedOn;
	private Vector2 prevVelocity;

	void Awake () {
		spring = projectile.GetComponent <SpringJoint2D> ();
		catapult = spring.connectedBody.transform;
	}

	// Use this for initialization
	void Start () 
	{
		// INITIALISATION DES ATTRIBUTS DE JEU
		if(GameController.Jeu.Tirs_A_Realiser.Count == 0 && GameController.Jeu.Tirs_Realises.Count == 0)// Si les triplets n'ont pas déjà été générés
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
			int rang = rnd.Next(0, GameController.Jeu.Tirs_A_Realiser.Count-1);
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
			double ratioEchelle = GameController.Jeu.Config.Ratio_echelle;
			float tailleXYZProjectile = tirAFaire.Projectile.Taille;
			projectile.transform.localScale = new Vector3((float) ratioEchelle* tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile);
			
			LineRendererSetup ();
			rayToMouse = new Ray(catapult.position, Vector3.zero);
			leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
			maxStretchSqr = maxStretch * maxStretch;
			CircleCollider2D circle = projectile.collider2D as CircleCollider2D;
			circleRadius = circle.radius * (float) ratioEchelle * tailleXYZProjectile;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (clickedOn)
			Dragging ();
		
		if (spring != null) {
			if (!projectile.rigidbody2D.isKinematic && prevVelocity.sqrMagnitude > projectile.rigidbody2D.velocity.sqrMagnitude) {
				Destroy (spring);
				projectile.rigidbody2D.velocity = prevVelocity;
			}
			
			if (!clickedOn)
				prevVelocity = projectile.rigidbody2D.velocity;
			
			LineRendererUpdate ();
			
		} else {
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
		}
	}
	
	void LineRendererSetup () {
		catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition(0, catapultLineBack.transform.position);
		
		catapultLineFront.sortingLayerName = "Foreground";
		catapultLineBack.sortingLayerName = "Foreground";
		
		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}
	
	void OnMouseDown () {
		spring.enabled = false;
		clickedOn = true;
	}
	
	void OnMouseUp () {
		spring.enabled = true;
		projectile.rigidbody2D.isKinematic = false;
		clickedOn = false;
	}
	
	void Dragging () {
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;
		
		if (catapultToMouse.sqrMagnitude > maxStretchSqr) {
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
		}
		
		mouseWorldPoint.z = 0f;
		transform.position = mouseWorldPoint;
	}
	
	void LineRendererUpdate () {
		Vector2 catapultToProjectile = projectile.transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
		catapultLineFront.SetPosition(1, holdPoint);
		catapultLineBack.SetPosition(1, holdPoint);
	}
}
