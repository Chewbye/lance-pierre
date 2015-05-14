using System.Collections;
using System;

/*
 * ** Participant **
 * 
 * Classe representant l'entité Participant
 */

public class Participant {

	protected int numero; // numero du participant
	protected int age; // age du participant
	protected string sexe; // sexe du participant
	protected string mainDominante; // main préféré du participant
	protected string pratiqueJeuxVideo; // réponse question "pratique des jeux vidéos" du participant

	/*
	 * ** ACCESSEURS en consultations/modifications **
	 */
	public int Numero {
		get {
			return numero;
		}
		set {
			numero = value;
		}
	}

	public int Age {
		get {
			return age;
		}
		set {
			age = value;
		}
	}

	public string Sexe {
		get {
			return sexe;
		}
		set {
			sexe = value;
		}
	}

	public string MainDominante {
		get {
			return mainDominante;
		}
		set {
			mainDominante = value;
		}
	}

	public string PratiqueJeuxVideo {
		get {
			return pratiqueJeuxVideo;
		}
		set {
			pratiqueJeuxVideo = value;
		}
	}

	/*
	 * ** CONSTRUCTEUR à Vide**
	 */
	public Participant()
	{
	}

	/*
	 * ** CONSTRUCTEUR **
	 */
	public Participant(int num, int age, string sexe, string mainDominante, string partiqueJV)
	{
		this.numero = num;
		this.age = age;
		this.sexe = sexe;
		this.mainDominante = mainDominante;
		this.pratiqueJeuxVideo = partiqueJV;
	}

	/*
	 * ** ToString **
	 */
	public override String ToString()
	{
		String s = "Participant ("+this.numero+","+this.age+","+this.sexe+","+this.mainDominante+","+this.pratiqueJeuxVideo+")";

		return s;
	}

	/*
	 * ** numeroValide **
	 * 
	 * Vérification si le numero est valide
	 */
	public bool numeroValide()
	{
		return this.numero >= 0;
	}

	/*
	 * ** ageValide **
	 * 
	 * Vérification si l'age est valide
	 */
	public bool ageValide()
	{
		return this.age > 0;
	}

	/*
	 * ** sexeValide **
	 * 
	 * Vérification si le sexe est valide
	 */
	public bool sexeValide()
	{
		return this.sexe.Equals ("homme") || this.sexe.Equals ("femme");
	}

	/*
	 * ** mainDominanteValide **
	 * 
	 * Vérification si la main dominante est valide
	 */
	public bool mainDominanteValide()
	{
		return this.mainDominante.Equals ("gauche") || this.mainDominante.Equals ("droite");
	}

	/*
	 * ** mainDominanteValide **
	 * 
	 * Vérification si la réponse à la question est valide
	 */
	public bool pratiqueJvValide()
	{
		return this.pratiqueJeuxVideo.Equals ("oui") || this.pratiqueJeuxVideo.Equals ("non");
	}
}
