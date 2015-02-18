using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.IO;
using System;

public class UIManagerScript : MonoBehaviour {
	
	public InputField IF_Taille_cible;
	public InputField IF_Hauteur_cible;
	public InputField IF_Distance_cible_lancepierre;
	public InputField IF_Gravité;
	public InputField IF_Rigidité_lancepierre;
	public InputField IF_Nombre_de_lancers;
	public InputField IF_Taille_du_projectile;
	public Toggle T_Afficher_le_score;
	public InputField IF_Nb_points_gagnes_par_cible;
	public InputField IF_Delai_lancer_projectile;
	public InputField IF_Delai_Evaluation_cible;
	
	void Start () {
		GameController.Jeu = new Jeu ();
		IF_Taille_cible.text = Convert.ToString(GameController.Jeu.Taille_cible);
		IF_Hauteur_cible.text = Convert.ToString(GameController.Jeu.Hauteur_cible);
		IF_Distance_cible_lancepierre.text = Convert.ToString(GameController.Jeu.Distance_cible_lancepierre);
		IF_Gravité.text = Convert.ToString(GameController.Jeu.Gravite);
		IF_Rigidité_lancepierre.text = Convert.ToString(GameController.Jeu.Rigidite_lancepierre);
		IF_Nombre_de_lancers.text = Convert.ToString(GameController.Jeu.Nb_lancers);
		IF_Taille_du_projectile.text = Convert.ToString(GameController.Jeu.Taille_projectile);
		T_Afficher_le_score.enabled = GameController.Jeu.Afficher_le_score;
		IF_Nb_points_gagnes_par_cible.text = Convert.ToString(GameController.Jeu.Nb_points_gagnes_par_cible);
		IF_Delai_lancer_projectile.text = Convert.ToString(GameController.Jeu.Delai_lancer_projectile);
		IF_Delai_Evaluation_cible.text = Convert.ToString(GameController.Jeu.Delai_evaluation_cible);
	}
	
	void Update () {
	
	}
	
	public void onValueChangeTaille_cible(){
		int res;
		if (int.TryParse (IF_Taille_cible.text, out res)) {
			GameController.Jeu.Taille_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeHauteur_cible(){
		int res;
		if (int.TryParse (IF_Hauteur_cible.text, out res)) {
			GameController.Jeu.Hauteur_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeDistance_cible_lancepierre(){
		int res;
		if (int.TryParse (IF_Distance_cible_lancepierre.text, out res)) {
			GameController.Jeu.Distance_cible_lancepierre = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeGravite(){
		int res;
		if (int.TryParse (IF_Gravité.text, out res)) {
			GameController.Jeu.Gravite = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeRigidité_lancepierre(){
		int res;
		if (int.TryParse (IF_Rigidité_lancepierre.text, out res)) {
			GameController.Jeu.Rigidite_lancepierre = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeNombre_de_lancer(){
		int res;
		if (int.TryParse (IF_Nombre_de_lancers.text, out res)) {
			GameController.Jeu.Nb_lancers = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeTaille_du_projectile(){
		int res;
		if (int.TryParse (IF_Taille_du_projectile.text, out res)) {
			GameController.Jeu.Taille_projectile = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeAfficher_le_score(){
		GameController.Jeu.Afficher_le_score = T_Afficher_le_score.enabled;
	}

	public void onValueChangeNb_points_gagnes_par_cible(){
		int res;
		if (int.TryParse (IF_Nb_points_gagnes_par_cible.text, out res)) {
			GameController.Jeu.Nb_points_gagnes_par_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeDelai_lancer_projectile(){
		int res;
		if (int.TryParse (IF_Delai_lancer_projectile.text, out res)) {
			GameController.Jeu.Delai_lancer_projectile = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeDelai_evaluation_cible(){
		int res;
		if (int.TryParse (IF_Delai_Evaluation_cible.text, out res)) {
			GameController.Jeu.Delai_evaluation_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onClickPreTest(){
		Debug.Log (GameController.Jeu);
	}

	public void onClickParamsModifier(){
		Debug.Log ("Modifier!");

		var path = EditorUtility.OpenFilePanel(
			"Sélectionner un fichier de configuration",
			"",
			"xml");

		Debug.Log ("Fichier choisi:" + path);

		ConfContainer confContainer = new ConfContainer ();
		confContainer.ConfEntries = new ConfEntry[2];
		confContainer.ConfEntries [0] = new ConfEntry ("test", "val");
		confContainer.ConfEntries [1] = new ConfEntry ("test2", "val2");
		Debug.Log (Application.persistentDataPath);
		confContainer.Save (Path.Combine (Application.dataPath, "test.xml"));

	}
}
