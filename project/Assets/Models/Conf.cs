using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;

public class Conf{
	private int _Taille_cible;
	
	public int Taille_cible {
		get {
			return _Taille_cible;
		}
		set {
			_Taille_cible = value;
		}
	}
	
	private int _Hauteur_cible;
	
	public int Hauteur_cible {
		get {
			return _Hauteur_cible;
		}
		set {
			_Hauteur_cible = value;
		}
	}
	
	private int _Distance_cible_lancepierre;
	
	public int Distance_cible_lancepierre {
		get {
			return _Distance_cible_lancepierre;
		}
		set {
			_Distance_cible_lancepierre = value;
		}
	}
	
	private int _Gravite;
	
	public int Gravite {
		get {
			return _Gravite;
		}
		set {
			_Gravite = value;
		}
	}
	
	private int _Rigidite_lancepierre;
	
	public int Rigidite_lancepierre {
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
	
	private int _Taille_projectile;
	
	public int Taille_projectile {
		get {
			return _Taille_projectile;
		}
		set {
			_Taille_projectile = value;
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
	
	private int _Delai_lancer_projectile;
	
	public int Delai_lancer_projectile {
		get {
			return _Delai_lancer_projectile;
		}
		set {
			_Delai_lancer_projectile = value;
		}
	}
	
	private int _Delai_evaluation_cible;
	
	public int Delai_evaluation_cible {
		get {
			return _Delai_evaluation_cible;
		}
		set {
			_Delai_evaluation_cible = value;
		}
	}
	
	/**
	 * Créé un modèle Conf à partir d'un fichier de configuation
	 */
	public Conf(string confPath){
		this.loadConfig (confPath);
	}
	
	/**
	 * Créé un modèle Conf avec des valeurs par défaut
	 */
	public Conf(){
		_Taille_cible = 0;
		_Hauteur_cible = 0;
		_Distance_cible_lancepierre = 0;
		_Gravite = 0;
		_Rigidite_lancepierre = 0;
		_Nb_lancers = 0;
		_Taille_projectile = 0;
		_Afficher_le_score = true;
		_Nb_points_gagnes_par_cible = 0;
		_Delai_lancer_projectile = 0;
		_Delai_evaluation_cible = 0;
	}
	
	
	public override string ToString(){
		string res = "";
		res += _Taille_cible + System.Environment.NewLine;
		res += _Hauteur_cible + System.Environment.NewLine;
		res += _Distance_cible_lancepierre + System.Environment.NewLine;
		res += _Gravite + System.Environment.NewLine;
		res += _Rigidite_lancepierre + System.Environment.NewLine;
		res += _Nb_lancers + System.Environment.NewLine;
		res += _Taille_projectile + System.Environment.NewLine;
		res += Convert.ToString(_Afficher_le_score) + System.Environment.NewLine;
		res += _Nb_points_gagnes_par_cible + System.Environment.NewLine;
		res += _Delai_lancer_projectile + System.Environment.NewLine;
		res += _Delai_evaluation_cible + System.Environment.NewLine;
		
		return res;
	}
	
	/* Sauvegarde la configuration du jeu actuel dans le fichier path */
	public void saveConfig(string path){
		ConfContainer confContainer = new ConfContainer ();
		confContainer.ConfEntries = new ConfEntry[11];
		
		Type type = this.GetType();
		PropertyInfo[] properties = type.GetProperties();
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
			UnityEngine.Debug.Log(confEntry.Attribut + " " + confEntry.Valeur + " " + (pi.GetType()));
			int resInt;
			bool resBool;
			if (int.TryParse (confEntry.Valeur, out resInt)) {
				pi.SetValue(this, resInt, null);
			} else if(bool.TryParse (confEntry.Valeur, out resBool)){
				pi.SetValue(this, resBool, null);
			}


		}
	}
}
