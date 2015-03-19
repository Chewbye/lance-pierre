using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

public class Jeu{
	private Conf _Config;
	public Conf Config {
		get {
			return _Config;
		}
		set {
			_Config = value;
		}
	}

	private ArrayList _ConfigsList;
	public ArrayList ConfigsList {
		get {
			return _ConfigsList;
		}
		set {
			_ConfigsList = value;
		}
	}

	private int _Nb_lancers;
	public int Nb_lancers {
		get {
			return _Nb_lancers;
		}
		set {
			_Nb_lancers = value;
		}
	}

	private double _Ratio_echelle;
	public double Ratio_echelle {
		get {
			return _Ratio_echelle;
		}
		set {
			_Ratio_echelle = value;
		}
	}

	private int _Score;
	public int Score {
		get {
			return _Score;
		}
		set {
			_Score = value;
		}
	}

	// Indique le nombre courant de tirs effectués
	private int _Tir_courant;
	public int Tir_courant {
		get {
			return _Tir_courant;
		}
		set {
			_Tir_courant = value;
		}
	}

	// Nécessaire de garder la meme instance Random au cours de la partie
	private System.Random _Rang_Aleatoire;
	public System.Random Rang_Aleatoire {
		get {
			return _Rang_Aleatoire;
		}
		set {
			_Rang_Aleatoire = value;
		}
	}

	// Liste contenant la réussite ou non de chacun des tirs
	private List<bool> _Reussite_Tirs;
	public List<bool> Reussiste_Tirs {
		get {
			return _Reussite_Tirs;
		}
		set {
			_Reussite_Tirs = value;
		}
	}

	// Indique le temps restant pour le tir courant
	private float _Temps_Restant_Courant;
	public float Temps_Restant_Courant {
		get {
			return _Temps_Restant_Courant;
		}
		set {
			_Temps_Restant_Courant = value;
		}
	}

	// Liste contenant le temps mis par le joueur à tirer pour chacun des tirs
	private List<float> _Temps_Mis_Pour_Tirer;
	public List<float> Temps_Mis_Pour_Tirer {
		get {
			return _Temps_Mis_Pour_Tirer;
		}
		set {
			_Temps_Mis_Pour_Tirer = value;
		}
	}

	// Liste contenant l'ensemble des tirs à réaliser 
	private List<TripletTirs> _Tirs_A_Realiser;
	public List<TripletTirs> Tirs_A_Realiser {
		get {
			return _Tirs_A_Realiser;
		}
		set {
			_Tirs_A_Realiser = value;
		}
	}

	// Liste contenant l'ensemble des tirs réalisés 
	private List<TripletTirs> _Tirs_Realises;
	public List<TripletTirs> Tirs_Realises {
		get {
			return _Tirs_Realises;
		}
		set {
			_Tirs_Realises = value;
		}
	}
		
	// Liste contenant l'ensemble des mesures de la taille de la cible
	private List<float> _Mesures_Taille_Cible;
	public List<float> Mesures_Taille_Cible {
		get {
			return _Mesures_Taille_Cible;
		}
		set {
			_Mesures_Taille_Cible = value;
		}
	}

	//Configuration de l'application
	private AppConf _AppConfig;
	public AppConf AppConfig {
		get {
			return _AppConfig;
		}
		set {
			_AppConfig = value;
		}
	}

	/**
	 * Créé un modèle Jeu à partir d'un fichier de configuation
	 */
	public Jeu(Conf configuration){
		_Config = configuration;
		newGame ();
		refreshConfigFiles ();
	}

	/**
	 * Créé un modèle Jeu avec des valeurs par défaut
	 */
	public Jeu(){

		_Config = new Conf ();
		newGame ();
		refreshConfigFiles ();

		loadAppConfig (Application.dataPath + "/app.conf");

		//Charge la dernière configuration sélectionnée avant de quitter l'application
		if (!_AppConfig.LastConfName.Equals ("")) {
			loadConfig(Application.dataPath + "/" +_AppConfig.LastConfName + ".xml");
		}
	}

	/**
	 * Initialise les variables pour une nouvelle partie
	 */
	public void newGame(){
		_Nb_lancers = _Config.Nb_lancers;
		_Score = 0;
		_Tir_courant = 0;
		_Rang_Aleatoire = new System.Random();
		_Reussite_Tirs = new List<bool> ();
		_Tirs_A_Realiser = new List<TripletTirs> ();
		_Tirs_Realises = new List<TripletTirs> ();
		_Temps_Mis_Pour_Tirer = new List<float> ();
		_Mesures_Taille_Cible = new List<float> ();
	}
	

	/**
	 * Stocke la liste des modèles de configuration déja enregistrées dans l'attribut _ConfigsList par ordre de dates de création
	 */
	public void refreshConfigFiles(){
		//Récupération des noms des fichiers de configuration
		string[] fileNames = Directory.GetFiles(Application.dataPath, "*.xml");

		//Récupération des dates de création de chaque fichier
		DateTime[] creationTimes = new DateTime[fileNames.Length];
		for (int i=0; i < fileNames.Length; i++)
			creationTimes[i] = new FileInfo(fileNames[i]).CreationTime;

		//Tri du tableau fileNames par date de création
		Array.Sort(creationTimes,fileNames);

		_ConfigsList = new ArrayList ();
		//Transformation tableau vers ArrayList
		foreach (string fileName in fileNames) {
			_ConfigsList.Add (getConfigFile(fileName.Replace("/", "\\")));
		}
	}


	public override string ToString(){
		string res = "";
		res += "NB_lancers=" + _Nb_lancers + System.Environment.NewLine 
						+ "Score=" + _Score + System.Environment.NewLine 
				+ "TirCourant=" + _Tir_courant + System.Environment.NewLine;
			
		return res;
	}

	/* Sauvegarde la configuration du jeu actuel dans le fichier path */
	public void saveConfig(string path){
		_Config.saveConfig(path);
		refreshConfigFiles ();
	}

	/* Charge la configuration du fichier vers le jeu actuel */
	public void loadConfig(string path){
		_Config = getConfigFile (path);
	}

	/* Charge la configuration de l'application */
	public void loadAppConfig(string path){
		_AppConfig = getAppConfigFile (path);
	}

	public Conf getConfigFile(string path){
		Conf res = null;
		XmlSerializer xs = new XmlSerializer(typeof(Conf));
		using (StreamReader rd = new StreamReader(path))
		{
			res = xs.Deserialize(rd) as Conf;
		}
		return res;
	}

	public AppConf getAppConfigFile(string path){
		//Création du fichier de configuration de l'application la première fois
		if (!File.Exists (path)) {
			_AppConfig = new AppConf();
			if(_ConfigsList.Count>0)
				_AppConfig.LastConfName = ((Conf)_ConfigsList[_ConfigsList.Count - 1]).Name;
			_AppConfig.saveConfig(path);
		}

		AppConf res = null;
		XmlSerializer xs = new XmlSerializer(typeof(AppConf));
		using (StreamReader rd = new StreamReader(path))
		{
			res = xs.Deserialize(rd) as AppConf;
		}
		return res;
	}
}
