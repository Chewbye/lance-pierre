using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Classe utilisee pour limiter le temps que peut mettre le joeur pour tirer
public class GestionTemps : MonoBehaviour 
{
	private float delainAvantEvaluation;
	private float delaiApresEvaluation;

	private float simulationEvaluation;

	// Use this for initialization
	void Start () 
	{
		// On simule un temps d'evaluation de 5s
		simulationEvaluation = 5;
		delainAvantEvaluation = GameController.Jeu.Delai_Avant_Evaluation;
		delaiApresEvaluation = GameController.Jeu.Delai_Apres_Evaluation;
		GameController.Jeu.Temps_Restant_Courant = GameController.Jeu.Config.Delai_lancer_projectile;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Si nous sommes en CONDITION DE CONTROLE
		if(GameController.Jeu.Config.Condition_De_Controle)
		{
			// Si nous sommes en Condition de Controle et que l'evaluation n'est pas encore faite : attendre puis evaluation
			if(!GameController.Jeu.Evaluation_Effectuee)
			{
				GameController.Jeu.Delai_Avant_Evaluation -= Time.deltaTime;
			}

			// SIMULATION EVALUATION
			if(GameController.Jeu.Delai_Avant_Evaluation <= 0.0f)
			{
				GameController.Jeu.Evaluation_En_Cours = true;
				simulationEvaluation -= Time.deltaTime;
				if (simulationEvaluation <= 0.0f)
				{
					GameController.Jeu.Evaluation_En_Cours = false;
					GameController.Jeu.Evaluation_Effectuee = true;
				}
			}
			
			// Si nous sommes en Condition de Controle et que l'evaluation est faite : attendre puis permettre tir
			if(GameController.Jeu.Evaluation_Effectuee)
			{
				GameController.Jeu.Delai_Apres_Evaluation -= Time.deltaTime;
			}

			// Le joueur a un certain temps pour tirer
			if(GameController.Jeu.Delai_Apres_Evaluation <= 0.0f && !GameController.Jeu.Tir_Effectue)
			{
				GameController.Jeu.Temps_Restant_Courant -= Time.deltaTime;
				if (GameController.Jeu.Temps_Restant_Courant <= 0.0f)
				{
					Conclure();
				}
			}
		}

		// Si nous sommes en CONDITION DE PERCEPTION
		if(GameController.Jeu.Config.Condition_De_Perception)
		{
			Debug.Log("STOP FABIEN");
		}

		// Si nous sommes en CONDITION DE MEMOIRE
		if(GameController.Jeu.Config.Condition_De_Memoire)
		{
			Debug.Log("STOP FABIEN");
		}
	}

	// Le joueur n'a pas joué avant le temps autorise
	void Conclure() 
	{
		Debug.Log("Temps écoulé pour tirer !");
		// La cible est donc manquee
		GameController.Jeu.Cible_Manquee = true;
	}
}
