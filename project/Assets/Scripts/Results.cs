﻿using UnityEngine;
using UnityEngine.UI;
using System.Xml.XPath;
using System.Text.RegularExpressions;


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
	public Text gravite;
	public Text rigidite_lancepierre;
	public Text nb_lancers;
	public Text nb_series;
	public Text nb_positions;
	public Text nb_tailles_cibles;
	public Text nb_tailles_projectiles;
	public Toggle afficher_le_score;
	public Text nb_points_gagnes_par_cible;
	public Text delai_lancer_projectile;
	public Text delai_evaluation_cible;
	
	//Récapitulatif
	public Text score_final;
	public Text nombre_cibles_touchees;
	public Text nombre_cibles_manquees;
	/*public Text nombre_evaluations_moins_1cm;
	public Text nombre_evaluations_environ_2cm;
	public Text nombre_evaluations_environ_3cm;
	public Text nombre_evaluations_environ_4cm;
	public Text nombre_evaluations_environ_5cm;
	public Text nombre_evaluations_sup_5cm_inf_10cm;
	public Text nombre_evaluations_sup_10cm;
	public Text temps_evaluation_cible;*/
	
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
		//Assignation des valeurs définies dans le fichier de config choisi initialement;
		gravite.text = Convert.ToString(GameController.Jeu.Config.Gravite);
		rigidite_lancepierre.text = Convert.ToString(GameController.Jeu.Config.Rigidite_lancepierre);
		nb_lancers.text = Convert.ToString(GameController.Jeu.Config.Nb_lancers);
		nb_series.text = Convert.ToString(GameController.Jeu.Config.NB_series);
		nb_positions.text = Convert.ToString(GameController.Jeu.Config.Positions_Cibles.Count);
		nb_tailles_cibles.text = Convert.ToString(GameController.Jeu.Config.Tailles_Cibles.Count);
		nb_tailles_projectiles.text = Convert.ToString(GameController.Jeu.Config.Projectiles.Count);
		afficher_le_score.isOn = GameController.Jeu.Config.Afficher_le_score;
		nb_points_gagnes_par_cible.text = Convert.ToString(GameController.Jeu.Config.Nb_points_gagnes_par_cible);
		delai_lancer_projectile.text = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		delai_evaluation_cible.text = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
		
		//Assignation des valeurs par traitement des résultats obtenus durant la partie
		score_final.text = Convert.ToString(GameController.Jeu.Score); 
		//Ici on comptera le nombre de valeurs correspondantes
		/*nombre_evaluations_moins_1cm.text = "2"; //- 1cm
		nombre_evaluations_environ_2cm.text = "5"; //entre 1 et 2,4cm
		nombre_evaluations_environ_3cm.text = "10"; //entre 2,5 et 3,4cm
		nombre_evaluations_environ_4cm.text = "3"; //entre 3,5 et 4,4cm
		nombre_evaluations_environ_5cm.text = "0"; //entre 4,5 et 4,9cm
		nombre_evaluations_sup_5cm_inf_10cm.text = "0"; //entre 5 et 9,9cm
		nombre_evaluations_sup_10cm.text = "0"; //+ 10cm
		temps_evaluation_cible.text = "2"; //Ici on fera une moyenne des temps*/

		writeXML ();
	}
	
	public void writeXML() {
		string nomFichier; 
		string date = "Le " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " a " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
		//Ici on aura le numéro de la session
		nomFichier = "Session_" + "1" + ".xml";
		fichierCourant = nomFichier;
		
		if (!File.Exists (nomFichier)) {
			FileStream fs = File.Open (nomFichier, FileMode.Create);
			
			string text;
			
			text = "<?xml version=\"1.0\"?>" +
				"<?mso-application progid=\"Excel.Sheet\"?>" +
					"<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" " + 
					"xmlns:o=\"urn:schemas-microsoft-com:office:office\" " +
					"xmlns:x=\"urn:schemas-microsoft-com:office:excel\" " +
					"xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" " +
					"xmlns:html=\"http://www.w3.org/TR/REC-html40\">" +
					"<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">" +
					"</ExcelWorkbook>" +
					"<Styles>" +
					"<Style ss:ID=\"Default\" ss:Name=\"Normal\">" +
					"<Alignment ss:Vertical=\"Bottom\"/>" +
					"<Borders/>" +
					"<Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"#000000\"/>" + 
					"<Interior/>" +
					"<NumberFormat/>" +
					"<Protection/>" +
					"</Style>" +
					"</Styles>";
			
			text += "<Worksheet ss:Name=\"Configuration\">" + 
				"<Table>" +
					"<Column ss:Width=\"150\"/><Column ss:Width=\"120\"/>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Configuration" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"vraiNomDeConfig" + // à changer par le bon nom de config
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					/*"<Cell><Data ss:Type=\"String\">" +
					"Taille de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					taille_cible.text + 
					"</Data></Cell>" +*/
					"</Row>" +
					"<Row>" +
					/*"<Cell><Data ss:Type=\"String\">" +
					"Hauteur de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					hauteur_cible.text + 
					"</Data></Cell>" +*/
					"</Row>" +
					"<Row>" +
					/*"<Cell><Data ss:Type=\"String\">" +
					"Distance cible/LP" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					distance_cible_lancepierre.text + 
					"</Data></Cell>" +*/
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Gravite" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					gravite.text + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Rigidite du LP" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					rigidite_lancepierre.text + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de lancers" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_lancers.text + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					/*"<Cell><Data ss:Type=\"String\">" +
					"Taille du projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					taille_projectile.text + 
					"</Data></Cell>" +*/
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Affichage du score" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">";
			if (afficher_le_score.enabled == true) {
				text += "Oui";
			} else {
				text += "Non";
			}
			text = text + "</Data></Cell>" +
				"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de points par cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_points_gagnes_par_cible.text + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai pour lancer le projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_lancer_projectile.text + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai pour evaluer la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_evaluation_cible.text + 
					"</Data></Cell>" +
					"</Row>" +
					"</Table>" +
					"</Worksheet>";
			
			//Ici on aura N-Passations
			int dernierLancer = -3;
			int nb_lancers_int = Convert.ToInt32(nb_lancers.text);
			int premierLancer = dernierLancer - (nb_lancers_int - 1); 
			//int premierLancer = dernierLancer - (2 - 1);
			text += "<Worksheet ss:Name=\"Passation1\">" + 
				"<Table>" +
					"<Column ss:Width=\"150\"/><Column ss:Width=\"105\"/><Column ss:Width=\"90\"/><Column ss:Width=\"90\"/>" +
					"<Column ss:Width=\"90\"/><Column ss:Width=\"75\"/><Column ss:Width=\"120\"/><Column ss:Width=\"130\"/>" +
					"<Column ss:Width=\"60\"/><Column ss:Width=\"80\"/><Column ss:Width=\"90\"/><Column ss:Width=\"55\"/>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Date" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					date + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Numero de participant" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le vrai numéro de participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Age" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"22" + // Mettre le vrai age du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Sexe" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Homme" + // Mettre le vrai sexe du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Main dominante" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Droite" + // Mettre la vraie main dominante du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Experience dans les jeux videos" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Oui" + // Mettre la vraie expérience du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"</Row>" +
					"<Row>" +
					"<Cell></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Taille de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Position de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Taille du projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Poids du projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Points obtenus" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour evaluer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai evaluation" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Resultat du lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Evaluation" +
					"</Data></Cell>" +
					"</Row>" +
					// Faire pour le bon nombre de lancers
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Lancer 1" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la vraie taille de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la vraie position de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la vraie taille du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le vrai poids du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon nombre de points
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps imparti pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps imparti pour evaluation 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps pour evaluer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon résultat du lancer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la bonne evaluation
					"</Data></Cell>" +
					"</Row>" +
					// Faire pour le bon nombre de lancers
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Lancer 2" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la vraie taille de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la vraie position de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la vraie taille du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le vrai poids du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon nombre de points
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps imparti pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps imparti pour evaluation 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps pour evaluer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon résultat du lancer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la bonne evaluation
					"</Data></Cell>" +
					"</Row>" +
					"<Row></Row>" +
					"<Row>" +
					"<Cell></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"</Row>" +
					"<Row><Cell></Cell>" +
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"</Row>" +
					"</Table>" +
					"</Worksheet>";
			
			//Somme à garder pour les stats !!!
			//"<Cell ss:Formula=\"=SUM(RC[-1],R[3]C[-1])\"><Data ss:Type=\"Number\"></Data> </Cell>" +
			text += "<Worksheet ss:Name=\"Statistiques\">" + 
				"<Table ss:ExpandedColumnCount=\"2\" ss:ExpandedRowCount=\"1\" x:FullColumns=\"1\" " +
					"x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">" +
					"<Row>" +
					"</Row>" +
					"</Table>" +
					"</Worksheet>" +
					"</Workbook>";
			
			Byte[] info = new UTF8Encoding (true).GetBytes (text);
			fs.Write (info, 0, text.Length);
			
			fs.Close ();
		} else {
			String contenu = "";
			
			contenu = System.IO.File.ReadAllText(nomFichier);
			
			int numPassation = this.getNumPassation (contenu);
			
			string[] lines = Regex.Split(contenu, "</Worksheet>");
			
			String textToWrite = "";
			
			for (int i = 0; i < (lines.Length - 2); i++) {
				textToWrite += lines[i] + "</Worksheet>";
			}
			
			lines[(lines.Length - 2)] += "</Worksheet>";
			
			//Ici on aura N-Participations
			int dernierLancer = -3;
			//int premierLancer = dernierLancer - (nb_lancers.text - 1); A Decommenter !!!
			int premierLancer = dernierLancer - (2 - 1);
			String identitePassation = "Passation" + numPassation;
			textToWrite += "<Worksheet ss:Name=\"";
			textToWrite += identitePassation;
			textToWrite += "\">" + 
				"<Table>" +
					"<Column ss:Width=\"150\"/><Column ss:Width=\"105\"/><Column ss:Width=\"90\"/><Column ss:Width=\"90\"/>" +
					"<Column ss:Width=\"90\"/><Column ss:Width=\"75\"/><Column ss:Width=\"120\"/><Column ss:Width=\"130\"/>" +
					"<Column ss:Width=\"60\"/><Column ss:Width=\"80\"/><Column ss:Width=\"90\"/><Column ss:Width=\"55\"/>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Date" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					date + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Numero de participant" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le vrai numéro de participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Age" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"22" + // Mettre le vrai age du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Sexe" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Homme" + // Mettre le vrai sexe du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Main dominante" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Droite" + // Mettre le vrai sexe du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Experience dans les jeux videos" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Oui" + // Mettre le vrai sexe du participant
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"</Row>" +
					"<Row>" +
					"<Cell></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Taille de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Position de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Taille du projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Poids du projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Points obtenus" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour evaluer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai evaluation" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Resultat du lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Evaluation" +
					"</Data></Cell>" +
					"</Row>" +
					// Faire pour le bon nombre de lancers
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Lancer 1" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la vraie taille de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la vraie position de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la vraie taille du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le vrai poids du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon nombre de points
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps imparti pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps imparti pour evaluation 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon temps pour evaluer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre le bon résultat du lancer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"1" + // Mettre la bonne evaluation
					"</Data></Cell>" +
					"</Row>" +
					// Faire pour le bon nombre de lancers
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Lancer 2" + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la vraie taille de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la vraie position de la cible
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la vraie taille du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le vrai poids du projectile
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon nombre de points
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps imparti pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps imparti pour evaluation 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps pour lancer 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon temps pour evaluer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre le bon résultat du lancer
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					"3" + // Mettre la bonne evaluation
					"</Data></Cell>" +
					"</Row>" +
					"<Row></Row>" +
					"<Row>" +
					"<Cell></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Moyenne" +
					"</Data></Cell>" +
					"</Row>" +
					"<Row><Cell></Cell>" +
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"<Cell ss:Formula=\"=AVERAGE(R[" + premierLancer + "]C,R[" + dernierLancer + "]C)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
					"</Row>" +
					"</Table>" +
					"</Worksheet>";
			
			textToWrite += lines[(lines.Length - 2)] + lines[(lines.Length - 1)];
			
			//UnityEngine.Debug.Log(textToWrite);
			
			FileStream fs = File.Open (nomFichier, FileMode.Create);
			
			Byte[] info = new UTF8Encoding (true).GetBytes (textToWrite);
			fs.Write (info, 0, textToWrite.Length);
			
			fs.Close ();
		}
	}
	
	public void onClickOpenXML() {
		Process.Start (fichierCourant);
	}
	
	public void onClickOpenAllXML() {
		Process.Start (fichierCourant); // à modifier par la suite
	}
	
	public void onMenu() {
		Application.LoadLevel ("mainMenu");
	}
	
	public int getNumPassation(String contenu) {
		int nbPassations = 1;
		
		Boolean exists = true;
		String basis = "Passation";
		
		while (exists) {
			basis += nbPassations;
			if (contenu.Contains(basis)) {
				nbPassations++;
				basis = "Passation";
			}
			else
				exists = false;
		}
		
		return nbPassations;
	}
}
