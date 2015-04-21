using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

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

	private bool _Prise_en_compte_du_score;
	
	public bool Prise_en_compte_du_score {
		get {
			return _Prise_en_compte_du_score;
		}
		set {
			_Prise_en_compte_du_score = value;
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

	private int _Nb_points_perdus_par_cible_manque;
	
	public int Nb_points_perdus_par_cible_manque {
		get {
			return _Nb_points_perdus_par_cible_manque;
		}
		set {
			_Nb_points_perdus_par_cible_manque = value;
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

	private float _Delai_validation_mesure_cible;
	
	public float Delai_validation_mesure_cible {
		get {
			return _Delai_validation_mesure_cible;
		}
		set {
			_Delai_validation_mesure_cible = value;
		}
	}

	private float _Marge_stabilisation_validation_cible;
	
	public float Marge_stabilisation_validation_cible {
		get {
			return _Marge_stabilisation_validation_cible;
		}
		set {
			_Marge_stabilisation_validation_cible = value;
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

	private List<float> _Tailles_Cibles;
	public List<float> Tailles_Cibles {
		get {
			return _Tailles_Cibles;
		}
		set {
			_Tailles_Cibles = value;
		}
	}

	private List<Projectile> _Projectiles;
	public List<Projectile> Projectiles {
		get {
			return _Projectiles;
		}
		set {
			_Projectiles = value;
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

	private double _Taille_Hauteur_Catapulte;
	public double Taille_Hauteur_Catapulte {
		get {
			return _Taille_Hauteur_Catapulte;
		}
		set {
			_Taille_Hauteur_Catapulte = value;
		}
	}

	private double _Distance_X_Catapulte;
	public double Distance_X_Catapulte {
		get {
			return _Distance_X_Catapulte;
		}
		set {
			_Distance_X_Catapulte = value;
		}
	}
	
	private double _Distance_Y_Catapulte;
	public double Distance_Y_Catapulte {
		get {
			return _Distance_Y_Catapulte;
		}
		set {
			_Distance_Y_Catapulte = value;
		}
	}

	private int _NB_series;
	
	public int NB_series {
		get {
			return _NB_series;
		}
		set {
			_NB_series = value;
		}
	}

	private bool _Condition_De_Controle; 

	public bool Condition_De_Controle {
		get {
			return _Condition_De_Controle;
		}
		set {
			_Condition_De_Controle = value;
		}
	}

	private bool _Condition_De_Perception; 
	
	public bool Condition_De_Perception {
		get {
			return _Condition_De_Perception;
		}
		set {
			_Condition_De_Perception = value;
		}
	}

	private bool _Condition_De_Memoire; 
	
	public bool Condition_De_Memoire {
		get {
			return _Condition_De_Memoire;
		}
		set {
			_Condition_De_Memoire = value;
		}
	}

	private float _Delai_avant_evaluation_cible;
	
	public float Delai_avant_evaluation_cible {
		get {
			return _Delai_avant_evaluation_cible;
		}
		set {
			_Delai_avant_evaluation_cible = value;
		}
	}

	private float _Delai_avant_disparition_cible;
	
	public float Delai_avant_disparition_cible {
		get {
			return _Delai_avant_disparition_cible;
		}
		set {
			_Delai_avant_disparition_cible = value;
		}
	}

	private bool _Affichage_barre_progression;

	public bool Affichage_barre_progression {
		get {
			return _Affichage_barre_progression;
		}
		set {
			_Affichage_barre_progression = value;
		}
	}

	private int _Largeur_barre_progression;

	public int Largeur_barre_progression {
		get {
			return _Largeur_barre_progression;
		}
		set {
			_Largeur_barre_progression = value;
		}
	}

	private int _Hauteur_barre_progression;

	public int Hauteur_barre_progression {
		get {
			return _Hauteur_barre_progression;
		}
		set {
			_Hauteur_barre_progression = value;
		}
	}

	public string _Couleur_cible;

	public string Couleur_cible {
		get {
			return _Couleur_cible;
		}
		set {
			_Couleur_cible = value;
		}
	}

	private Color32 _Couleur_barre;

	public Color32 Couleur_barre {
		get {
			return _Couleur_barre;
		}
		set {
			_Couleur_barre = value;
		}
	}

	private bool _Afficher_effet_destruction_cible;
	
	public bool Afficher_effet_destruction_cible {
		get {
			return _Afficher_effet_destruction_cible;
		}
		set {
			_Afficher_effet_destruction_cible = value;
		}
	}

	/**
	 * Créé un modèle Conf à partir d'un fichier de configuation
	 */
	public Conf(string confPath){
		_Name = Path.GetFileNameWithoutExtension (confPath);
		_Positions_Cibles = new List<PositionCible> (); //TEMPORAIRE
		_Tailles_Cibles = new List<float> (); //TEMPORAIRE
		_Projectiles = new List<Projectile> (); //TEMPORAIRE
	}
	
	/**
	 * Créé un modèle Conf avec des valeurs par défaut
	 */
	public Conf(){
		_Name = "";
		_Gravite = 1;
		_Rigidite_lancepierre = 3.0f;
		_Nb_lancers = 1;
		_Afficher_le_score = true;
		_Prise_en_compte_du_score = true;
		_Nb_points_gagnes_par_cible = 1;
		_Nb_points_perdus_par_cible_manque = 1;
		_Delai_lancer_projectile = 20;
		_Delai_evaluation_cible = 1.0f;
		_Delai_validation_mesure_cible = 1.0f;
		_Marge_stabilisation_validation_cible = 0.5f;
		_Positions_Cibles = new List<PositionCible> ();
		_Tailles_Cibles = new List<float> ();
		_Projectiles = new List<Projectile> ();
		_Ratio_echelle = 1;
		_Taille_Hauteur_Catapulte = 3;
		_Distance_X_Catapulte = -10;
		_Distance_Y_Catapulte = -4;
		_NB_series = 1;
		_Condition_De_Controle = true;
		_Condition_De_Perception = false;
		_Condition_De_Memoire = false;
		_Delai_avant_evaluation_cible = 2;
		_Delai_avant_disparition_cible = 2;
		_Affichage_barre_progression = true;
		_Largeur_barre_progression = 512;
		_Hauteur_barre_progression = 256;
		_Couleur_cible = "Rouge";
		_Couleur_barre = new Color32(95, 222, 95, 255);
		_Afficher_effet_destruction_cible = false;
	}
	
	
	public override string ToString(){
		string res = "";
		res += "_Gravite: " + _Gravite + System.Environment.NewLine;
		res += "_Rigidite_lancepierre: " +_Rigidite_lancepierre + System.Environment.NewLine;
		res += "_Nb_lancers: " +_Nb_lancers + System.Environment.NewLine;
		res += "_Afficher_le_score: " + Convert.ToString(_Afficher_le_score) + System.Environment.NewLine;
		res += "_Prise_en_compte_du_score: " + Convert.ToString(_Prise_en_compte_du_score) + System.Environment.NewLine;
		res += "_Nb_points_gagnes_par_cible: " + _Nb_points_gagnes_par_cible + System.Environment.NewLine;
		res += "_Nb_points_perdus_par_cible_manque: " + _Nb_points_perdus_par_cible_manque + System.Environment.NewLine;
		res += "_Delai_lancer_projectile: " + _Delai_lancer_projectile + System.Environment.NewLine;
		res += "_Delai_evaluation_cible: " + _Delai_evaluation_cible + System.Environment.NewLine;
		res += "_Delai_validation_mesure_cible: " + _Delai_validation_mesure_cible + System.Environment.NewLine;
		res += "_Marge_stabilisation_validation_cible: " + _Marge_stabilisation_validation_cible + System.Environment.NewLine;
		res += "_Ratio_echelle: " + _Ratio_echelle + System.Environment.NewLine;
		res += "_Taille_Hauteur_Catapulte: " + _Taille_Hauteur_Catapulte + System.Environment.NewLine;
		res += "_Distance_X_Catapulte: " + _Distance_X_Catapulte + System.Environment.NewLine;
		res += "_Distance_Y_Catapulte: " + _Distance_Y_Catapulte + System.Environment.NewLine;
		res += "_NB_series: " + _NB_series + System.Environment.NewLine;
		res += "_Condition_De_Controle: " + Convert.ToString(_Condition_De_Controle) + System.Environment.NewLine;
		res += "_Condition_De_Perception: " + Convert.ToString(_Condition_De_Perception) + System.Environment.NewLine;
		res += "_Condition_De_Memoire: " + Convert.ToString(_Condition_De_Memoire) + System.Environment.NewLine;
		res += "_Delai_avant_evaluation_cible: " + Convert.ToString(_Delai_avant_evaluation_cible) + System.Environment.NewLine;
		res += "_Delai_avant_disparition_cible: " + Convert.ToString(_Delai_avant_disparition_cible) + System.Environment.NewLine;
		res += "_Affichage_barre_progression: " + Convert.ToString(_Affichage_barre_progression) + System.Environment.NewLine;
		res += "_Largeur_barre_progression: " + Convert.ToString(_Largeur_barre_progression) + System.Environment.NewLine;
		res += "_Hauteur_barre_progression: " + Convert.ToString(_Hauteur_barre_progression) + System.Environment.NewLine;
		res += "_Couleur_cible: " + Convert.ToString(_Couleur_cible) + System.Environment.NewLine;
		res += "_Couleur_barre: " + Convert.ToString(_Couleur_barre) + System.Environment.NewLine;
		res += "_Afficher_effet_destruction_cible: " + Convert.ToString(_Afficher_effet_destruction_cible) + System.Environment.NewLine;

		foreach (PositionCible poscible in _Positions_Cibles) {
			res += "POSITION CIBLE: " + System.Environment.NewLine;
			res += poscible.toString() + System.Environment.NewLine;;
		}

		foreach (float taillecible in _Tailles_Cibles) {
			res += "TAILLE CIBLE: " + System.Environment.NewLine;
			res += taillecible + System.Environment.NewLine;;
		}

		foreach (Projectile proj in _Projectiles) {
			res += "TAILLE PROJECTILE: " + System.Environment.NewLine;
			res += proj.toString() + System.Environment.NewLine;;
		}

		return res;
	}
	
	/* Sauvegarde la configuration du jeu actuel dans le fichier path */
	public void saveConfig(string path){
		XmlSerializer xs = new XmlSerializer(typeof(Conf));
		using (StreamWriter wr = new StreamWriter(path))
		{
			xs.Serialize(wr, this);
		}
	}

	/*
	 * Met à jour le nombre de lancers
	 */
	public int updateNB_Lancers(){
		_Nb_lancers = _Positions_Cibles.Count * _Tailles_Cibles.Count * _Projectiles.Count * _NB_series ;
		return _Nb_lancers;
	}
}
