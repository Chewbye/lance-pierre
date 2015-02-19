using UnityEngine;
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

public class LeapMotion : MonoBehaviour {

	public Controller controller;
	public LeapMeasure lm;
	
	void Start ()
	{
		// LeapMeasure étant une classe exterieur à la scene, il faut l'ajouter en tant que composant
		lm = gameObject.AddComponent<LeapMeasure> (); 
		controller = new Controller();
	}
	
	void Update ()
	{
		Frame frame = controller.Frame(); // on récupère les données du leap motion

		//print (lm.Timer+"\n");

		if (frame.Hands.Count > 0) // si une main est détectée
		{
			bool doigtsValide = lm.fingersMeasure (frame);

			if (doigtsValide) // si on a deux doigts "étendus" 
			{

				float distance = lm.getDistance (frame); // on récupère la distance 

				// log pour vérifier
				print("Frame : " + frame.Hands[0].Fingers[0].TipPosition + " - " + frame.Hands [0].Fingers[1].TipPosition + "\n");				
				print("distance : "+ distance + " mm \n");

				// si le timer atteind le délais de mesure alors la mesure est faite
				bool done = lm.measureDone (distance);

				if (done)
				{
					print("**** distance evalué à : "+ distance + " mm ****\n");
					lm.Timer = 0;

					// **** passage à la scene suivante
				}
			} 
			else 
			{
				lm.Timer = 0;
			}
		} 
		else 
		{
			lm.Timer = 0;
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
