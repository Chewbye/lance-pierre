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

	public bool erreur;
	public string typeErreur;
	public int largeurPopup;
	public int hauteurPopup;



	// Use this for initialization
	void Start () {
		GameController.Jeu.Participant.Sexe = "homme";
		GameController.Jeu.Participant.MainDominante = "droite";
		GameController.Jeu.Participant.PratiqueJeuxVideo = "oui";
		erreur = false;
		typeErreur = "";
		this.largeurPopup = 150;
		this.hauteurPopup = 150;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onChangeValueNumero()
	{
		int res = 0;
		if (int.TryParse(IF_numero.text,out res)) {
			GameController.Jeu.Participant.Numero = res;
		} else {
			GameController.Jeu.Participant.Numero = 0;
		}
	}

	public void onChangeValueAge()
	{
		int res = 0;
		if (int.TryParse (IF_age.text, out res)) {
			GameController.Jeu.Participant.Age = res;
		} else {
			GameController.Jeu.Participant.Age = 0;
		}
	}

	public void onChangeToggleSexeHomme()
	{
		if (T_Homme.isOn) {
			GameController.Jeu.Participant.Sexe = "homme";
		}
	}

	public void onChangeToggleSexeFemme()
	{
		if (T_Femme.isOn) {
			GameController.Jeu.Participant.Sexe = "femme";
		}
	}

	public void onChangeToggleMainDominanteGauche()
	{
		if (T_Gauche.isOn) {
			GameController.Jeu.Participant.MainDominante = "gauche";
		}

	}

	public void onChangeToggleMainDominanteDroite()
	{
		if (T_Droite.isOn) {
			GameController.Jeu.Participant.MainDominante = "droite";
		}
	}

	public void onChangeToggleJVOui()
	{
		if (T_Oui.isOn) {
			GameController.Jeu.Participant.PratiqueJeuxVideo = "oui";
		}
	}

	public void onChangeToggleJVNon()
	{
		if (T_Non.isOn) {
			GameController.Jeu.Participant.PratiqueJeuxVideo = "non";
		}
	}


	public void onClickButtonLancerTest()
	{
		if (GameController.Jeu.Participant.numeroValide ()) {
			if (GameController.Jeu.Participant.ageValide ()) {
				Debug.Log(GameController.Jeu.Participant.ToString()+ "\n");
				UnityEngine.Application.LoadLevel ("Jeu");
			} else {
				Debug.Log("age invalide \n");
				erreur = true;
				typeErreur = "age invalide";
			}
		} else {
			Debug.Log("numero invalide \n");
			erreur = true;
			typeErreur = "numero invalide";

		}

	}

	public void onClickButtonRevenirMenu()
	{
		GameController.Jeu.isPretest = true;
		UnityEngine.Application.LoadLevel ("menu");
	}

	void OnGUI()
	{
		if (erreur) {
			UnityEngine.Rect rect = new Rect((Screen.width-largeurPopup)/2,(Screen.height-hauteurPopup)/2,largeurPopup,hauteurPopup);
			GUI.Window (1,rect,DoMyWindows,typeErreur);
		}

	}

	void DoMyWindows (int id)
	{
		if (GUI.Button (new Rect ((largeurPopup-80)/2, (hauteurPopup-20)/2, 80, 20), "OK")) {
			erreur = false;
		}
	}
}
