using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class FinDeTest : MonoBehaviour {

	//Enlever ce qui ne sert à rien pour l'XML !
	//Configuration
	private string nom_configuration;
	private string gravite;
	private string rigidite_lancepierre;
	private string nb_lancers;
	private string nb_series;
	private string nb_positions;
	private string nb_tailles_cibles;
	private string nb_tailles_projectiles;
	private bool afficher_le_score;
	private string nb_points_gagnes_par_cible;
	private string nb_points_perdus_par_cible;
	private string delai_lancer_projectile;
	private string delai_evaluation_cible;
	private string delai_validation_mesure;
	private string marge_stabilisation_leap;
	private string condition_test;

	string fichierCourant;

	// Use this for initialization
	void Start () {
		if (GameController.Jeu == null) 
			GameController.Jeu = new Jeu ();
		setFields ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onMenu() {
		Application.LoadLevel ("mainMenu");
	}

	public void onResultats() {
		Application.LoadLevel ("results");
	}

	/**
	 * Met à jour les champs du menu principal avec la configuration actuelle du jeu 
	 */
	public void setFields(){
		//Assignation des valeurs définies dans le fichier de config choisi initialement;
		nom_configuration = Convert.ToString (GameController.Jeu.Config.Name);
		afficher_le_score = false;
		gravite = Convert.ToString(GameController.Jeu.Config.Gravite);
		rigidite_lancepierre = Convert.ToString(GameController.Jeu.Config.Rigidite_lancepierre);
		nb_lancers = Convert.ToString(GameController.Jeu.Config.Nb_lancers);
		nb_series = Convert.ToString(GameController.Jeu.Config.NB_series);
		nb_positions = Convert.ToString(GameController.Jeu.Config.Positions_Cibles.Count);
		nb_tailles_cibles = Convert.ToString(GameController.Jeu.Config.Tailles_Cibles.Count);
		nb_tailles_projectiles = Convert.ToString(GameController.Jeu.Config.Projectiles.Count);
		afficher_le_score = GameController.Jeu.Config.Afficher_le_score;
		nb_points_gagnes_par_cible = Convert.ToString(GameController.Jeu.Config.Nb_points_gagnes_par_cible);
		//nb_points_perdus_par_cible = Convert.ToString(GameController.Jeu.Config.Nb_points_perdus_par_cible);
		delai_lancer_projectile = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		delai_evaluation_cible = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
		delai_validation_mesure = Convert.ToString (GameController.Jeu.Config.Delai_validation_mesure_cible);
		marge_stabilisation_leap = Convert.ToString (GameController.Jeu.Config.Marge_stabilisation_validation_cible);
		//condition_test = Convert.ToString (GameController.Jeu.Config.condition_test);
		
		writeXML ();
	}
	
	public void writeXML() {
		string date = "Le " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " a " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
		fichierCourant = nom_configuration + ".xml";
		int nb_lancers_int = Convert.ToInt32(nb_lancers);
		int nbReussi = 0;
		int nbManque = 0;

		for (int i = 0; i < GameController.Jeu.Reussiste_Tirs.Count; i++) {
			if (GameController.Jeu.Reussiste_Tirs[i] == true) 
				nbReussi++;
			else 
				nbManque++;
		}
		
		if (!File.Exists (fichierCourant)) {
			FileStream fs = File.Open (fichierCourant, FileMode.Create);
			
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
					"<Column ss:Width=\"210\"/><Column ss:Width=\"120\"/>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Configuration" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					nom_configuration + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de lancers" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_lancers + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de tailles de cibles" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_tailles_cibles + 
					"</Data></Cell>" +
					"<Cell></Cell>";
			for (int i = 0; i < Convert.ToInt32(nb_tailles_cibles); i++) {
				text += "<Cell><Data ss:Type=\"Number\">" +
					+ GameController.Jeu.Config.Tailles_Cibles[i] +
					"</Data></Cell>";
			}

				text += "</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de tailles de projectiles et poids" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_tailles_projectiles + 
					"</Data></Cell>" +
					"<Cell></Cell>";
			for (int i = 0; i < Convert.ToInt32(nb_tailles_projectiles); i++) {
				text += "<Cell><Data ss:Type=\"String\">" +
					+ GameController.Jeu.Config.Projectiles[i].Taille +
						" - " + GameController.Jeu.Config.Projectiles[i].Poids + "</Data></Cell>";
			}
				text += "</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de positions de cibles" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_positions + 
					"</Data></Cell>" +
					"<Cell></Cell>";
			for (int i = 0; i < Convert.ToInt32(nb_positions); i++) {
				text += "<Cell><Data ss:Type=\"String\">[" +
					+ GameController.Jeu.Config.Positions_Cibles[i].DistanceX + ", " + 
						GameController.Jeu.Config.Positions_Cibles[i].DistanceY + "]</Data></Cell>";
			}
			text += "</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de series de lancers" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_series + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Gravite" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					gravite + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Rigidite du Lance-Pierre" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					rigidite_lancepierre + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Affichage du score" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">";
				if (afficher_le_score == true) {
					text += "Oui";
				} else {
					text += "Non";
				}
				text = text + "</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de points gagnes par cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					nb_points_gagnes_par_cible + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Nombre de points perdus par cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					0 + // à changer par la bonne valeur (nb_points_perdus_par_cible)
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai pour lancer le projectile" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_lancer_projectile + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour evaluer la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_evaluation_cible + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour valider la mesure" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_validation_mesure + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Marge de stabilisation du leap motion" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					marge_stabilisation_leap + 
					"</Data></Cell>" +
					"</Row>" +
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Condition de test" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Pour l'instant rien" + // à changer avec la bonne valeur (condition_test)
					"</Data></Cell>" +
					"</Row>" +
					"</Table>" +
					"</Worksheet>";
			
			//Ici on aura N-Passations
			int dernierLancer = -3;
			int premierLancer = dernierLancer - (nb_lancers_int - 1); 
			//int premierLancer = dernierLancer - (2 - 1);
			text += "<Worksheet ss:Name=\"Passation1\">" + 
				"<Table>" +
					"<Column ss:Width=\"170\"/><Column ss:Width=\"130\"/><Column ss:Width=\"110\"/><Column ss:Width=\"110\"/>" +
					"<Column ss:Width=\"110\"/><Column ss:Width=\"110\"/><Column ss:Width=\"95\"/><Column ss:Width=\"140\"/><Column ss:Width=\"150\"/>" +
					"<Column ss:Width=\"80\"/><Column ss:Width=\"100\"/><Column ss:Width=\"110\"/><Column ss:Width=\"75\"/>" +
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
					"Position de la cible X" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Position de la cible Y" +
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
					"Delai imparti pour lancer (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour evaluer (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai lancer (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai evaluation (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Resultat du lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Evaluation" +
					"</Data></Cell>" +
					"</Row>";
			for (int i = 0; i < nb_lancers_int; i++) {
				text += "<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Lancer " + (i + 1) + 
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					GameController.Jeu.Tirs_Realises[i].Taille_Cible +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					GameController.Jeu.Tirs_Realises[i].Position_Cible.DistanceX +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					GameController.Jeu.Tirs_Realises[i].Position_Cible.DistanceY +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					GameController.Jeu.Tirs_Realises[i].Projectile.Taille +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					GameController.Jeu.Tirs_Realises[i].Projectile.Poids +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">";
				if (GameController.Jeu.Reussiste_Tirs[i] == true) 
					text += GameController.Jeu.Config.Nb_points_gagnes_par_cible;
				else
					text += "0";
				text += "</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Config.Delai_lancer_projectile +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Config.Delai_evaluation_cible +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						Math.Round(GameController.Jeu.Temps_Mis_Pour_Tirer[i], 2) +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						"1" + // Mettre le bon temps pour evaluer (Math round 2 comme ci-dessus)
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"String\">";
				if (GameController.Jeu.Reussiste_Tirs[i] == true) 
					text += "Touche";
				else 
					text += "Manque";
				text += "</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						"1" + // Mettre la bonne evaluation
						"</Data></Cell>" +
						"</Row>";
			}
			text += "<Row></Row>" +
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
				"Taux de reussite" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Moyenne" +
				"</Data></Cell>" +
				"</Row>" +
				"<Row><Cell></Cell>" +
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
				"<Cell><Data ss:Type=\"Number\">";
				if (nbManque == 0) {
					text += nbReussi;
				}
				else {
					double nR = Convert.ToDouble(nbReussi);
					double nL = Convert.ToDouble(nb_lancers_int);
					text += Math.Round(nR/nL, 2);
				}
			text += "</Data></Cell>" +
					"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
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
			
			contenu = System.IO.File.ReadAllText(fichierCourant);
			
			int numPassation = this.getNumPassation (contenu);
			
			string[] lines = Regex.Split(contenu, "</Worksheet>");
			
			String textToWrite = "";
			
			for (int i = 0; i < (lines.Length - 2); i++) {
				textToWrite += lines[i] + "</Worksheet>";
			}
			
			lines[(lines.Length - 2)] += "</Worksheet>";
			
			//Ici on aura N-Participations
			int dernierLancer = -3;
			int premierLancer = dernierLancer - (nb_lancers_int - 1); 
			//int premierLancer = dernierLancer - (2 - 1);
			String identitePassation = "Passation" + numPassation;
			textToWrite += "<Worksheet ss:Name=\"";
			textToWrite += identitePassation;
			textToWrite += "\">" + 
				"<Table>" +
					"<Column ss:Width=\"170\"/><Column ss:Width=\"130\"/><Column ss:Width=\"110\"/><Column ss:Width=\"110\"/>" +
					"<Column ss:Width=\"110\"/><Column ss:Width=\"110\"/><Column ss:Width=\"95\"/><Column ss:Width=\"140\"/><Column ss:Width=\"150\"/>" +
					"<Column ss:Width=\"80\"/><Column ss:Width=\"100\"/><Column ss:Width=\"110\"/><Column ss:Width=\"75\"/>" +
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
					"Position de la cible X" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Position de la cible Y" +
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
					"Delai imparti pour lancer (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai imparti pour evaluer (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai lancer (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai evaluation (s)" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Resultat du lancer" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Evaluation" +
					"</Data></Cell>" +
					"</Row>";
			for (int i = 0; i < nb_lancers_int; i++) {
				textToWrite += "<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
						"Lancer " + (i + 1) + 
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Tirs_Realises[i].Taille_Cible +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Tirs_Realises[i].Position_Cible.DistanceX +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Tirs_Realises[i].Position_Cible.DistanceY +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Tirs_Realises[i].Projectile.Taille +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Tirs_Realises[i].Projectile.Poids +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">";
				if (GameController.Jeu.Reussiste_Tirs[i] == true) 
					textToWrite += GameController.Jeu.Config.Nb_points_gagnes_par_cible;
				else
					textToWrite += "0";
				textToWrite += "</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Config.Delai_lancer_projectile +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						GameController.Jeu.Config.Delai_evaluation_cible +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						Math.Round(GameController.Jeu.Temps_Mis_Pour_Tirer[i], 2) +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"Number\">" +
						"1" + // Mettre le bon temps pour evaluer (Math round 2 comme ci-dessus)
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"String\">";
				if (GameController.Jeu.Reussiste_Tirs[i] == true) 
					textToWrite += "Touche";
				else 
					textToWrite += "Manque";
				textToWrite += "</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
						"1" + // Mettre la bonne evaluation
						"</Data></Cell>" +
						"</Row>";
			}
					textToWrite += "<Row></Row>" +
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
						"Taux de reussite" +
						"</Data></Cell>" +
						"<Cell><Data ss:Type=\"String\">" +
						"Moyenne" +
						"</Data></Cell>" +
						"</Row>" +
						"<Row><Cell></Cell>" +
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
						"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
						"<Cell><Data ss:Type=\"Number\">";
						if (nbManque == 0) {
							textToWrite += nbReussi;
						}
						else {
							double nR = Convert.ToDouble(nbReussi);
							double nL = Convert.ToDouble(nb_lancers_int);
							textToWrite += Math.Round(nR/nL, 2);
						}
							textToWrite += "</Data></Cell>" +
								"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
								"</Row>" +
								"</Table>" +
								"</Worksheet>";
			
			textToWrite += lines[(lines.Length - 2)] + lines[(lines.Length - 1)];
			
			//UnityEngine.Debug.Log(textToWrite);
			
			FileStream fs = File.Open (fichierCourant, FileMode.Create);
			
			Byte[] info = new UTF8Encoding (true).GetBytes (textToWrite);
			fs.Write (info, 0, textToWrite.Length);
			
			fs.Close ();
		}
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
