using UnityEngine;
using System;
using System.Collections;
using Leap;
using System.Runtime.InteropServices;
using System.Collections.Generic;

/*
 * ** LeapMotion **
 * 
 * Classe associée à la scene de la mesure de cible
 */

public class LeapMotion : MonoBehaviour {
	
	public Controller controller; // controleur permettant d'accéder aux données en temps réel du leap motion
	public LeapMeasure lm; // variable de type leapMesure qui contient l'ensemble des méthodes permettant la mesure de la taille de la cible
	public BarreProgression bp; // variable de type BarreProgression qui permet la création de la barre de progression

	private float tempsMisPourEvaluer; // temps mis pour evaluer la taille de la cible

	private float tempsRestantPourEvaluer; // temps restant pour evaluer la taille de la cible

	/*
	 * Création et initilisation des différents attributs et composants
	 */ 
	void Start ()
	{
		controller = new Controller();
		lm = new LeapMeasure(); 
		bp = new BarreProgression ("BarreVide", "Remplissage", 0, lm.TimerMax);
		tempsRestantPourEvaluer = GameController.Jeu.Config.Delai_evaluation_cible;
		tempsMisPourEvaluer = 0;
	}
	
	void Update ()
	{
		// Si nous sommes en CONDITION DE CONTROLE
		if(GameController.Jeu.Config.Condition_De_Controle)
		{
			if(GameController.Jeu.Delai_Avant_Evaluation <= 0.0f && !GameController.Jeu.Evaluation_Effectuee) // POUR AXEL, SI CETTE COND ALORS COMMENCER EVALUATION
			{
				if (!GameController.Jeu.Evaluation_En_Cours)
				{
					lm = new LeapMeasure(); 
				}

				GameController.Jeu.Evaluation_En_Cours = true; // POUR AXEL, PASSER LE BOOLEAN A TRUE
				Evaluation();
			}
		}

		// Si nous sommes en CONDITION DE PERCEPTION
		if(GameController.Jeu.Config.Condition_De_Perception && !GameController.Jeu.Evaluation_Effectuee)
		{
			if(GameController.Jeu.Tir_Fini) // POUR AXEL, SI CETTE COND ALORS COMMENCER EVALUATION
			{
				if (!GameController.Jeu.Evaluation_En_Cours)
				{
					lm = new LeapMeasure(); 
				}

				GameController.Jeu.Evaluation_En_Cours = true; // POUR AXEL, PASSER LE BOOLEAN A TRUE
				Evaluation();
			}
		}
		// Si nous sommes en CONDITION DE MEMOIRE
		if(GameController.Jeu.Config.Condition_De_Memoire && !GameController.Jeu.Evaluation_Effectuee)
		{
			if(GameController.Jeu.Delai_Avant_Evaluation <= 0.0f) // POUR AXEL, SI CETTE COND ALORS COMMENCER EVALUATION
			{
				if (!GameController.Jeu.Evaluation_En_Cours)
				{
					lm = new LeapMeasure(); 
				}

				GameController.Jeu.Evaluation_En_Cours = true; // POUR AXEL, PASSER LE BOOLEAN A TRUE
				Evaluation();
			}
		}
	}

	/*
	 * ** Evaluation **
	 * 
	 * Procédure d'evaluation de la cible
	 */ 
	void Evaluation()
	{
		if (tempsRestantPourEvaluer >= 0.0f) // si il reste du temps pour évaluer
		{
			tempsMisPourEvaluer = tempsMisPourEvaluer + Time.deltaTime; 

			if (GameController.Jeu.Config.Delai_evaluation_cible > 0.0f) // si on a configuré un délais pour l'évaluation
			{
				tempsRestantPourEvaluer -= Time.deltaTime;
			}

			Frame frame = controller.Frame (); // on récupère les données du leap motion
			
			if (frame.Hands.Count > 0) { // si une main est détectée
				
				float distance = lm.getDistance (frame); // on récupère la distance 
				
				// log pour vérifier
				print ("Frame : " + frame.Hands [0].Fingers [0].TipPosition + " - " + frame.Hands [0].Fingers [1].TipPosition + "\n");				
				print ("distance : " + distance + " mm "+ lm.Borne+"\n");
				
				// si le timer atteind le délais de mesure alors la mesure est faite
				bool done = lm.measureDone (distance);

				if (done) {
					print ("**** temps mis pour evaluer cible : " + tempsMisPourEvaluer + " s ****\n");
					print ("**** distance evalué à : " + distance + " mm ****\n");
					GameController.Jeu.Mesures_Taille_Cible.Add(distance);
					GameController.Jeu.Temps_Mis_Pour_Evaluer.Add(tempsMisPourEvaluer);
					GameController.Jeu.Evaluation_En_Cours = false; // SUPER IMPORTANT
					GameController.Jeu.Evaluation_Effectuee = true; // SUPER IMPORTANT
				}
			} else {
				lm.PremierMesure = true;
				lm.Timer = DateTime.Now;
			}
		} else {
			GameController.Jeu.Mesures_Taille_Cible.Add(99);
			float tempPourEvalMax = GameController.Jeu.Config.Delai_evaluation_cible;
			GameController.Jeu.Temps_Mis_Pour_Evaluer.Add(tempPourEvalMax);

			lm.PremierMesure = true;
			lm.Timer = DateTime.Now;

			GameController.Jeu.Evaluation_En_Cours = false; // SUPER IMPORTANT
			GameController.Jeu.Evaluation_Effectuee = true; // SUPER IMPORTANT
		}
	}

	/*
	 * Création graphique des textures.
	 */
	void OnGUI()
	{

		if(bp != null)
		{
			// Si nous sommes en CONDITION DE CONTROLE
			if(GameController.Jeu.Config.Condition_De_Controle)
			{
				if(GameController.Jeu.Evaluation_En_Cours) // POUR AXEL, SI CETTE COND ALORS COMMENCER EVALUATION
				{
					if (GameController.Jeu.Config.Affichage_barre_progression) {
						bp.Valeur = lm.calculNbSecondesEcoule ();
						bp.Update (true);
						bp.Show (UnityEngine.Screen.width, UnityEngine.Screen.height);
					}
				}
			}
			
			// Si nous sommes en CONDITION DE PERCEPTION
			if(GameController.Jeu.Config.Condition_De_Perception)
			{
				if(GameController.Jeu.Tir_Fini) // POUR AXEL, SI CETTE COND ALORS COMMENCER EVALUATION
				{
					if (GameController.Jeu.Config.Affichage_barre_progression) {
						bp.Valeur = lm.calculNbSecondesEcoule ();
						bp.Update (true);
						bp.Show (UnityEngine.Screen.width, UnityEngine.Screen.height);
					}
				}
			}
			// Si nous sommes en CONDITION DE MEMOIRE
			if(GameController.Jeu.Config.Condition_De_Memoire)
			{
				if(GameController.Jeu.Evaluation_En_Cours) // POUR AXEL, SI CETTE COND ALORS COMMENCER EVALUATION
				{
					if (GameController.Jeu.Config.Affichage_barre_progression) {
						bp.Valeur = lm.calculNbSecondesEcoule ();
						bp.Update (true);
						bp.Show (UnityEngine.Screen.width, UnityEngine.Screen.height);
					}
				}
			}
			
			if(GameController.Jeu.Evaluation_Effectuee)
			{
				bp = null;
			}
		}

	}


	// permet de quitter "proprement" l'appli sans faire freeze Unity
	#if UNITY_STANDALONE_WIN
	[DllImport("mono", SetLastError=true)]
	static extern void mono_thread_exit();
	#endif
	
	void OnApplicationQuit() {
		controller.Dispose();
		#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && UNITY_3_5
		mono_thread_exit ();
		#endif
	}
}
