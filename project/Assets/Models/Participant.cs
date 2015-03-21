using System.Collections;
using System;

public class Participant {

	protected int numero;
	protected int age;
	protected string sexe;
	protected string mainDominante;
	protected string pratiqueJeuxVideo;

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

	public Participant()
	{
	}

	public Participant(int num, int age, string sexe, string mainDominante, string partiqueJV)
	{
		this.numero = num;
		this.age = age;
		this.sexe = sexe;
		this.mainDominante = mainDominante;
		this.pratiqueJeuxVideo = partiqueJV;
	}

	public override String ToString()
	{
		String s = "Participant ("+this.numero+","+this.age+","+this.sexe+","+this.mainDominante+","+this.pratiqueJeuxVideo+")";

		/*
		String s = "Participant : \n";
		s = s + "- numero : " + this.numero + "\n";
		s = s + "- age : " + this.age + "\n";
		s = s + "- sexe : " + this.sexe + "\n";
		s = s + "- main dominante : " + this.mainDominante + "\n";
		s = s + "- pratique régulière jeux video : " + this.pratiqueJeuxVideo + "\n";
		*/

		return s;
	}

	public bool numeroValide()
	{
		return (this.numero > 0);
	}

	public bool ageValide()
	{
		return this.age > 0;
	}

	public bool sexeValide()
	{
		return this.sexe.Equals ("homme") || this.sexe.Equals ("femme");
	}

	public bool mainDominanteValide()
	{
		return this.mainDominante.Equals ("gauche") || this.mainDominante.Equals ("droite");
	}

	public bool pratiqueJvValide()
	{
		return this.pratiqueJeuxVideo.Equals ("oui") || this.pratiqueJeuxVideo.Equals ("non");
	}
}
