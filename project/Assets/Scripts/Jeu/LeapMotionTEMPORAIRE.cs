using UnityEngine;
using System;
using System.Collections;
using Leap;
using System.Runtime.InteropServices;
using System.Collections.Generic;

/*
 * Classe associée à la scene du lancé de projectile et de la mesure de cible
 * 
 * Contient :
 * - un attribut de type Controlleur permettant d'obtenir les actions (les données) du 
 * leap motion
 * - un attribut de type LeapMeasure qui contient des méthodes pour la mesure de la cible
 */

public class LeapMotionTEMPORAIRE : MonoBehaviour {
	
	public Controller controller;
	public LeapMeasure lm;
	public BarreProgression bp;
	public DateTime beginTimer;
	public TimeSpan diffTime;

	private float tempsRestantPourEvaluer;
	
	void Start ()
	{
		controller = new Controller();
		lm = new LeapMeasure(); 
		bp = new BarreProgression ("BarreVide", "Remplissage", 0, lm.TimerMax);
		beginTimer = DateTime.Now;
		tempsRestantPourEvaluer = GameController.Jeu.Config.Delai_evaluation_cible;
		//diffTime = DateTime.Now - beginTimer;
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

	void Evaluation()
	{
		if (tempsRestantPourEvaluer >= 0.0f) 
		{

			if (GameController.Jeu.Config.Delai_evaluation_cible > 0.0f)
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
					print ("**** distance evalué à : " + distance + " mm ****\n");
					GameController.Jeu.Mesures_Taille_Cible.Add(distance);
					GameController.Jeu.Evaluation_En_Cours = false; // SUPER IMPORTANT
					GameController.Jeu.Evaluation_Effectuee = true; // SUPER IMPORTANT
				}
			} else {
				lm.PremierMesure = true;
				lm.Timer = DateTime.Now;
			}
		} else {

			GameController.Jeu.Mesures_Taille_Cible.Add(-99);

			lm.PremierMesure = true;
			lm.Timer = DateTime.Now;
			beginTimer = DateTime.Now;

			GameController.Jeu.Evaluation_En_Cours = false; // SUPER IMPORTANT
			GameController.Jeu.Evaluation_Effectuee = true; // SUPER IMPORTANT
		}
	}


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
