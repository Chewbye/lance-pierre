using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Création du modèle Jeu au lancement de l'application
		if (GameController.Jeu == null) {
			GameController.Jeu = new Jeu ();
			
			//Initialisation des tableaux
			GameController.Jeu.Config.Positions_Cibles.Add (new PositionCible (1, 1));
			GameController.Jeu.Config.Tailles_Cibles.Add (1.0f);
			GameController.Jeu.Config.Projectiles.Add (new Projectile (1, 1));
		} else{
			GameController.Jeu.newGame();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClickConfiguration(){
		UnityEngine.Application.LoadLevel ("MainMenu");
	}
}
