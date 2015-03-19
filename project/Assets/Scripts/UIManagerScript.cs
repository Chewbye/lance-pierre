using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;
using System.Reflection;

public class UIManagerScript : MonoBehaviour {

	/* Fichier de style */
	public GUISkin menuStyle;

	/* Champ d'affichage du nombre de lancers */
	public GameObject textNBLancers;

	/* Champs du formulaire du menu */
	/* Onglet Général */
	public InputField IF_Gravite;
	public InputField IF_Nb_lancers;
	public Toggle T_Afficher_le_score;
	public InputField IF_Nb_series;
	
	/* Onglet Cibles */
	public InputField IF_Nb_points_gagnes_par_cible;


	/* Onglet Lance-pierre */
	public InputField IF_Rigidite_lancepierre;
	public InputField IF_Delai_lancer_projectile;

	/* Onglet Evaluation */
	public InputField IF_Delai_evaluation_cible;
	public InputField IF_Delai_validation_mesure_cible;
	public InputField IF_Marge_stabilisation_validation_cible;
	public Toggle T_Condition_De_Controle;
	public Toggle T_Condition_De_Perception;
	public Toggle T_Condition_De_Memoire;

	/* Liste des fichiers de configuration */
	public GameObject Configs_List_Panel;
	public UnityEngine.UI.Button prefabBoutonConfig;
	public Rect windowConfName;
	private bool renderWindowConfigName = false;
	private string newConfigName = "";
	public GameObject PanelBackground;

	public MenuTableManager menuTableManager;

	void Start () {

		windowConfName = new Rect((UnityEngine.Screen.width / 2) - 150, (UnityEngine.Screen.height / 2) - 60, 300, 120);

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


		refreshGUIFields ();

		//Affiche la liste des fichiers de configurations déja sauvegardés à l'ouverture de l'application
		foreach (Conf conf in GameController.Jeu.ConfigsList) {
			UnityEngine.UI.Button newConfigButton = CreateButton (prefabBoutonConfig, Configs_List_Panel, new Vector2 (0, 0), new Vector2 (0, 0));
			Text buttonText= newConfigButton.GetComponent<Text>();
			newConfigButton.GetComponentsInChildren<Text>()[0].text = conf.Name;
			string confName = conf.Name;
			AddListener(newConfigButton, conf.Name);
		}		

		menuTableManager.refreshGUITables();
	}
	
	void Update () {
	
	}

	void OnGUI() {
		GUI.skin = menuStyle;
		if (renderWindowConfigName) {
			windowConfName = GUI.Window (0, windowConfName, creerContenuWindowConfigName, "Sauvegarder la configuration");
		} else {
			PanelBackground.SetActive (false); //Cache le panel pour "désactiver" les éléments en dessous de la fenetre
		}
	}

	// Outside of method running the above
	void AddListener(UnityEngine.UI.Button b, string value)
	{
		b.onClick.AddListener(() => chargerFichierConfiguration(value));
	}
	
	/**
	 * Créé les éléments de la fenetre demandant le nom du fichier de configuration à sauvegarder
	 */
	public void creerContenuWindowConfigName(int windowID){
		GUI.FocusWindow(windowID);
		GUI.Label (new Rect ((windowConfName.width / 2) - 90, 20, 280, 30), "Nom du fichier de configuration:");
		newConfigName = GUI.TextField(new Rect(10, 40, 280, 30), newConfigName, 25); //Création du champs texte pour le nom du fichier de configuration
		PanelBackground.SetActive (true); //Affiche le panel pour "désactiver" les éléments en dessous de la fenetre

		if (GUI.Button (new Rect ((windowConfName.width / 4) - 50, 80, 100, 20), "Sauvegarder"))
			onClickParamsSauvegarder ();

		if (GUI.Button(new Rect((windowConfName.width / 4) * 3 - 50, 80, 100, 20), "Annuler"))
			renderWindowConfigName = false;//On cache la fenetre
	}

	public void afficherWindowConfigName(){
		renderWindowConfigName = true;
	}

	/** 
	 * Créé un bouton de chargement d'un fichier de configuration 
	 * @return le bouton de chargement d'un fichier de configuration 
	 **/
	public static UnityEngine.UI.Button CreateButton(UnityEngine.UI.Button buttonPrefab, GameObject canvas, Vector2 cornerTopRight, Vector2 cornerBottomLeft)
	{
		var button = UnityEngine.Object.Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as UnityEngine.UI.Button;
		var rectTransform = button.GetComponent<RectTransform>();
		rectTransform.SetParent(canvas.transform);
		rectTransform.anchorMax = cornerTopRight;
		rectTransform.anchorMin = cornerBottomLeft;
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.localScale = new Vector3 (1,1,1);
		return button;
	}

	/**
	 * Met à jour les champs du menu principal avec la configuration actuelle du jeu 
	 */
	public void refreshGUIFields(){
		//Assignation des valeurs par défaut au modèle config
		IF_Gravite.text = Convert.ToString(GameController.Jeu.Config.Gravite);
		IF_Rigidite_lancepierre.text = Convert.ToString(GameController.Jeu.Config.Rigidite_lancepierre);
		IF_Nb_lancers.text = Convert.ToString(GameController.Jeu.Config.Nb_lancers);
		T_Afficher_le_score.isOn = GameController.Jeu.Config.Afficher_le_score;
		IF_Nb_points_gagnes_par_cible.text = Convert.ToString(GameController.Jeu.Config.Nb_points_gagnes_par_cible);
		IF_Delai_lancer_projectile.text = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		IF_Delai_evaluation_cible.text = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
		IF_Delai_validation_mesure_cible.text = Convert.ToString(GameController.Jeu.Config.Delai_validation_mesure_cible);
		IF_Marge_stabilisation_validation_cible.text = Convert.ToString(GameController.Jeu.Config.Marge_stabilisation_validation_cible);
		IF_Nb_series.text = Convert.ToString (GameController.Jeu.Config.NB_series);
		T_Condition_De_Controle.isOn = GameController.Jeu.Config.Condition_De_Controle;
		T_Condition_De_Perception.isOn = GameController.Jeu.Config.Condition_De_Perception;
		T_Condition_De_Memoire.isOn = GameController.Jeu.Config.Condition_De_Memoire;
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
		}
	}

	public void onValueChangeAfficher_le_score(){
		GameController.Jeu.Config.Afficher_le_score = T_Afficher_le_score.isOn;
	}

	public void onValueChangeNb_points_gagnes_par_cible(){
		int res;
		if (int.TryParse (IF_Nb_points_gagnes_par_cible.text, out res)) {
			GameController.Jeu.Config.Nb_points_gagnes_par_cible = res;
		}
	}

	public void onValueChangeNb_series(){
		int res;
		if (int.TryParse (IF_Nb_series.text, out res)) {
			GameController.Jeu.Config.NB_series = res;
			textNBLancers.GetComponent<Text>().text = GameController.Jeu.Config.updateNB_Lancers ().ToString();
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

	public void onValueChangeDelai_validation_mesure_cible(){
		float res;
		if (float.TryParse (IF_Delai_validation_mesure_cible.text, out res)) {
			GameController.Jeu.Config.Delai_validation_mesure_cible = res;
		}
	}

	public void onValueChangeMarge_stabilisation_validation_cible(){
		float res;
		if (float.TryParse (IF_Marge_stabilisation_validation_cible.text, out res)) {
			GameController.Jeu.Config.Marge_stabilisation_validation_cible = res;
		}
	}

	public void onValueChangeCondition_de_controle(){
		GameController.Jeu.Config.Condition_De_Controle = T_Condition_De_Controle.isOn;
	}

	public void onValueChangeCondition_de_perception(){
		GameController.Jeu.Config.Condition_De_Perception = T_Condition_De_Perception.isOn;
	}

	public void onValueChangeCondition_de_memoire(){
		GameController.Jeu.Config.Condition_De_Memoire = T_Condition_De_Memoire.isOn;
	}



	/**
	 * Méthode appelée lorsqu'on clique sur le bouton "Pré-test Leap Motion"
	 */
	public void onClickPreTestLeapMotion(){
		launchGame ("evaluationTailleCible");
	}

	/**
	 * Méthode appelée lorsqu'on clique sur le bouton "Pré-test passation"
	 */
	public void onClickPreTestPassation(){
		launchGame ("jeu");
	}

	/**
	 * Méthode appelée lorsqu'on clique sur le bouton "Retour au menu"
	 */
	public void onClickRetourMenu(){
		launchGame ("menu");
	}

	/**
	 * Lance la scene du jeu en fonction de son nom sceneName
	 */
	public void launchGame(string sceneName){
		UnityEngine.Application.LoadLevel (sceneName);
	}

	/**
	 * Charge un fichier de configuration 
	 */
	public void chargerFichierConfiguration(string filename){
		Debug.Log ("Modifier!");

		string saveDirectory = UnityEngine.Application.dataPath;

		//Chargement du fichier
		GameController.Jeu.loadConfig(saveDirectory + "/" + filename + ".xml");
		
		//Mis à jour du GUI avec la nouvelle config
		refreshGUIFields ();

		//Mise à jour des tableaux
		menuTableManager.refreshGUITables();

		//Mise à jour du dernier fichier de configuration utilisé par l'application
		GameController.Jeu.AppConfig.LastConfName = filename;
		GameController.Jeu.AppConfig.saveConfig (UnityEngine.Application.dataPath + "/app.conf");
	}

	public void CallbackConfExistsDialog(DialogResult result){
		Debug.Log (result.ToString ());
	}

	/**
	 * Sauvegarde la configuration actuelle dans  un fichier de configuration 
	 */
	public void onClickParamsSauvegarder(){
		Debug.Log ("Sauvegarder!" + UnityEngine.Application.dataPath + "/" + newConfigName + ".xml");
		string saveDirectory = UnityEngine.Application.dataPath;

		/** Vérifie si un fichier de conf avec ce nom n'existe pas déja **/
		bool confExists = false;
		foreach (Conf conf in GameController.Jeu.ConfigsList) {
			if(conf.Name.Equals(newConfigName)){
				confExists = true;
			}
		}

		//Un fichier de configuration existe déja avec ce nom
		if (confExists) {
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			MessageBoxIcon icon = MessageBoxIcon.Warning;
			MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1;
			var result = MessageBox.Show("Un fichier de configuration possède déja le nom " + newConfigName + ". \nVoulez-vous le remplacer par celui-ci?", "Remplacer le fichier existant?", buttons, icon, defaultButton);
			if(result == DialogResult.No){
				renderWindowConfigName = false;
				return;
			}
		}

		GameController.Jeu.Config.Name = newConfigName;
		GameController.Jeu.saveConfig(UnityEngine.Application.dataPath + "/" + newConfigName + ".xml");

		//Si le fichier de conf n'existe pas déja
		if (!confExists) {
			/* Création et affichage de la nouvelle config dans la liste des fichiers de configuration */
			UnityEngine.UI.Button newConfigButton = CreateButton (prefabBoutonConfig, Configs_List_Panel, new Vector2 (0, 0), new Vector2 (0, 0));
			Text buttonText = newConfigButton.GetComponent<Text> ();
			newConfigButton.GetComponentsInChildren<Text> () [0].text = newConfigName;
		}

		renderWindowConfigName = false; //Cache la fenetre de choix du nom de fichier de configuration
	}
}

