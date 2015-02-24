using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
//using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

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
	public GameObject Configs_List_Panel;
	public Button prefabBoutonConfig;
	public Rect windowConfName;
	private bool renderWindowConfigName = false;

	[DllImport("user32.dll")]
	private static extern void OpenFileDialog(); //in your case : OpenFileDialog

	[DllImport("user32.dll")]
	private static extern void SaveFileDialog(); //in your case : OpenFileDialog
	
	void Start () {
		windowConfName = new Rect((Screen.width / 2) - 200, (Screen.height / 2) - 50, 400, 100);

		//Création du modèle Jeu au lancement de l'application
		GameController.Jeu = new Jeu ();
		refreshGUIFields ();

		//Affiche la liste des fichiers de configurations déja sauvegardés à l'ouverture de l'application
		foreach (Conf conf in GameController.Jeu.ConfigsList) {
			Button newConfigButton = CreateButton (prefabBoutonConfig, Configs_List_Panel, new Vector2 (0, 0), new Vector2 (0, 0));
			Text buttonText= newConfigButton.GetComponent<Text>();
			newConfigButton.GetComponentsInChildren<Text>()[0].text = conf.Name;
		}			
	}
	
	void Update () {
	
	}

	void OnGUI() {
		if(renderWindowConfigName)
			windowConfName = GUI.Window(0, windowConfName, creerContenuWindowConfigName, "Sauvegarder la configuration");
	}

	public void creerContenuWindowConfigName(int windowID){
		string stringToEdit = "Hello World";
		stringToEdit = GUI.TextField(new Rect(10, 20, 200, 30), stringToEdit, 25);

		if (GUI.Button(new Rect(10, 50, 100, 20), "Hello World"))
			print("Got a click");
	}

	public void afficherWindowConfigName(){
		renderWindowConfigName = true;
	}

	public static Button CreateButton(Button buttonPrefab, GameObject canvas, Vector2 cornerTopRight, Vector2 cornerBottomLeft)
	{
		var button = UnityEngine.Object.Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as Button;
		var rectTransform = button.GetComponent<RectTransform>();
		rectTransform.SetParent(canvas.transform);
		rectTransform.anchorMax = cornerTopRight;
		rectTransform.anchorMin = cornerBottomLeft;
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
		return button;
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
		float res;
		if (float.TryParse (IF_Taille_cible.text, out res)) {
			GameController.Jeu.Config.Taille_cible = res;
		}
	}

	public void onValueChangeHauteur_cible(){
		float res;
		if (float.TryParse (IF_Hauteur_cible.text, out res)) {
			GameController.Jeu.Config.Hauteur_cible = res;
		}
	}

	public void onValueChangeDistance_cible_lancepierre(){
		float res;
		if (float.TryParse (IF_Distance_cible_lancepierre.text, out res)) {
			GameController.Jeu.Config.Distance_cible_lancepierre = res;
		}
	}

	public void onValueChangeGravite(){
		float res;
		if (float.TryParse (IF_Gravite.text, out res)) {
			GameController.Jeu.Config.Gravite = res;
		}
	}

	public void onValueChangeRigidité_lancepierre(){
		float res;
		if (float.TryParse (IF_Rigidite_lancepierre.text, out res)) {
			GameController.Jeu.Config.Rigidite_lancepierre = res;
		}
	}

	public void onValueChangeNombre_de_lancer(){
		int res;
		if (int.TryParse (IF_Nb_lancers.text, out res)) {
			GameController.Jeu.Config.Nb_lancers = res;
			GameController.Jeu._Un_tir = new bool[res];
			GameController.Jeu._Une_tailleCible = new double[res];
			GameController.Jeu._Une_distance = new double[res];
			GameController.Jeu._Un_temps = new double[res];
		}
	}

	public void onValueChangeTaille_du_projectile(){
		float res;
		if (float.TryParse (IF_Taille_projectile.text, out res)) {
			GameController.Jeu.Config.Taille_projectile = res;
		}
	}

	public void onValueChangeAfficher_le_score(){
		GameController.Jeu.Config.Afficher_le_score = T_Afficher_le_score.enabled;
	}

	public void onValueChangeNb_points_gagnes_par_cible(){
		int res;
		if (int.TryParse (IF_Nb_points_gagnes_par_cible.text, out res)) {
			GameController.Jeu.Config.Nb_points_gagnes_par_cible = res;
		}
	}

	public void onValueChangeDelai_lancer_projectile(){
		float res;
		if (float.TryParse (IF_Delai_lancer_projectile.text, out res)) {
			GameController.Jeu.Config.Delai_lancer_projectile = res;
		}
	}

	public void onValueChangeDelai_evaluation_cible(){
		float res;
		if (float.TryParse (IF_Delai_evaluation_cible.text, out res)) {
			GameController.Jeu.Config.Delai_evaluation_cible = res;
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
		string saveDirectory = Application.dataPath;
		string filename = Path.GetFileNameWithoutExtension ("test4.xml");

		/*
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
		*/

	}
}

