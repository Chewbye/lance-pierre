using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;

public class Conf{
	private string _Name;
	public string Name {
		get {
			return _Name;
		}
		set {
			_Name = value;
		}
	}

	private float _Gravite;
	
	public float Gravite {
		get {
			return _Gravite;
		}
		set {
			_Gravite = value;
		}
	}
	
	private float _Rigidite_lancepierre;
	
	public float Rigidite_lancepierre {
		get {
			return _Rigidite_lancepierre;
		}
		set {
			_Rigidite_lancepierre = value;
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

	private bool _Afficher_le_score;
	
	public bool Afficher_le_score {
		get {
			return _Afficher_le_score;
		}
		set {
			_Afficher_le_score = value;
		}
	}
	
	private int _Nb_points_gagnes_par_cible;
	
	public int Nb_points_gagnes_par_cible {
		get {
			return _Nb_points_gagnes_par_cible;
		}
		set {
			_Nb_points_gagnes_par_cible = value;
		}
	}
	
	private float _Delai_lancer_projectile;
	
	public float Delai_lancer_projectile {
		get {
			return _Delai_lancer_projectile;
		}
		set {
			_Delai_lancer_projectile = value;
		}
	}
	
	private float _Delai_evaluation_cible;
	
	public float Delai_evaluation_cible {
		get {
			return _Delai_evaluation_cible;
		}
		set {
			_Delai_evaluation_cible = value;
		}
	}

	private List<PositionCible> _Positions_Cibles;
	public List<PositionCible> Positions_Cibles {
		get {
			return _Positions_Cibles;
		}
		set {
			_Positions_Cibles = value;
		}
	}


	/**
	 * Créé un modèle Conf à partir d'un fichier de configuation
	 */
	public Conf(string confPath){
		_Name = Path.GetFileNameWithoutExtension (confPath);
		this.loadConfig (confPath);
		_Positions_Cibles = new List<PositionCible> (); //TEMPORAIRE
	}
	
	/**
	 * Créé un modèle Conf avec des valeurs par défaut
	 */
	public Conf(){
		_Name = "";
		_Gravite = 9.81f;
		_Rigidite_lancepierre = 1.0f;
		_Nb_lancers = 1;
		_Afficher_le_score = true;
		_Nb_points_gagnes_par_cible = 1;
		_Delai_lancer_projectile = 1.0f;
		_Delai_evaluation_cible = 1.0f;
		_Positions_Cibles = new List<PositionCible> ();
	}
	
	
	public override string ToString(){
		string res = "";
		res += _Gravite + System.Environment.NewLine;
		res += _Rigidite_lancepierre + System.Environment.NewLine;
		res += _Nb_lancers + System.Environment.NewLine;
		res += Convert.ToString(_Afficher_le_score) + System.Environment.NewLine;
		res += _Nb_points_gagnes_par_cible + System.Environment.NewLine;
		res += _Delai_lancer_projectile + System.Environment.NewLine;
		res += _Delai_evaluation_cible + System.Environment.NewLine;

		foreach (PositionCible poscible in _Positions_Cibles) {
			res += "POSITION CIBLE: " + System.Environment.NewLine;
			res += poscible.toString();
		}

		return res;
	}
	
	/* Sauvegarde la configuration du jeu actuel dans le fichier path */
	public void saveConfig(string path){
		ConfContainer confContainer = new ConfContainer ();
		Type type = this.GetType();
		PropertyInfo[] properties = type.GetProperties();

		confContainer.ConfEntries = new ConfEntry[properties.Length];
		int i = 0;
		foreach (PropertyInfo property in properties){
			confContainer.ConfEntries [i] = new ConfEntry (property.Name, Convert.ToString(property.GetValue(this, null)));
			i++;
		}
		
		confContainer.Save (path);
	}
	
	/* Charge la configuration du fichier vers le jeu actuel */
	public void loadConfig(string path){
		ConfContainer confContainer = ConfContainer.Load (path);
		
		Type type = this.GetType();
		PropertyInfo pi; 
		foreach (ConfEntry confEntry in confContainer.ConfEntries){

			pi = type.GetProperty(confEntry.Attribut);
			int resInt;
			bool resBool;
			float resFloat;

			if (int.TryParse (confEntry.Valeur, out resInt)) {
				pi.SetValue(this, resInt, null);
			} else if (float.TryParse (confEntry.Valeur, out resFloat)) {
				pi.SetValue(this, resFloat, null); 
			} else if(bool.TryParse (confEntry.Valeur, out resBool)){
				pi.SetValue(this, resBool, null);
			} else{
				//pi.SetValue(this, confEntry.Valeur, null);
			}


		}
	}
}
