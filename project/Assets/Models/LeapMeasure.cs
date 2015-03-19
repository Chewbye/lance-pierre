using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

/*
 * Classe contenant une liste de méthode permettant l'evaluation de la cible
 * 
 * Contient :
 * - un attribut timer (ou tick) 
 * - un attribut ancienneDistance qui represente la distance mesurée au tick précédant et
 * comparer celle-ci avec la distance actuellement mesurée
 * - un attribut statique TIMER_MAX representant le délai au cours duquel la personne donne 
 * une mesure définitive
 * - un attribut statique BORNE permettant de borner la distance actuellement mesurée avec
 * l'ancienne distance
 */
using System;

public class LeapMeasure {
	
	protected DateTime timer;
	protected float ancienneDistance;
	protected float timerMax;
	protected float borne;
	protected bool premierMesure;
	
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
