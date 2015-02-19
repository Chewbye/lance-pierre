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

public class LeapMeasure : MonoBehaviour{

	protected int timer;
	protected float ancienneDistance;
	protected static int TIMER_MAX;
	protected static float BORNE;

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

		GameController.Jeu = new Jeu (); // **** à commenter des que le jeu tournera

		GameController.Jeu.Config.Delai_evaluation_cible = 10; // **** à commenter dès que le jeu tournera

		TIMER_MAX = GameController.Jeu.Config.Delai_evaluation_cible;
		BORNE = 5; // **** A modifier (Prévoir un parametre qui permet de borner la distance durant le timer)
	}

	/*
	 * Retourne la liste des indices des doigts "étendus"
	 */
	public bool fingersMeasure(Frame frame)
	{
		return (frame.Hands [0].Fingers [0].IsExtended && frame.Hands [0].Fingers [1].IsExtended);
	}

	/*
	public List<int> fingersMeasure(Frame frame)
	{
		List<int> l = new List<int>();
		for (int i=0; i<frame.Hands[0].Fingers.Count; i++)
		{
			if (frame.Hands[0].Fingers[i].IsExtended)
			{
				l.Add(i);
			}
		}

		return l;
	}
	*/

	/*
	 * Retourne la distance (en mm) entre deux doigts
	 */
	public float getDistance(Frame frame)
	{
		float distance = frame.Hands [0].Fingers[0].TipPosition.DistanceTo(frame.Hands [0].Fingers[1].TipPosition);
		
		return distance;
	}

	/*
	public float getDistance(List<int> l,Frame frame)
	{
		float distance = frame.Hands [0].Fingers[l[0]].TipPosition.DistanceTo(frame.Hands [0].Fingers[l[1]].TipPosition);
	
		return distance;
	}
	*/

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
			if (ancienneDistance - BORNE <= distance && distance <= ancienneDistance + BORNE)
			{
				timer++;
				ancienneDistance = distance;
				
				if (timer == TIMER_MAX)
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
