using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Classe utilisee pour initialiser en debut de session toutes les combinaisons possibles de tirs puis pour en choisir un aleatoirement
public class GestionInitialisationTir : MonoBehaviour 
{
	public GameObject cible;
	public GameObject catapulte;
	public GameObject catapulteFront;
	public GameObject projectile;
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

	private TripletTirs tirAFaire;

	private bool catapulteActivee;

	void Awake () 
	{
		spring = GetComponent <SpringJoint2D> ();
		catapult = spring.connectedBody.transform;
	}

	void Start () 
	{
		ratioCalibrage = 4;
		// INITIALISATION DES ATTRIBUTS DANS JEU
		ratioEchelle = GameController.Jeu.Config.Ratio_echelle;
		GameController.Jeu.Nb_lancers = GameController.Jeu.Config.Nb_lancers;
		GameController.Jeu.Tir_Effectue = false;
		GameController.Jeu.Cible_Touchee = false;
		GameController.Jeu.Cible_Manquee = false;
		
		catapulteActivee = false;
		
		diametreProjectile = renderer.bounds.size.x * GameController.Jeu.Config.Ratio_echelle;
		Vector3 positionCatapulte = catapulte.transform.position;
		
		// CHANGEMENT DE LA TAILLE DE LA CATAPULTE
		ChangerTailleCatapulte();
		
		// CHANGEMENT DE LA POSITION DE LA CATAPULTE ET DU PROJECTILE
		ChangerPositionCatapulte();
		
		// On initilalise le rendu de la corde de la catapulte
		LineRendererSetup ();
		
		// Si les triplets de tirs n'ont pas déjà été générés
		if(GameController.Jeu.Tirs_A_Realiser.Count == 0 && GameController.Jeu.Tirs_Realises.Count == 0)
		{
			GenererTirs();
		}
		
		// Si nous ne sommes pas en fin de partie
		if(GameController.Jeu.Tirs_Realises.Count < GameController.Jeu.Config.Nb_lancers)
		{
			GameController.Jeu.isEntrainement = false;
			ChoisirTirJeu();
		}
		else // Si nous sommes en fin de partie
		{
			//GameController.Jeu.isPretest = false;
			Application.LoadLevel ("finDeTest");
		}
		
		if(GameController.Jeu.Config.Condition_De_Controle)
		{
			// On desactive la catapulte
			catapulte.renderer.enabled = false;
			catapulteFront.renderer.enabled = false;
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
			
			// On desactive le projectile
			projectile.renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Si nous sommes en mode Condtion de Controle
		if(GameController.Jeu.Config.Condition_De_Controle)
		{
			// On reactive la catapulte et le projectile
			ActiverCatapulteModeControle();
		}
		// Si le joueur clique sur le projectile
		if (clickedOn)
		{
			Dragging ();
		}
		
		if (spring != null) 
		{
			if (!rigidbody2D.isKinematic && prevVelocity.sqrMagnitude > rigidbody2D.velocity.sqrMagnitude) 
			{
				Destroy (spring);
				rigidbody2D.velocity = prevVelocity;
			}
			
			if (!clickedOn)
			{
				prevVelocity = rigidbody2D.velocity;
			}

			LineRendererUpdate ();	
		} 
		else 
		{
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;
		}
	}

	void ActiverCatapulteModeControle()
	{
		if(!catapulteActivee && GameController.Jeu.Evaluation_Effectuee && (GameController.Jeu.Delai_Apres_Evaluation <= 0.0f))
		{
			// On active la catapulte
			catapulte.renderer.enabled = true;
			catapulteFront.renderer.enabled = true;
			catapultLineFront.enabled = true;
			catapultLineBack.enabled = true;
			
			// On active le projectile
			projectile.renderer.enabled = true;

			catapulteActivee = true;
		}
	}
	
	void GenererTirs()
	{
		Debug.Log("Génération des combinaisons des tirs ...");
		Debug.Log("Nombre de lancers : " + GameController.Jeu.Nb_lancers);
		int nbCombinaisonsGenerees = 0;
		// Calcul de toutes les combinaisons possibles de Position de Cible, Taille de Cible et Taille de Projectile
		// Nombre de series a generer
		for(int i = 0; i < GameController.Jeu.Config.NB_series; i++)
		{
			// Nombre de positions de cibles a traiter
			for(int j = 0; j <GameController.Jeu.Config.Positions_Cibles.Count; j++)
			{
				// Nombre de tailles de cibles a traiter
				for( int k = 0; k < GameController.Jeu.Config.Tailles_Cibles.Count; k++)
				{
					// Nombre de tailles de projectiles a traiter
					for(int l = 0; l < GameController.Jeu.Config.Projectiles.Count; l++)
					{
						// On cree le triplet de tir correspondant
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
	
	void ChoisirTirJeu()
	{
		// CHANGEMENT DE LA RIGIDITE DU LANCE PIERRE
		rigidite = GameController.Jeu.Config.Rigidite_lancepierre;
		
		// CHOIX D'UN TIR A REALISER
		// Choix d'un tir
		int rang = GameController.Jeu.Rang_Aleatoire.Next(0, GameController.Jeu.Tirs_A_Realiser.Count);
		tirAFaire = GameController.Jeu.Tirs_A_Realiser[rang];
		Debug.Log("Tir choisi (DistanceX=" + tirAFaire.Position_Cible.DistanceX + ", DistanceY=" + tirAFaire.Position_Cible.DistanceY
		          + ", TailleCible=" + tirAFaire.Taille_Cible + ", TailleProjectile=" + tirAFaire.Projectile.Taille + ", PoidsProjectile=" + tirAFaire.Projectile.Poids + ")");
		
		// Suppression du tir dans la liste des tirs à réaliser
		GameController.Jeu.Tirs_A_Realiser.Remove(tirAFaire);
		
		// Ajout du tir dans la liste des tirs effecutés
		GameController.Jeu.Tirs_Realises.Add(tirAFaire);
		
		//CALCUL POSITION DE LA CATAPULTE
		Vector3 positionCatapulte = catapulte.transform.position;
		
		// CHANGEMENT DE LA POSITION ET DE LA TAILLE DE LA CIBLE
		ChangerProprieteCible();

		// CHANGEMENT DE LA TAILLE ET DU POIDS DU PROJECTILE
		ChangerProprieteProjectile();

	}

	void ChangerProprieteCible()
	{
		// Conversion des positions X Y Z en centimetres vers l'unite de Unity
		float positionXCible = catapulte.transform.position.x + (tirAFaire.Position_Cible.DistanceX * (float)(diametreProjectile / ratioCalibrage));
		float positionYCible = catapulte.transform.position.y + (tirAFaire.Position_Cible.DistanceY * (float)(diametreProjectile / ratioCalibrage));
		float positionZCible = cible.transform.position.z * (float)(diametreProjectile / ratioCalibrage);

		cible.transform.position = new Vector3(positionXCible, positionYCible, positionZCible);

		// Conversion de la taille en centimetres vers l'unite de Unity
		// La cible doit avoir la meme taille --VISUELLEMENT-- que la balle dans l'IDE Unity
		float tailleXYZCible = tirAFaire.Taille_Cible * (float)(GameController.Jeu.Config.Ratio_echelle * cible.transform.localScale.x / ratioCalibrage); 

		cible.transform.localScale = new Vector3(tailleXYZCible, tailleXYZCible, tailleXYZCible);
	}

	void ChangerProprieteProjectile()
	{
		// Conversion des positions X Y Z en centimetres vers l'unite de Unity
		float tailleXYZProjectile = (float) (tirAFaire.Projectile.Taille / ratioCalibrage);
		transform.localScale = new Vector3((float) ratioEchelle* tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile, (float)ratioEchelle*tailleXYZProjectile);

		// On cree le rendu de la corde
		LineRendererSetup ();
		rayToMouse = new Ray(catapult.position, Vector3.zero);
		leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
		maxStretchSqr = rigidite * rigidite;
		CircleCollider2D circle = collider2D as CircleCollider2D;
		circleRadius = circle.radius * (float) ratioEchelle * tailleXYZProjectile;
		
		// On change la gravite du projectile
		rigidbody2D.gravityScale = tirAFaire.Projectile.Poids * GameController.Jeu.Config.Gravite;
	}

	void ChangerTailleCatapulte()
	{
		// Conversion de la taille en centimetres vers l'unite de Unity
		// La catapulte doit avoir la meme taille --VISUELLEMENT-- que la balle dans l'IDE Unity
		double hauteurCatapulteCm = GameController.Jeu.Config.Taille_Hauteur_Catapulte;
		double hauteurCatapulte = GameController.Jeu.Config.Taille_Hauteur_Catapulte 
			* (float)GameController.Jeu.Config.Ratio_echelle 
				* catapulte.transform.localScale.x / ratioCalibrage; 
		catapulte.transform.localScale = new Vector3((float) hauteurCatapulte, (float) hauteurCatapulte, (float)hauteurCatapulte);
	}

	void ChangerPositionCatapulte()
	{
		// Conversion des positions X Y Z en centimetres vers l'unite de Unity
		float positionXCatapulte = (float) (GameController.Jeu.Config.Distance_X_Catapulte * diametreProjectile / ratioCalibrage);
		float positionYCatapulte = (float) (GameController.Jeu.Config.Distance_Y_Catapulte * diametreProjectile / ratioCalibrage);
		float positionZCatapulte = catapulte.transform.position.z * (float)(diametreProjectile / ratioCalibrage);

		catapulte.transform.position = new Vector3(positionXCatapulte, positionYCatapulte, positionZCatapulte);

		// On adapte la position du projectile en fonction de la taille de la catapulte
		float positionXProjectile = (float) ((GameController.Jeu.Config.Distance_X_Catapulte * diametreProjectile + (-1 * diametreProjectile)) / ratioCalibrage);
		float positionYProjectile = (float) ((GameController.Jeu.Config.Distance_Y_Catapulte * diametreProjectile + (-1 * diametreProjectile)) / ratioCalibrage);
		float positionZProjectile = transform.position.z * (float)(diametreProjectile / ratioCalibrage);

		transform.position = new Vector3(positionXProjectile, positionYProjectile, positionZProjectile);
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
	
	void LineRendererSetup() 
	{
		catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition(0, catapultLineBack.transform.position);
		
		catapultLineFront.sortingLayerName = "Foreground";
		catapultLineBack.sortingLayerName = "Foreground";
		
		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}

	// Le joueur clique sur le projectile
	void OnMouseDown() 
	{
		if(!GameController.Jeu.Cible_Manquee)
		{
			spring.enabled = false;
			clickedOn = true;
		}
		else
		{
			clickedOn = false;
			rigidbody2D.isKinematic = true;
		}
	}

	// Le joueur lache le projectile
	void OnMouseUp() 
	{
		if(!GameController.Jeu.Cible_Manquee)
		{
			spring.enabled = true;
			rigidbody2D.isKinematic = false;
			clickedOn = false;
			
			// On indique le temps mis par le joueur pour tirer
			GameController.Jeu.Temps_Mis_Pour_Tirer.Add(GameController.Jeu.Config.Delai_lancer_projectile - GameController.Jeu.Temps_Restant_Courant);
			
			// On indique que le tir est effectue
			GameController.Jeu.Tir_Effectue = true;
		}
		else
		{
			clickedOn = false;
			rigidbody2D.isKinematic = true;
		}
	}
	
	void Dragging() 
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
	
	void LineRendererUpdate() 
	{
		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);

		catapultLineFront.SetPosition(1, holdPoint);
		catapultLineBack.SetPosition(1, holdPoint);
	}
}
