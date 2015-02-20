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

	/**
	 * Créé un modèle Jeu à partir d'un fichier de configuation
	 */
	public Jeu(Conf configuration){
		_Config = configuration;
		_Nb_lancers = _Config.Nb_lancers;
	}

	/**
	 * Créé un modèle Jeu avec des valeurs par défaut
	 */
	public Jeu(){
		_Config = new Conf ();
		_Nb_lancers = _Config.Nb_lancers;
	}


	public override string ToString(){
		string res = "";
		res += _Nb_lancers + System.Environment.NewLine;

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
