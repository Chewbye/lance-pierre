using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	private double _Ratio_echelle;
	public double Ratio_echelle {
		get {
			return _Ratio_echelle;
		}
		set {
			_Ratio_echelle = value;
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

	// Indique le nombre courant de tirs réussis
	private int _Nb_cible_touchees;
	public int Nb_cible_touchees {
		get {
			return _Nb_cible_touchees;
		}
		set {
			_Nb_cible_touchees = value;
		}
	}

	// Indique le nombre courant de tirs manqués
	private int _Nb_cible_manquees;
	public int Nb_cible_manquees {
		get {
			return _Nb_cible_manquees;
		}
		set {
			_Nb_cible_manquees = value;
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

	//Liste contenant la position de la cible à chacun des tirs
	private List<PositionCible> _Positions_Cibles;
	public List<PositionCible> Positions_Cibles {
		get {
			return _Positions_Cibles;
		}
		set {
			_Positions_Cibles = value;
		}
	}

	//Liste contenant la taille de la cible à chacun des tirs
	private List<float> _Tailles_Cibles;
	public List<float> Tailles_Cibles {
		get {
			return _Tailles_Cibles;
		}
		set {
			_Tailles_Cibles = value;
		}
	}

	//Liste contenant la taille et poids du projectile à chacun des Tirs
	private List<Projectile> _Projectiles;
	public List<Projectile> Projectiles {
		get {
			return _Projectiles;
		}
		set {
			_Projectiles = value;
		}
	}

	// Indique combien de fois chacune des positions de cible doit apparaitre (nb taille cible * nb taille projectile * nb de série)
	private int _Occurence_Position_Cible;
	public int Occurence_Position_Cible {
		get {
			return _Occurence_Position_Cible;
		}
		set {
			_Occurence_Position_Cible = value;
		}
	}

	// Indique combien de fois chacune des tailles de cible doit apparaitre (nb position cible * nb taille projectile * nb de série)
	private int _Occurence_Taille_Cible;
	public int Occurence_Taille_Cible {
		get {
			return _Occurence_Taille_Cible;
		}
		set {
			_Occurence_Taille_Cible = value;
		}
	}

	// Indique combien de fois chacune des tailles de projectile doit apparaitre (nb position cible * nb taille cible * nb de série)
	private int _Occurence_Taille_Projectile;
	public int Occurence_Taille_Projectile {
		get {
			return _Occurence_Taille_Projectile;
		}
		set {
			_Occurence_Taille_Projectile = value;
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
		_Tir_courant = 0;
		_Occurence_Position_Cible = -1;
		_Occurence_Taille_Cible = -1;
		_Occurence_Taille_Projectile = -1;
		_Reussite_Tirs = new List<bool> ();
		_Positions_Cibles = new List<PositionCible> ();
		_Tailles_Cibles = new List<float> ();
		_Projectiles = new List<Projectile> ();
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
		_Tir_courant = 0;
		_Occurence_Position_Cible = -1;
		_Occurence_Taille_Cible = -1;
		_Occurence_Taille_Projectile = -1;
		_Reussite_Tirs = new List<bool> ();
		_Positions_Cibles = new List<PositionCible> ();
		_Tailles_Cibles = new List<float> ();
		_Projectiles = new List<Projectile> ();
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
		res += "NB_lancers=" + _Nb_lancers + System.Environment.NewLine 
						+ "ScoreFinal=" + _Score_final + System.Environment.NewLine 
						+ "NBCiblesTouchees=" + _Nb_cible_touchees + System.Environment.NewLine 
						+ "NBCiblesManquees=" + _Nb_cible_manquees + System.Environment.NewLine
						+ "TirCourant=" + _Tir_courant + System.Environment.NewLine
						+ "_Occurence_Position_Cible=" + _Occurence_Position_Cible + System.Environment.NewLine
						+ "_Occurence_Taille_Cible=" + _Occurence_Taille_Cible + System.Environment.NewLine
						+ "_Occurence_Taille_Projectile=" + _Occurence_Taille_Projectile + System.Environment.NewLine;
			
		return res;
	}

	/*Retourne true si position n'a pas été choisie trop de fois pendant la partie et l'ajoute dans la liste des positions jouées
	 * false sinon*/
	public bool PositionCiblePossible(PositionCible position)
	{
		int dejachoisie = 0;
		foreach (PositionCible p in _Positions_Cibles) 
		{
			if(p.Equals(position))
			{
				dejachoisie++;
			}
		}
		if (dejachoisie < _Occurence_Position_Cible) 
		{
			this._Positions_Cibles.Add(position);
			return true;
		} 
		else 
		{
			return false;
		}
	}

	/*Retourne true si taille n'a pas été choisie trop de fois pendant la partie et l'ajoute dans la liste des tailles jouées
	 * false sinon*/
	public bool TailleCiblePossible(float taille)
	{
		int dejachoisie = 0;
		foreach (float t in _Tailles_Cibles) 
		{
			if(t == taille)
			{
				dejachoisie++;
			}
		}
		if (dejachoisie < _Occurence_Taille_Cible) 
		{
			this._Tailles_Cibles.Add(taille);
			return true;
		} 
		else 
		{
			return false;
		}
	}

	/*Retourne true si taille(projectile) n'a pas été choisie trop de fois pendant la partie et l'ajoute dans la liste des projectiles jouées
	 * false sinon*/
	public bool TailleProjectilePossible(Projectile projectile)
	{
		int dejachoisie = 0;
		foreach (Projectile p in _Projectiles) 
		{
			if(p.Equals(projectile))
			{
				dejachoisie++;
			}
		}
		if (dejachoisie < _Occurence_Taille_Projectile) 
		{
			this._Projectiles.Add(projectile);
			return true;
		} 
		else 
		{
			return false;
		}
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
