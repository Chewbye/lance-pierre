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

	private int _Nb_lancers;
	public int Nb_lancers {
		get {
			return _Nb_lancers;
		}
		set {
			_Nb_lancers = value;
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

	/**
	 * Créé un modèle Jeu à partir d'un fichier de configuation
	 */
	public Jeu(Conf configuration){
		_Config = configuration;
		_Nb_lancers = _Config.Nb_lancers;
		_Score_final = 0;
		_Nb_cible_manquees = 0;
		_Nb_cible_touchees = 0;
		_Ensemble_tirs = new bool[_Nb_lancers];
		_Tir_courant = 0;
	}

	/**
	 * Créé un modèle Jeu avec des valeurs par défaut
	 */
	public Jeu(){
		_Config = new Conf ();
		_Nb_lancers = _Config.Nb_lancers;
		_Score_final = 0;
		_Nb_cible_manquees = 0;
		_Nb_cible_touchees = 0;
		_Ensemble_tirs = new bool[_Nb_lancers];
		_Tir_courant = 0;
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
	}

	/* Charge la configuration du fichier vers le jeu actuel */
	public void loadConfig(string path){
		_Config.loadConfig(path);
	}
}
