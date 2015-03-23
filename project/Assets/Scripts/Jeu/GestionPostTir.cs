using UnityEngine;
using System.Collections;

public class GestionPostTir : MonoBehaviour 
{
	public GameObject projectile;
	public GameObject cible;

	private float tempsRestant; // Indique combien de temps en seconde les particules et l'info gagne/perdu doivent apparaitre

	// Use this for initialization
	void Start () 
	{
		tempsRestant = 2;
	}

	void Update () 
	{
		if(GameController.Jeu.Tir_Effectue)
		{
			
		}

		// Le tir a ete reussi
		if(GameController.Jeu.Cible_Touchee)
		{
			// On desactive l'affichage de la cible
			cible.rigidbody2D.isKinematic = true;
			cible.renderer.enabled = false;

			// On desactive l'affichage du projectile
			projectile.rigidbody2D.isKinematic = true;
			projectile.renderer.enabled = false;

			tempsRestant -= Time.deltaTime;
			if (tempsRestant <= 0.0f)
			{
				ConclureTirReussi();
			}
		}

		// Le tir a ete manque
		else if(GameController.Jeu.Cible_Manquee)
		{
			// On desactive l'affichage de la cible
			cible.rigidbody2D.isKinematic = true;
			cible.renderer.enabled = false;
			
			// On desactive l'affichage du projectile
			projectile.rigidbody2D.isKinematic = true;
			projectile.renderer.enabled = false;
			SpringJoint2D spring = projectile.GetComponent <SpringJoint2D> ();
			spring.enabled = true;

			tempsRestant -= Time.deltaTime;
			if (tempsRestant <= 0.0f)
			{
				ConclureTirManque();
			}
		}
	}

	void ConclureTirReussi()
	{
		// On indique que le tir courant est reussi
		GameController.Jeu.Reussiste_Tirs.Add(true);
		
		// On baisse le score
		GameController.Jeu.Score = GameController.Jeu.Score + GameController.Jeu.Config.Nb_points_gagnes_par_cible;

		//On recharge la meme scène
		GameController.Jeu.Tir_Effectue = false;
		GameController.Jeu.Cible_Touchee = false;
		GameController.Jeu.Cible_Manquee = false;
		Application.LoadLevel (Application.loadedLevel);
	}

	void ConclureTirManque()
	{
		// On indique que le tir courant est manqué
		GameController.Jeu.Reussiste_Tirs.Add(false);

		if (GameController.Jeu.Temps_Restant_Courant <= 0.0f)
		{
			// On indique le temps mis par le joueur pour tirer (ici -1 car le joueur n'a pas tiré)
			GameController.Jeu.Temps_Mis_Pour_Tirer.Add(-1);
		}

		// On baisse le score
		GameController.Jeu.Score = GameController.Jeu.Score - GameController.Jeu.Config.Nb_points_perdus_par_cible_manque;

		//On recharge la meme scène
		GameController.Jeu.Tir_Effectue = false;
		GameController.Jeu.Cible_Touchee = false;
		GameController.Jeu.Cible_Manquee = false;
		Application.LoadLevel (Application.loadedLevel);
	}
}
