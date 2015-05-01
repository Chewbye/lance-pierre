using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * ** MenuSecondaire **
 * 
 * Classe associée à la scene du formulaire du participant
 */

public class MenuSecondaire : MonoBehaviour {

	public InputField IF_numero; // champs du numero du participant
	public InputField IF_age; // champs de l'age du participant
	public Toggle T_Homme; // checkbox choix "homme" pour question sur sexe
	public Toggle T_Femme; // checkbox choix "femme" pour question sur sexe
	public Toggle T_Gauche; // checkbox choix "gauche" pour question sur main dominante
	public Toggle T_Droite; // checkbox choix "droite" pour question sur main dominante
	public Toggle T_Oui; // checkbox choix "oui" pour question sur pratique courante jeux video
	public Toggle T_Non; // checkbox choix "non" pour question sur pratique courante jeux video

	public bool erreur; // variable qui permet de signaler une erreur de saisie
	public string typeErreur; // variable qui stocke le type d'erreur signalé
	public int largeurPopup; // largeur de la pop-up d'erreur
	public int hauteurPopup; // hauteur de la pop-up d'erreur



	// Use this for initialization
	/*
	 * Création et initilisation des différents attributs et composants
	 */ 
	void Start () {
		GameController.Jeu.Participant.Sexe = "Homme";
		GameController.Jeu.Participant.MainDominante = "Droite";
		GameController.Jeu.Participant.PratiqueJeuxVideo = "Oui";
		erreur = false;
		typeErreur = "";
		this.largeurPopup = 150;
		this.hauteurPopup = 150;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * ** GESTION DES EVENEMENTS SUR LES CHAMPS ET LES CHECKBOXS **
	 */
	public void onChangeValueNumero()
	{
		int res = 0;
		if (int.TryParse(IF_numero.text,out res)) {
			GameController.Jeu.Participant.Numero = res;
		} else {
			GameController.Jeu.Participant.Numero = -1;
		}
	}

	public void onChangeValueAge()
	{
		int res = 0;
		if (int.TryParse (IF_age.text, out res)) {
			GameController.Jeu.Participant.Age = res;
		} else {
			GameController.Jeu.Participant.Age = -1;
		}
	}

	public void onChangeToggleSexeHomme()
	{
		if (T_Homme.isOn) {
			GameController.Jeu.Participant.Sexe = "Homme";
		}
	}

	public void onChangeToggleSexeFemme()
	{
		if (T_Femme.isOn) {
			GameController.Jeu.Participant.Sexe = "Femme";
		}
	}

	public void onChangeToggleMainDominanteGauche()
	{
		if (T_Gauche.isOn) {
			GameController.Jeu.Participant.MainDominante = "Gauche";
		}

	}

	public void onChangeToggleMainDominanteDroite()
	{
		if (T_Droite.isOn) {
			GameController.Jeu.Participant.MainDominante = "Droite";
		}
	}

	public void onChangeToggleJVOui()
	{
		if (T_Oui.isOn) {
			GameController.Jeu.Participant.PratiqueJeuxVideo = "Oui";
		}
	}

	public void onChangeToggleJVNon()
	{
		if (T_Non.isOn) {
			GameController.Jeu.Participant.PratiqueJeuxVideo = "Non";
		}
	}

	/*
	 * ** GESTION DU CLIQUE SUR LE BOUTON "lancer le test" **
	 */
	public void onClickButtonLancerTest()
	{
		// verifications de la validité des champs
		if (GameController.Jeu.Participant.numeroValide ()) {
			if (GameController.Jeu.Participant.ageValide ()) {
				Debug.Log(GameController.Jeu.Participant.ToString()+ "\n");
				UnityEngine.Application.LoadLevel ("Jeu");
			} else {
				Debug.Log("Age invalide \n");
				erreur = true;
				typeErreur = "Age invalide";
			}
		} else {
			Debug.Log("Numéro invalide \n");
			erreur = true;
			typeErreur = "Numéro invalide";

		}

	}

	/*
	 * ** GESTION DU CLIQUE SUR LE BOUTON "revenir au menu" **
	 */
	public void onClickButtonRevenirMenu()
	{
		GameController.Jeu.isPretest = true;
		UnityEngine.Application.LoadLevel ("menu");
	}

	/*
	 * Création graphique de la pop-up d'erreur si erreur.
	 */
	void OnGUI()
	{
		if (erreur) {
			UnityEngine.Rect rect = new Rect((Screen.width-largeurPopup)/2,(Screen.height-hauteurPopup)/2,largeurPopup,hauteurPopup);
			GUI.Window (1,rect,DoMyWindows,typeErreur);
		}

	}

	/*
	 * POP-UP
	 */
	void DoMyWindows (int id)
	{
		if (GUI.Button (new Rect ((largeurPopup-80)/2, (hauteurPopup-20)/2, 80, 20), "OK")) {
			erreur = false;
		}
	}
}
