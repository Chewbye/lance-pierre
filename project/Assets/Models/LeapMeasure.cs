using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using System;

/*
 * ** LeapMeasure **
 * 
 * Classe contenant une liste de méthode permettant l'evaluation de la taille de la cible
 */

public class LeapMeasure {
	
	protected DateTime timer; // timer qui mesure le temps mis pour effectuer la mesure
	protected float ancienneDistance; // variable qui contiendra l'ancienne distance mesure 
	protected float timerMax; // temps maximum pour effectuer la mesure
	protected float borne; // marge de stabilisation max autorisé 
	protected bool premierMesure; // variable qui permettra de vérifier si c'est la premier mesure de l'utilisateur

	/*
	 * ** ACCESSEURS en consultations/modifications **
	 */
	public float TimerMax {
		get {
			return timerMax;
		}
		set {
			timerMax = value;
		}
	}
	
	public float Borne {
		get {
			return borne;
		}
	}
	
	public DateTime Timer {
		get {
			return this.timer;
		}
		set {
			timer = value;
		}
	}
	
	
	public float AncienneDistance {
		get {
			return this.ancienneDistance;
		}
		set {
			ancienneDistance = value;
		}
	}

	public bool PremierMesure {
		get {
			return premierMesure;
		}
		set {
			premierMesure = value;
		}
	}

	/*
	 * ** CONSTRUCTEUR **
	 */
	public LeapMeasure()
	{
		timer = DateTime.Now;
		ancienneDistance = 0;
		timerMax = GameController.Jeu.Config.Delai_validation_mesure_cible;
		borne = (GameController.Jeu.Config.Marge_stabilisation_validation_cible) * 10; // conversion cm -> mm
		premierMesure = true;
	}
	
	/*
	 * ** fingersMeasure **
	 * 
	 * Retourne vrai si le pouce et l'index sont "étendus"
	 */
	public bool fingersMeasure(Frame frame)
	{
		return (frame.Hands [0].Fingers [0].IsExtended && frame.Hands [0].Fingers [1].IsExtended);
	}
	
	/*
	 * ** getDistance **
	 * 
	 * Retourne la distance (en mm) entre deux doigts
	 */
	public float getDistance(Frame frame)
	{
		float distance = frame.Hands [0].Fingers[0].TipPosition.DistanceTo(frame.Hands [0].Fingers[1].TipPosition);
		
		return distance;
	}
	
	/*
	 * ** measureDone **
	 * 
	 * Retourne vrai si la mesure de l'utilisateur est définitive
	 * Faux sinon et RAZ du timer
	 */
	public bool measureDone(float distance)
	{
		if (premierMesure) // si c'est la première mesure, on initialise l'ancienne distancte par rapport à la distance mesure 
		{
			ancienneDistance = distance;
			premierMesure = false;
			
			return false;
		}
		else
		{
			if (ancienneDistance - borne <= distance && distance <= ancienneDistance + borne) // si l'écart max entre la distance actuellement mesuré et l'ancienne, on regarde le timer 
			{
				ancienneDistance = distance;

				TimeSpan diffTime = DateTime.Now - timer;

				if (diffTime.TotalSeconds >= timerMax) //si on dépasse le temps max pour mesure, on valide la mesure
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				premierMesure = true;
				timer = DateTime.Now;
				return false;
			}
		}
	}

	/*
	 * ** calculNbSecondesEcoule **
	 * 
	 * Retourne le nombre de secondes du timer
	 */
	public float calculNbSecondesEcoule()
	{
		TimeSpan diffTime = DateTime.Now - timer;
		return (float) diffTime.TotalSeconds;
	}
}
