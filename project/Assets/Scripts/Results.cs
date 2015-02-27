using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public class Results : MonoBehaviour {
	
	//Configuration
	public Text taille_cible;
	public Text hauteur_cible;
	public Text distance_cible_lancepierre;
	public Text gravite;
	public Text rigidite_lancepierre;
	public Text nb_lancers;
	public Text taille_projectile;
	public Toggle afficher_le_score;
	public Text nb_points_gagnes_par_cible;
	public Text delai_lancer_projectile;
	public Text delai_evaluation_cible;
	
	//Récapitulatif
	public Text score_final;
	public Text nombre_cibles_touchees;
	public Text nombre_cibles_manquees;
	public Text nombre_evaluations_moins_1cm;
	public Text nombre_evaluations_environ_2cm;
	public Text nombre_evaluations_environ_3cm;
	public Text nombre_evaluations_environ_4cm;
	public Text nombre_evaluations_environ_5cm;
	public Text nombre_evaluations_sup_5cm_inf_10cm;
	public Text nombre_evaluations_sup_10cm;
	public Text temps_evaluation_cible;
	
	string fichierCourant;
	string nomDossier;
	
	// Use this for initialization
	void Start () {
		if (GameController.Jeu == null) 
			GameController.Jeu = new Jeu ();
		setFields ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/**
	 * Met à jour les champs du menu principal avec la configuration actuelle du jeu 
	 */
	public void setFields(){
		//Assignation des valeurs définies dans le fichier de config choisi initialement
		taille_cible.text = Convert.ToString(GameController.Jeu.Config.Taille_cible);
		hauteur_cible.text = Convert.ToString(GameController.Jeu.Config.Hauteur_cible);
		distance_cible_lancepierre.text = Convert.ToString(GameController.Jeu.Config.Distance_cible_lancepierre);
		gravite.text = Convert.ToString(GameController.Jeu.Config.Gravite);
		rigidite_lancepierre.text = Convert.ToString(GameController.Jeu.Config.Rigidite_lancepierre);
		nb_lancers.text = Convert.ToString(GameController.Jeu.Config.Nb_lancers);
		taille_projectile.text = Convert.ToString(GameController.Jeu.Config.Taille_projectile);
		afficher_le_score.enabled = GameController.Jeu.Config.Afficher_le_score;
		nb_points_gagnes_par_cible.text = Convert.ToString(GameController.Jeu.Config.Nb_points_gagnes_par_cible);
		delai_lancer_projectile.text = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		delai_evaluation_cible.text = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
		
		//Assignation des valeurs par traitement des résultats obtenus durant la partie
		//A remplacer par les résultats stockés par Pierre
		score_final.text = Convert.ToString(GameController.Jeu.Score_final);
		nombre_cibles_touchees.text = Convert.ToString(GameController.Jeu.Nb_cible_touchees);
		nombre_cibles_manquees.text = Convert.ToString(GameController.Jeu.Nb_cible_manquees);
		//Ici on comptera le nombre de valeurs correspondantes
		/*nombre_evaluations_moins_1cm.text = "2"; //- 1cm
		nombre_evaluations_environ_2cm.text = "5"; //entre 1 et 2,4cm
		nombre_evaluations_environ_3cm.text = "10"; //entre 2,5 et 3,4cm
		nombre_evaluations_environ_4cm.text = "3"; //entre 3,5 et 4,4cm
		nombre_evaluations_environ_5cm.text = "0"; //entre 4,5 et 4,9cm
		nombre_evaluations_sup_5cm_inf_10cm.text = "0"; //entre 5 et 9,9cm
		nombre_evaluations_sup_10cm.text = "0"; //+ 10cm
		temps_evaluation_cible.text = "2"; //Ici on fera une moyenne des temps*/
		
		//writeCSV();
		writeXLS ();
	}
	
	public void writeCSV() {
		string text; 
		
		// Version 1
		/*
		FileStream fs = File.Open("resultats.csv", FileMode.Append);
		text = "Configuration;\n"; // Partie configuration

		text = text + "Nom du fichier de configuration;maConfig;\n"; //à remplacer par le bon nom de config

		text = text + "Taille de la cible (cm);";
		text = text +  taille_cible.text + ";\n";

		text = text + "Hauteur de la cible (cm);";
		text = text +  hauteur_cible.text + ";\n";

		text = text + "Distance separant la cible du lance-pierre (cm);";
		text = text +  distance_cible_lancepierre.text + ";\n";

		text = text + "Gravite;";
		text = text +  gravite.text + ";\n";

		text = text + "Rigidite du lance-pierre;";
		text = text +  rigidite_lancepierre.text + ";\n";

		text = text + "Nombre de lancers;";
		text = text +  nb_lancers.text + ";\n";

		text = text + "Taille des projectiles (cm);";
		text = text +  taille_projectile.text + ";\n";

		text = text + "Afficher le score;";

		if (afficher_le_score.enabled == true) {
			text = text + "Oui;\n";
		} else {
			text = text + "Non;\n";
		}

		text = text + "Nombre de points gagnes par cible (points);";
		text = text +  nb_points_gagnes_par_cible.text + ";\n";

		text = text + "Delai avant de pouvoir lancer le projectile (secondes);";
		text = text +  delai_lancer_projectile.text + ";\n";

		text = text + "Delai avant de pouvoir evaluer la taille de la cible (secondes);";
		text = text +  delai_evaluation_cible.text + ";\n\n";

		text = text + "Recapitulatif;\n"; // Partie récapitulatif

		text = text + "Score final (points);";
		text = text +  score_final.text + ";\n";

		text = text + "Nombre de cibles touchees;";
		text = text +  nombre_cibles_touchees.text + ";\n";

		text = text + "Nombre de cibles manquees;";
		text = text +  nombre_cibles_manquees.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a moins de 1cm pres;";
		text = text +  nombre_evaluations_moins_1cm.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a environ 2cm pres;";
		text = text +  nombre_evaluations_environ_2cm.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a environ 3cm pres;";
		text = text +  nombre_evaluations_environ_3cm.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a environ 4cm pres;";
		text = text +  nombre_evaluations_environ_4cm.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a environ 5cm pres;";
		text = text +  nombre_evaluations_environ_5cm.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a plus de 5cm et moins de 10cm pres;";
		text = text +  nombre_evaluations_sup_5cm_inf_10cm.text + ";\n";

		text = text + "Nombre d'evaluations bonnes a plus de 10cm pres;";
		text = text +  nombre_evaluations_sup_10cm.text + ";\n";

		text = text + "Temps moyen pour evaluer la cible;";
		text = text +  temps_evaluation_cible.text + ";\n";

		//text = text + "Détails des lancers;\n"; // Partie détails des lancers (selon comment Pierre stocke les lancers)

		text = text + "\n\n\n"; 
		Byte[] info = new UTF8Encoding(true).GetBytes(text);
		fs.Write(info, 0, text.Length);
		*/
		
		//Version 2
		string nomFichier; 
		string date = "Le " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " a " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
		string dateRepertoire = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
		String dateFichier = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "-" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

		if (!Directory.Exists(dateRepertoire)) {
			System.IO.Directory.CreateDirectory(dateRepertoire);
		}
		nomFichier = "resultats" + dateFichier + ".csv";
		string cheminNomFichier = dateRepertoire + "/resultats" + dateFichier + ".csv";
		FileStream fs = File.Open(cheminNomFichier, FileMode.Create);
		fichierCourant = nomFichier;
		nomDossier = dateRepertoire;
		
		text = "Date;" + date + ";\n\n";
		
		text = text + "Configuration;\n\n"; // Partie configuration
		
		text = text + "Nom du fichier de configuration;maConfig;\n"; //à remplacer par le bon nom de config
		
		text = text + "Taille de la cible (cm);";
		text = text +  taille_cible.text + ";\n";
		
		text = text + "Hauteur de la cible (cm);";
		text = text +  hauteur_cible.text + ";\n";
		
		text = text + "Distance separant la cible du lance-pierre (cm);";
		text = text +  distance_cible_lancepierre.text + ";\n";
		
		text = text + "Gravite;";
		text = text +  gravite.text + ";\n";
		
		text = text + "Rigidite du lance-pierre;";
		text = text +  rigidite_lancepierre.text + ";\n";
		
		text = text + "Nombre de lancers;";
		text = text +  nb_lancers.text + ";\n";
		
		text = text + "Taille des projectiles (cm);";
		text = text +  taille_projectile.text + ";\n";
		
		text = text + "Afficher le score;";
		
		if (afficher_le_score.enabled == true) {
			text = text + "Oui;\n";
		} else {
			text = text + "Non;\n";
		}
		
		text = text + "Nombre de points gagnes par cible (points);";
		text = text +  nb_points_gagnes_par_cible.text + ";\n";
		
		text = text + "Delai avant de pouvoir lancer le projectile (secondes);";
		text = text +  delai_lancer_projectile.text + ";\n";
		
		text = text + "Delai avant de pouvoir evaluer la taille de la cible (secondes);";
		text = text +  delai_evaluation_cible.text + ";\n\n";
		
		text = text + "Recapitulatif;\n\n"; // Partie récapitulatif
		
		text = text + "Score final (points);";
		text = text +  score_final.text + ";\n";
		
		text = text + "Nombre de cibles touchees;";
		text = text +  nombre_cibles_touchees.text + ";\n";
		
		text = text + "Nombre de cibles manquees;";
		text = text +  nombre_cibles_manquees.text + ";\n\n";
		
		text = text + "Details des lancers;\n\n";
		
		int count = 0;
		foreach (bool tir in GameController.Jeu._Un_tir) {
			count++;
			text = text + "Tir numero " + count + ";";
			if (tir == true) 
				text = text + "Touche !;\n";
			else 
				text = text + "Manque !;\n";
		}
		Byte[] info = new UTF8Encoding(true).GetBytes(text);
		fs.Write(info, 0, text.Length);
		
		fs.Close();
	}

	public void writeXLS() {

	}

	public void onClickOpenOneCSV() {
		Directory.SetCurrentDirectory(nomDossier);

		Process.Start (fichierCourant);

		Directory.SetCurrentDirectory("..");
	}
	
	public void onClickOpenDayCSVs() {
		Directory.SetCurrentDirectory(nomDossier);

		String[] fichiers = Directory.GetFiles (".");

		foreach (String fichier in fichiers) {
			Process.Start (fichier);
		}

		Directory.SetCurrentDirectory("..");
	}

	public void onClickOpenXLS() {

	}

	public void onClickOpenAllXLS() {

	}
}
