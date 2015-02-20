using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.IO;
using System;
//using System.Windows.Forms;
using System.Runtime.InteropServices;

public class UIManagerScript : MonoBehaviour {
	
	public InputField IF_Taille_cible;
	public InputField IF_Hauteur_cible;
	public InputField IF_Distance_cible_lancepierre;
	public InputField IF_Gravite;
	public InputField IF_Rigidite_lancepierre;
	public InputField IF_Nb_lancers;
	public InputField IF_Taille_projectile;
	public Toggle T_Afficher_le_score;
	public InputField IF_Nb_points_gagnes_par_cible;
	public InputField IF_Delai_lancer_projectile;
	public InputField IF_Delai_evaluation_cible;
	public Text Label_fichier_config;

	[DllImport("user32.dll")]
	private static extern void OpenFileDialog(); //in your case : OpenFileDialog

	[DllImport("user32.dll")]
	private static extern void SaveFileDialog(); //in your case : OpenFileDialog
	
	void Start () {
		//Création du modèle Jeu au lancement de l'application
		GameController.Jeu = new Jeu ();
		refreshGUIFields ();
	}
	
	void Update () {
	
	}

	/**
	 * Met à jour les champs du menu principal avec la configuration actuelle du jeu 
	 */
	public void refreshGUIFields(){
		//Assignation des valeurs par défaut au modèle config
		IF_Taille_cible.text = Convert.ToString(GameController.Jeu.Config.Taille_cible);
		IF_Hauteur_cible.text = Convert.ToString(GameController.Jeu.Config.Hauteur_cible);
		IF_Distance_cible_lancepierre.text = Convert.ToString(GameController.Jeu.Config.Distance_cible_lancepierre);
		IF_Gravite.text = Convert.ToString(GameController.Jeu.Config.Gravite);
		IF_Rigidite_lancepierre.text = Convert.ToString(GameController.Jeu.Config.Rigidite_lancepierre);
		IF_Nb_lancers.text = Convert.ToString(GameController.Jeu.Config.Nb_lancers);
		IF_Taille_projectile.text = Convert.ToString(GameController.Jeu.Config.Taille_projectile);
		T_Afficher_le_score.enabled = GameController.Jeu.Config.Afficher_le_score;
		IF_Nb_points_gagnes_par_cible.text = Convert.ToString(GameController.Jeu.Config.Nb_points_gagnes_par_cible);
		IF_Delai_lancer_projectile.text = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		IF_Delai_evaluation_cible.text = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
	}
	
	public void onValueChangeTaille_cible(){
		int res;
		if (int.TryParse (IF_Taille_cible.text, out res)) {
			GameController.Jeu.Config.Taille_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeHauteur_cible(){
		int res;
		if (int.TryParse (IF_Hauteur_cible.text, out res)) {
			GameController.Jeu.Config.Hauteur_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeDistance_cible_lancepierre(){
		int res;
		if (int.TryParse (IF_Distance_cible_lancepierre.text, out res)) {
			GameController.Jeu.Config.Distance_cible_lancepierre = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeGravite(){
		int res;
		if (int.TryParse (IF_Gravite.text, out res)) {
			GameController.Jeu.Config.Gravite = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeRigidité_lancepierre(){
		int res;
		if (int.TryParse (IF_Rigidite_lancepierre.text, out res)) {
			GameController.Jeu.Config.Rigidite_lancepierre = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeNombre_de_lancer(){
		int res;
		if (int.TryParse (IF_Nb_lancers.text, out res)) {
			GameController.Jeu.Config.Nb_lancers = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeTaille_du_projectile(){
		int res;
		if (int.TryParse (IF_Taille_projectile.text, out res)) {
			GameController.Jeu.Config.Taille_projectile = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeAfficher_le_score(){
		GameController.Jeu.Config.Afficher_le_score = T_Afficher_le_score.enabled;
	}

	public void onValueChangeNb_points_gagnes_par_cible(){
		int res;
		if (int.TryParse (IF_Nb_points_gagnes_par_cible.text, out res)) {
			GameController.Jeu.Config.Nb_points_gagnes_par_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeDelai_lancer_projectile(){
		int res;
		if (int.TryParse (IF_Delai_lancer_projectile.text, out res)) {
			GameController.Jeu.Config.Delai_lancer_projectile = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onValueChangeDelai_evaluation_cible(){
		int res;
		if (int.TryParse (IF_Delai_evaluation_cible.text, out res)) {
			GameController.Jeu.Config.Delai_evaluation_cible = res;
		} else {
			Debug.Log ("Ceci n'est pas un entier!");
		}
	}

	public void onClickPreTest(){
		Application.LoadLevel ("jeu");
	}

	/**
	 * Charge un fichier de configuration 
	 */
	public void onClickParamsModifier(){
		Debug.Log ("Modifier!");

		System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

		openFileDialog1.InitialDirectory = Application.dataPath ;
		openFileDialog1.Filter = "Fichier de configuration (*.xml)|*.xml" ;
		openFileDialog1.RestoreDirectory = true ;

		if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK){
			string filename = Path.GetFileNameWithoutExtension (openFileDialog1.FileName);
			Debug.Log ("Fichier choisi:" + openFileDialog1.FileName);
			
			//Chargement du fichier
			GameController.Jeu.loadConfig(openFileDialog1.FileName);
			
			//Mis à jour du GUI avec la nouvelle config
			refreshGUIFields ();
			
			Label_fichier_config.text = filename;
		}


	}

	/**
	 * Sauvegarde la configuration actuelle dans  un fichier de configuration 
	 */
	public void onClickParamsSauvegarder(){
		Debug.Log ("Sauvegarder!");

		System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		
		saveFileDialog1.InitialDirectory = Application.dataPath ;
		saveFileDialog1.Filter = "Fichier de configuration (*.xml)|*.xml" ;
		saveFileDialog1.RestoreDirectory = true ;

		if(saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK){
			string filename = Path.GetFileNameWithoutExtension (saveFileDialog1.FileName);
			Debug.Log ("Fichier choisi:" + saveFileDialog1.FileName);
			GameController.Jeu.saveConfig(saveFileDialog1.FileName);
			Label_fichier_config.text = filename;
		}

	}
}

