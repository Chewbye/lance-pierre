using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;

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

	private int _Score_courant;
	public int Score_courant {
		get {
			return _Score_courant;
		}
		set {
			_Score_courant = value;
		}
	}

	private int _Score_final;
	public int Score_final {
		get {
			return _Score_final;
		}
		set {
			_Score_final = value;
		}
	}

	private int _Nb_cible_touchees;
	public int Nb_cible_touchees {
		get {
			return _Nb_cible_touchees;
		}
		set {
			_Nb_cible_touchees = value;
		}
	}

	private int _Nb_cible_manquees;
	public int Nb_cible_manquees {
		get {
			return _Nb_cible_manquees;
		}
		set {
			_Nb_cible_manquees = value;
		}
	}

	private int _Tir_courant;
	public int Tir_courant {
		get {
			return _Tir_courant;
		}
		set {
			_Tir_courant = value;
		}
	}

	private bool[] _Ensemble_tirs;
	public bool[] _Un_tir {
		get {
			return _Ensemble_tirs;
		}
		set {
			_Ensemble_tirs = value;
		}
	}

	private double[] _Ensemble_distances;
	public double[] _Une_distance {
		get {
			return _Ensemble_distances;
		}
		set {
			_Ensemble_distances = value;
		}
	}

	private double[] _Ensemble_tailleCible;
	public double[] _Une_tailleCible {
		get {
			return _Ensemble_tailleCible;
		}
		set {
			_Ensemble_tailleCible = value;
		}
	}

	private double[] _Ensemble_temps;
	public double[] _Un_temps {
		get {
			return _Ensemble_temps;
		}
		set {
			_Ensemble_temps = value;
		}
	}

	/**
	 * Créé un modèle Jeu à partir d'un fichier de configuation
	 */
	public Jeu(Conf configuration){
		_Config = configuration;
		_Nb_lancers = _Config.Nb_lancers;
		_Score_courant = 0;
		_Score_final = 0;
		_Nb_cible_manquees = 0;
		_Nb_cible_touchees = 0;
		_Ensemble_tirs = new bool[_Nb_lancers];
		_Ensemble_distances = new double[_Nb_lancers];
		_Ensemble_tailleCible = new double[_Nb_lancers];
		_Ensemble_temps = new double[_Nb_lancers];
		_Tir_courant = 0;
		refreshConfigFiles ();
	}

	/**
	 * Créé un modèle Jeu avec des valeurs par défaut
	 */
	public Jeu(){
		_Config = new Conf ();
		_Nb_lancers = _Config.Nb_lancers;
		_Score_courant = 0;
		_Score_final = 0;
		_Nb_cible_manquees = 0;
		_Nb_cible_touchees = 0;
		_Ensemble_tirs = new bool[_Nb_lancers];
		_Ensemble_distances = new double[_Nb_lancers];
		_Ensemble_tailleCible = new double[_Nb_lancers];
		_Ensemble_temps = new double[_Nb_lancers];
		_Tir_courant = 0;
		refreshConfigFiles ();
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
			_ConfigsList.Add (new Conf (fileName.Replace("/", "\\")));
		}
	}


	public override string ToString(){
		string res = "";
		res +=  "NB_lancers=" + _Nb_lancers + System.Environment.NewLine 
			    + "ScoreFinal=" + _Score_final + System.Environment.NewLine 
				+ "NBCiblesTouchees=" + _Nb_cible_touchees + System.Environment.NewLine 
				+ "NBCiblesManquees=" + _Nb_cible_manquees + System.Environment.NewLine
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
		_Config.loadConfig(path);
	}
}
