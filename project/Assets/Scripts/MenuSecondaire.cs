using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSecondaire : MonoBehaviour {

	public InputField IF_numero;
	public InputField IF_age;
	public Toggle T_Homme;
	public Toggle T_Femme;
	public Toggle T_Gauche;
	public Toggle T_Droite;
	public Toggle T_Oui;
	public Toggle T_Non;


	// Use this for initialization
	void Start () {
		GameController.Jeu = new Jeu ();
		GameController.Jeu.Participant = new Participant ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onChangeValueNumero()
	{
		int res;
		if (int.TryParse (IF_numero.text, out res)) {
			Debug.Log("ok 1");
			GameController.Jeu.Participant.Numero = res;
		}
		Debug.Log(IF_numero.text.Length);
	}

	public void onChangeValueAge()
	{
		int res;
		if (int.TryParse (IF_age.text, out res)) {
			Debug.Log("ok 2");
			GameController.Jeu.Participant.Age = res;
		}
		Debug.Log("---");
	}

	public void onChangeToggleSexeHomme()
	{
		if (T_Homme.isOn) {
			GameController.Jeu.Participant.Sexe = "homme";
			T_Femme.isOn = false; 
		}
	}

	public void onChangeToggleSexeFemme()
	{
		if (T_Femme.isOn) {
			GameController.Jeu.Participant.Sexe = "femme";
			T_Homme.isOn = false;
		}
	}

	public void onChangeToggleMainDominanteGauche()
	{
		if (T_Gauche.isOn) {
			GameController.Jeu.Participant.MainDominante = "gauche";
			T_Droite.isOn  = false; 
		}

	}

	public void onChangeToggleMainDominanteDroite()
	{
		if (T_Droite.isOn) {
			GameController.Jeu.Participant.MainDominante = "droite";
			T_Gauche.isOn = false;
		}
	}

	public void onChangeToggleJVOui()
	{
		if (T_Oui.isOn) {
			GameController.Jeu.Participant.PratiqueJeuxVideo = "oui";
			T_Non.isOn = false;
		}
	}

	public void onChangeToggleJVNon()
	{
		if (T_Non.isOn) {
			GameController.Jeu.Participant.PratiqueJeuxVideo = "non";
			T_Oui.isOn = false;
		}
	}


	public void onClickButtonLancerTest()
	{
		Debug.Log(GameController.Jeu.Participant.ToString()+ "\n");
	}
}
