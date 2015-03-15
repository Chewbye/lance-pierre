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

public class LeapMeasure {

	protected int timer;
	protected float ancienneDistance;
	protected float timerMax;
	protected float borne;

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

	public int Timer {
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

	public LeapMeasure()
	{
		timer = 0;
		ancienneDistance = 0;
		//timerMax = GameController.Jeu.Config.Delai_evaluation_cible;
		timerMax = 10;
		borne = 5; // **** A modifier (Prévoir un parametre qui permet de borner la distance durant le timer)
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
		if (timer==0)
		{
			ancienneDistance = distance;
			timer++;

			return false;
		}
		else
		{
			if (ancienneDistance - borne <= distance && distance <= ancienneDistance + borne)
			{
				timer++;
				ancienneDistance = distance;
				
				if (timer == timerMax)
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
				timer=0;

				return false;
			}
		}
	}
}
