using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Text labelConfSelected;

	// Use this for initialization
	void Start () {
		//Création du modèle Jeu au lancement de l'application
		if (GameController.Jeu == null) {
			GameController.Jeu = new Jeu ();
		} else{
			GameController.Jeu.newGame();
		}

		//Affichage du nom de la configuration choisie
		if (!GameController.Jeu.Config.Name.Equals ("")) {
			labelConfSelected.text = GameController.Jeu.Config.Name;
		} else {
			//Initialisation des tableaux
			GameController.Jeu.Config.Positions_Cibles.Add (new PositionCible (1, 1));
			GameController.Jeu.Config.Tailles_Cibles.Add (1.0f);
			GameController.Jeu.Config.Projectiles.Add (new Projectile (1, 1));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClickConfiguration(){
		UnityEngine.Application.LoadLevel ("MainMenu");
	}

	public void onClickDemarrer(){
		UnityEngine.Application.LoadLevel ("Jeu");
	}
}
