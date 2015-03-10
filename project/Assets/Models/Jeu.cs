﻿using UnityEngine;
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
		
	/**
	 * Créé un modèle Jeu à partir d'un fichier de configuation
	 */
	public Jeu(Conf configuration){
		_Config = configuration;
		_Nb_lancers = _Config.Nb_lancers;
		_Score = 0;
		_Tir_courant = 0;
		_Reussite_Tirs = new List<bool> ();
		_Tirs_A_Realiser = new List<TripletTirs> ();
		_Tirs_Realises = new List<TripletTirs> ();
		refreshConfigFiles ();
	}

	/**
	 * Créé un modèle Jeu avec des valeurs par défaut
	 */
	public Jeu(){
		_Config = new Conf ();
		_Nb_lancers = _Config.Nb_lancers;
		_Score = 0;
		_Tir_courant = 0;
		_Reussite_Tirs = new List<bool> ();
		_Tirs_A_Realiser = new List<TripletTirs> ();
		_Tirs_Realises = new List<TripletTirs> ();
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
		//_Config.loadConfig(path);
		_Config = getConfigFile (path);
	}

	public Conf getConfigFile(string path){
		//_Config.loadConfig(path);
		Conf res = null;
		XmlSerializer xs = new XmlSerializer(typeof(Conf));
		using (StreamReader rd = new StreamReader(path))
		{
			res = xs.Deserialize(rd) as Conf;
		}
		return res;
	}
}
