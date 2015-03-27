using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GestionPostTir : MonoBehaviour 
{
	public GameObject projectile;
	public GameObject cible;
	public AudioClip sonReussite;
	public AudioClip sonEchec;

	private float tempsRestant; // Indique combien de temps en seconde les particules et l'info gagne/perdu doivent apparaitre
	private bool sonDejaJoue;

	// Use this for initialization
	void Start () 
	{
		tempsRestant = 2;
		sonDejaJoue = false;
	}

	void Update () 
	{
		if(GameController.Jeu.Tir_Effectue)
		{
			
		}

		// Le tir a ete reussi
		if(GameController.Jeu.Cible_Touchee)
		{
			if(!sonDejaJoue)
			{
				// On joue le son de reussite
				audio.PlayOneShot(sonReussite, 1);
				sonDejaJoue =  true;
			}

			// On desactive l'affichage de la cible
			cible.rigidbody2D.isKinematic = true;
			cible.renderer.enabled = false;
			if(GameController.Jeu.Config.Condition_De_Perception && GameController.Jeu.Tir_Fini)
			{
				cible.renderer.enabled = true;
			}

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
			if(!sonDejaJoue)
			{
				// On joue le son d'echec
				audio.PlayOneShot(sonEchec, 1);
				sonDejaJoue =  true;
			}

			projectile.rigidbody2D.isKinematic = true;

			tempsRestant -= Time.deltaTime;
			if (tempsRestant <= 0.0f)
			{
				ConclureTirManque();
			}
		}
	}

	void ConclureTirReussi()
	{
		// On indique que le tir courant est termine
		GameController.Jeu.Tir_Fini = true;

		// Si nous sommes en condition de Controle
		if(GameController.Jeu.Config.Condition_De_Controle)
		{
			// On indique que le tir courant est reussi
			GameController.Jeu.Reussiste_Tirs.Add(true);
			
			// On augmente le score
			GameController.Jeu.Score = GameController.Jeu.Score + GameController.Jeu.Config.Nb_points_gagnes_par_cible;
			
			//On recharge la meme scène
			GameController.Jeu.Evaluation_Effectuee = false;
			GameController.Jeu.Evaluation_En_Cours = false;
			GameController.Jeu.Tir_Effectue = false;
			GameController.Jeu.Tir_Fini = false;
			GameController.Jeu.Cible_Touchee = false;
			GameController.Jeu.Cible_Manquee = false;
			GameController.Jeu.Delai_Avant_Evaluation = 2;
			GameController.Jeu.Delai_Apres_Evaluation = 2;

			Application.LoadLevel (Application.loadedLevel);
		}
		// Si nous sommes en condition de Memoire
		else if (GameController.Jeu.Config.Condition_De_Memoire)
		{
			// On indique que le tir courant est termine
			GameController.Jeu.Tir_Fini = true;
		}
		// Si nous sommes en condition de Perception
		else if (GameController.Jeu.Config.Condition_De_Perception && GameController.Jeu.Delai_Apres_Evaluation <= 0.0f)
		{
			// On indique que le tir courant est reussi
			GameController.Jeu.Reussiste_Tirs.Add(true);
			
			// On augmente le score
			GameController.Jeu.Score = GameController.Jeu.Score + GameController.Jeu.Config.Nb_points_gagnes_par_cible;
			
			//On recharge la meme scène
			GameController.Jeu.Evaluation_Effectuee = false;
			GameController.Jeu.Evaluation_En_Cours = false;
			GameController.Jeu.Tir_Effectue = false;
			GameController.Jeu.Tir_Fini = false;
			GameController.Jeu.Cible_Touchee = false;
			GameController.Jeu.Cible_Manquee = false;
			GameController.Jeu.Delai_Avant_Evaluation = 2;
			GameController.Jeu.Delai_Apres_Evaluation = 2;
			
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	void ConclureTirManque()
	{
		// On indique que le tir courant est termine
		GameController.Jeu.Tir_Fini = true;

		// Si nous sommes en condition de Controle
		if(GameController.Jeu.Config.Condition_De_Controle)
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
			GameController.Jeu.Evaluation_Effectuee = false;
			GameController.Jeu.Evaluation_En_Cours = false;
			GameController.Jeu.Tir_Effectue = false;
			GameController.Jeu.Tir_Fini = false;
			GameController.Jeu.Cible_Touchee = false;
			GameController.Jeu.Cible_Manquee = false;
			GameController.Jeu.Delai_Avant_Evaluation = 2;
			GameController.Jeu.Delai_Apres_Evaluation = 2;

			Application.LoadLevel (Application.loadedLevel);
		}
		// Si nous sommes en condition de Memoire
		else if (GameController.Jeu.Config.Condition_De_Memoire)
		{

		}
		// Si nous sommes en condition de Perception
		else if (GameController.Jeu.Config.Condition_De_Perception && GameController.Jeu.Delai_Apres_Evaluation <= 0.0f)
		{
			// Si l'evaluation est finie et le delai passe
			if(GameController.Jeu.Delai_Apres_Evaluation <= 0.0f)
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
				GameController.Jeu.Evaluation_Effectuee = false;
				GameController.Jeu.Evaluation_En_Cours = false;
				GameController.Jeu.Tir_Effectue = false;
				GameController.Jeu.Tir_Fini = false;
				GameController.Jeu.Cible_Touchee = false;
				GameController.Jeu.Cible_Manquee = false;
				GameController.Jeu.Delai_Avant_Evaluation = 2;
				GameController.Jeu.Delai_Apres_Evaluation = 2;
				
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
