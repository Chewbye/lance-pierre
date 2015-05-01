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
	protected bool premierMesure; // 
	
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
	
	public LeapMeasure()
	{
		timer = DateTime.Now;
		ancienneDistance = 0;
		timerMax = GameController.Jeu.Config.Delai_validation_mesure_cible;
		borne = (GameController.Jeu.Config.Marge_stabilisation_validation_cible) * 10; // conversion cm -> mm
		premierMesure = true;
	}
	
	/*
	 * Retourne la liste des indices des doigts "étendus"
	 */
	public bool fingersMeasure(Frame frame)
	{
		return (frame.Hands [0].Fingers [0].IsExtended && frame.Hands [0].Fingers [1].IsExtended);
	}
	
	/*
	 * Retourne la distance (en mm) entre deux doigts
	 */
	public float getDistance(Frame frame)
	{
		float distance = frame.Hands [0].Fingers[0].TipPosition.DistanceTo(frame.Hands [0].Fingers[1].TipPosition);
		
		return distance;
	}
	
	/*
	 * Retourne vrai si la mesure de l'utilisateur est définitive
	 * Faux sinon et RAZ du timer
	 */
	public bool measureDone(float distance)
	{
		if (premierMesure)
		{
			ancienneDistance = distance;
			premierMesure = false;
			
			return false;
		}
		else
		{
			if (ancienneDistance - borne <= distance && distance <= ancienneDistance + borne)
			{
				ancienneDistance = distance;

				TimeSpan diffTime = DateTime.Now - timer;

				if (diffTime.TotalSeconds >= timerMax)
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

	public float calculNbSecondesEcoule()
	{
		TimeSpan diffTime = DateTime.Now - timer;
		return (float) diffTime.TotalSeconds;
	}
}
