using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class FinDeTest : MonoBehaviour {
	
	// Informations à récupérer afin de les stocker dans le fichier XML
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
	private string numParticipant;
	private string ageParticipant;
	private string sexeParticipant;
	private string mainForteParticipant;
	private string experienceJeuxVideosParticipant;
	private bool prise_en_compte_score;
	private string hauteurLancePierre;
	private string positionXLP;
	private string positionYLP;
	private string delai_avant_disparition_cible;
	private string delai_avant_evaluation_cible;
	private bool afficher_barre_progression;
	private string couleurCible;
	private string hauteurBarreProgression;
	private string largeurBarreProgression;

	string fichierCourant; // Nom du fichier XML
	ArrayList storedPassations = new ArrayList (); // Numéros de passations existants (feuillets) => Pour prendre en compte les suppressions de feuillets

	// Use this for initialization
	void Start () {
		if (GameController.Jeu == null) 
			GameController.Jeu = new Jeu ();

		setFields ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Bouton sujet suivant => Chargement de la scène "menuSecondaire" et fin du preTest + réinitialisation des champs
	public void onSujetSuivant() {
		resetAll ();
		GameController.Jeu.isPretest = false;
		Application.LoadLevel ("menuSecondaire");
	}

	// Réinitialisation des champs
	public void resetAll() {
		GameController.Jeu.newGame ();
	}

	// Bouton menu => Chargement de la scène "menu" et fin du preTest 
	public void onMenu() {
		GameController.Jeu.isPretest = false;
		Application.LoadLevel ("menu");
	}

	// Bouton menu => Chargement de la scène "results"
	public void onResultats() {
		Application.LoadLevel ("results");
	}

	/**
	 * Met à jour les champs du menu principal avec la configuration actuelle du jeu 
	 */
	public void setFields(){
		// Assignation des valeurs définies dans le fichier de config choisi initialement
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
		nb_points_perdus_par_cible = Convert.ToString(GameController.Jeu.Config.Nb_points_perdus_par_cible_manque);
		delai_lancer_projectile = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		delai_evaluation_cible = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
		delai_validation_mesure = Convert.ToString (GameController.Jeu.Config.Delai_validation_mesure_cible);
		marge_stabilisation_leap = Convert.ToString (GameController.Jeu.Config.Marge_stabilisation_validation_cible);
		if (GameController.Jeu.Config.Condition_De_Memoire)
			condition_test = "Memoire";
		else if (GameController.Jeu.Config.Condition_De_Controle)
			condition_test = "Controle";
		else if (GameController.Jeu.Config.Condition_De_Perception)
			condition_test = "Perception";
		numParticipant = Convert.ToString (GameController.Jeu.Participant.Numero);
		ageParticipant = Convert.ToString (GameController.Jeu.Participant.Age);
		sexeParticipant = Convert.ToString (GameController.Jeu.Participant.Sexe);
		mainForteParticipant = Convert.ToString (GameController.Jeu.Participant.MainDominante);
		experienceJeuxVideosParticipant = Convert.ToString (GameController.Jeu.Participant.PratiqueJeuxVideo);
		prise_en_compte_score = GameController.Jeu.Config.Prise_en_compte_du_score;
		hauteurLancePierre = Convert.ToString (GameController.Jeu.Config.Taille_Hauteur_Catapulte);
		positionXLP = Convert.ToString (GameController.Jeu.Config.Distance_X_Catapulte);
		positionYLP = Convert.ToString (GameController.Jeu.Config.Distance_Y_Catapulte);
		delai_avant_disparition_cible = Convert.ToString (GameController.Jeu.Config.Delai_avant_disparition_cible);
		delai_avant_evaluation_cible = Convert.ToString (GameController.Jeu.Config.Delai_avant_evaluation_cible);
		afficher_barre_progression = GameController.Jeu.Config.Affichage_barre_progression;
		couleurCible = Convert.ToString (GameController.Jeu.Config.Couleur_cible);
     	hauteurBarreProgression = Convert.ToString (GameController.Jeu.Config.Hauteur_barre_progression);
        largeurBarreProgression = Convert.ToString (GameController.Jeu.Config.Largeur_barre_progression);

		if (numParticipant != "0")
			writeXML ();
	}

	// En-tete XML
	public String writeHeader() {
		String header = "<?xml version=\"1.0\"?>" +
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

		return header;
	}

	// Ecriture dans le fichier XML de la configuration choisie
	public String writeConfig() {
		String Config = "<Worksheet ss:Name=\"Configuration\">" + 
			"<Table>" +
				"<Column ss:Width=\"210\"/><Column ss:Width=\"120\"/>" +
				// Nom de la configuration
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Configuration" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"String\">" +
				nom_configuration + 
				"</Data></Cell>" +
				"</Row>" +
				// Nombre de lancers choisis
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Nombre de lancers" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				nb_lancers + 
				"</Data></Cell>" +
				"</Row>" +
				// Nombre de tailles différentes de cibles
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Nombre de tailles de cibles" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				nb_tailles_cibles + 
				"</Data></Cell>" +
				"<Cell></Cell>";
		for (int i = 0; i < Convert.ToInt32(nb_tailles_cibles); i++) {
			Config += "<Cell><Data ss:Type=\"Number\">" +
				+ GameController.Jeu.Config.Tailles_Cibles[i] +
					"</Data></Cell>";
		}
		
		Config += "</Row>" +
			// Nombre de projectiles et poids de ceux-ci
			"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Nombre de tailles de projectiles et poids" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				nb_tailles_projectiles + 
				"</Data></Cell>" +
				"<Cell></Cell>";
		for (int i = 0; i < Convert.ToInt32(nb_tailles_projectiles); i++) {
			Config += "<Cell><Data ss:Type=\"String\">" +
				+ GameController.Jeu.Config.Projectiles[i].Taille +
					" - " + GameController.Jeu.Config.Projectiles[i].Poids + "</Data></Cell>";
		}
		Config += "</Row>" +
			// Nombre de positions de cibles
			"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Nombre de positions de cibles" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				nb_positions + 
				"</Data></Cell>" +
				"<Cell></Cell>";
		for (int i = 0; i < Convert.ToInt32(nb_positions); i++) {
			Config += "<Cell><Data ss:Type=\"String\">[" +
				+ GameController.Jeu.Config.Positions_Cibles[i].DistanceX + ", " + 
					GameController.Jeu.Config.Positions_Cibles[i].DistanceY + "]</Data></Cell>";
		}
		Config += "</Row>" +
			// Nombre de séries de lancers (nombre de fois qu'on rejoue les différentes positions, poids, ... de cibles et de projectiles)
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Nombre de series de lancers" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			nb_series + 
			"</Data></Cell>" +
			"</Row>" +
			// Valeur de gravité du projectile (seul élément solide du jeu)
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Gravite" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			gravite + 
			"</Data></Cell>" +
			"</Row>" +
			// Rigidité du lance-pierre
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Rigidite du Lance-Pierre" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			rigidite_lancepierre + 
			"</Data></Cell>" +
			"</Row>" +
			// Hauteur du lance-pierre
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Hauteur du Lance-Pierre" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			hauteurLancePierre + 
			"</Data></Cell>" +
			"</Row>" +
			// Distance horizontale du lance-pierre par rapport au centre de la scène de jeu
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Distance X du Lance-Pierre" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			positionXLP + 
			"</Data></Cell>" +
			"</Row>" +
			// Distance verticale du lance-pierre par rapport au centre de la scène de jeu
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Distance Y du Lance-Pierre" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			positionYLP + 
				"</Data></Cell>" +
				"</Row>" +
				// Affichage ou non du score
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Affichage du score" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"String\">";
		if (afficher_le_score == true) {
			Config += "Oui";
		} else {
			Config += "Non";
		}
		Config += "</Data></Cell>" +
			"</Row>" +
				// Prise en compte du score ou non
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Prise en compte du score" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"String\">";
		if (prise_en_compte_score == true) {
			Config += "Oui";
		} else {
			Config += "Non";
		}
		Config += "</Data></Cell>" +
			"</Row>" +
			// Avec ou sans effets de destruction
			"<Row><Cell><Data ss:Type=\"String\">" +
			"Affichage des effets de destruction" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">";
		if (GameController.Jeu.Config.Afficher_effet_destruction_cible == true) {
			Config += "Oui";
		} else {
			Config += "Non";
		}
		Config += "</Data></Cell>" +
			"</Row>" +
				// Points gagnés en touchant la cible
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Nombre de points gagnes par cible" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				nb_points_gagnes_par_cible + 
				"</Data></Cell>" +
				"</Row>" +
				// Points perdus en manquant la cible
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Nombre de points perdus par cible" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				nb_points_perdus_par_cible +
				"</Data></Cell>" +
				"</Row>" +
				// Délai maximum pour lancer le projectile à partir de l'apparition de la scène de jeu
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Delai pour lancer le projectile" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				delai_lancer_projectile + 
				"</Data></Cell>" +
				"</Row>" +
				// Délai maximum pour évaluer la cible à partir de l'apparition de la barre de progression d'évaluation
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Delai imparti pour evaluer la cible" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				delai_evaluation_cible + 
				"</Data></Cell>" +
				"</Row>" +
				// Délai de maintien de l'écart des doigts avant de valider la mesure
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Delai imparti pour valider la mesure" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				delai_validation_mesure + 
				"</Data></Cell>" +
				"</Row>" +
				// Marge d'écart entre les doigts permettant de valider la mesure
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Marge de stabilisation du leap motion" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				marge_stabilisation_leap + 
				"</Data></Cell>" +
				"</Row>" +
				// Dans quelle condition de test se passe la passation ? (Controle, Mémoire, Perception)
				"<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Condition de test" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"String\">" +
				condition_test +
				"</Data></Cell>" +
				"</Row>";
			if (condition_test == "Memoire") {
				// Délai avant que la cible ne disparaisse de l'écran avant que l'utilisteur puisse évaluer la cible
				Config += "<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai avant disparition de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_avant_disparition_cible +
					"</Data></Cell>" +
					"</Row>" +
					// Délai avant de pouvoir évaluer la cible au leap motion
					"<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					"Delai avant evaluation de la cible" +
					"</Data></Cell>" +
					"<Cell><Data ss:Type=\"Number\">" +
					delai_avant_evaluation_cible +
					"</Data></Cell>" +
					"</Row>";
			}
			
		String couleurBarreProgression = Convert.ToString(GameController.Jeu.Config.Couleur_barre.r) + ", " + Convert.ToString(GameController.Jeu.Config.Couleur_barre.g) + ", " + Convert.ToString(GameController.Jeu.Config.Couleur_barre.b) + ", " + Convert.ToString(GameController.Jeu.Config.Couleur_barre.a);
		// Affichage ou non de la barre de progression
		Config += "<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Affichage barre de progression" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">";
		if (afficher_barre_progression == true) {
			// Hauteur de la barre de progression
			Config += "Oui</Data></Cell></Row><Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Hauteur de la barre de progression" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				hauteurBarreProgression + 
				// Largeur de la barre de progression
				"</Data></Cell></Row><Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				"Largeur de la barre de progression" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">" +
				largeurBarreProgression + 
				"</Data></Cell></Row><Row>" + 
				// Couleur de la barre de progression
				"<Cell><Data ss:Type=\"String\">" +
				"Couleur de la barre de progression" +
				"</Data></Cell>" +
				"<Cell><Data ss:Type=\"String\">" +
				couleurBarreProgression +
				"</Data></Cell>";
		} else {
			Config += "Non</Data></Cell>";
		}
		Config += "</Row>";
		// Couleur de la cible
		Config +="<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Couleur de la cible" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">" +
			couleurCible +
			"</Data></Cell>" +
			"</Row></Table>" +
			"</Worksheet>";

		return Config;
	}

	// Ecriture dans le fichier XML de toutes les informations sur une passation (une série de lancers)
	public String writePassation(int nbPassation) {
		// Date courante avec l'heure exacte de passation
		string date = "Le " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " a " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
		// Nombre de lancers sous forme d'un entier
		int nb_lancers_int = Convert.ToInt32 (nb_lancers);
		// Nombre de lancers réussis
		int nbReussi = 0;
		// Nombre de lancers manqués
		int nbManque = 0;
		// N'y a t'il aucun lancer possédant une évaluation au leap motion ?
		bool aucunLancerAvecEval = true;

		// Stockage du nombre de lancers réussis et manqués et vérification quant à l'existence d'un lancer avec evaluation au leap motion
		for (int i = 0; i < GameController.Jeu.Reussiste_Tirs.Count; i++) {
			if (GameController.Jeu.Reussiste_Tirs [i] == true) 
				nbReussi++;
			else 
				nbManque++;

			if (aucunLancerAvecEval == true) {
				if (GameController.Jeu.Mesures_Taille_Cible[i] != 99f) {
					aucunLancerAvecEval = false;
				}
			}
		}

		// Le dernier lancer se situe 3 lignes au dessus de la moyenne dans le tableur Excel
		int dernierLancer = -3;
		// Le premier lancer se situe (nb_lancers_int - 1) lignes au dessus du dernier lancer dans le tableur Excel
		int premierLancer = dernierLancer - (nb_lancers_int - 1); 

		// Nom du feuillet et taille des colonnes (en largeur)
		String passation = "<Worksheet ss:Name=\"Passation" + nbPassation + "\">" + 
			"<Table>" +
			"<Column ss:Width=\"170\"/><Column ss:Width=\"130\"/><Column ss:Width=\"110\"/><Column ss:Width=\"110\"/>" +
				"<Column ss:Width=\"110\"/><Column ss:Width=\"110\"/>";

		// Largeur de la colonne "Points obtenus" (si on prend en compte le score)
		if (prise_en_compte_score == true) 
			passation += "<Column ss:Width=\"95\"/>";

		// Largeur des autres colonnes
		passation += "<Column ss:Width=\"140\"/><Column ss:Width=\"150\"/>" +
			"<Column ss:Width=\"80\"/><Column ss:Width=\"100\"/><Column ss:Width=\"110\"/><Column ss:Width=\"75\"/><Column ss:Width=\"180\"/><Column ss:Width=\"165\"/>" +
			// Date courante
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Date" + 
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">" +
			date + 
			"</Data></Cell>" +
			"</Row>" +
			// Numéro de participant récupéré dans la scène menuSecondaire
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Numero de participant" + 
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" + 
			numParticipant +
			"</Data></Cell>" +
			"</Row>" +
			// Age de celui-ci
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Age" + 
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"Number\">" +
			ageParticipant +
			"</Data></Cell>" +
			"</Row>" +
			// Sexe de celui-ci
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Sexe" + 
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">" + 
			sexeParticipant +
			"</Data></Cell>" +
			"</Row>" +
			// Main forte de celui-ci (droitier ou gaucher)
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Main dominante" + 
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">" +
			mainForteParticipant +
			"</Data></Cell>" +
			"</Row>" +
			// Expérience dans les jeux vidéos (Oui/Non)
			"<Row>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Experience dans les jeux videos" + 
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">" +
			experienceJeuxVideosParticipant +
			"</Data></Cell>" +
			"</Row>" +
			"<Row>" +
			"</Row>" +
			"<Row>" +
			"<Cell></Cell>" +
			// Colonne destinée à recueillir les tailles de cible
			"<Cell><Data ss:Type=\"String\">" +
			"Taille de la cible" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les positions horizontales des cibles
			"<Cell><Data ss:Type=\"String\">" +
			"Position de la cible X" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les positions verticales des cibles
			"<Cell><Data ss:Type=\"String\">" +
			"Position de la cible Y" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les tailles des projectiles
			"<Cell><Data ss:Type=\"String\">" +
			"Taille du projectile" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les poids des projectiles
			"<Cell><Data ss:Type=\"String\">" +
			"Poids du projectile" +
			"</Data></Cell>";

		// Colonne destinée à recueillir les points obtenus si le score est pris en compte
		if (prise_en_compte_score == true) {
			passation += "<Cell><Data ss:Type=\"String\">" +
				"Points obtenus" +
				"</Data></Cell>";
		}

		// Colonne destinée à recueillir les délais impartis pour lancer les projectiles
		passation += "<Cell><Data ss:Type=\"String\">" +
			"Delai imparti pour lancer (s)" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les délais impartis pour évaluer la cible au leap motion
			"<Cell><Data ss:Type=\"String\">" +
			"Delai imparti pour evaluer (s)" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les délais pour lancer les projectiles (le temps réellement mis par l'utilisateur)
			"<Cell><Data ss:Type=\"String\">" +
			"Delai lancer (s)" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les délais pour évaluer la cible (le temps réellement mis par l'utilisateur)
			"<Cell><Data ss:Type=\"String\">" +
			"Delai evaluation (s)" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les résultats du lancer (touché ou manqué)
			"<Cell><Data ss:Type=\"String\">" +
			"Resultat du lancer" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les évuations au leap motion
			"<Cell><Data ss:Type=\"String\">" +
			"Evaluation" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir les différences entre l'évaluation au leap motion et la taille réelle de la cible
			"<Cell><Data ss:Type=\"String\">" +
			"Difference (Evaluation - Taille reelle)" +
			"</Data></Cell>" +
			// Colonne destinée à recueillir la valeur absolue de la différence afin de pouvoir etre utilisée dans l'onglet statistiques
			"<Cell><Data ss:Type=\"String\">" +
			"Valeur absolue de la difference" +
			"</Data></Cell>" +
			"</Row>";

		// On va maintenant inscrire les valeurs des champs pour chaque lancer
		for (int i = 0; i < nb_lancers_int; i++) {
			passation += "<Row>" +
				// Le numéro de lancer
				"<Cell><Data ss:Type=\"String\">" +
				"Lancer " + (i + 1) + 
				"</Data></Cell>" +
				// La taille de la cible
				"<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Tirs_Realises [i].Taille_Cible +
				"</Data></Cell>" +
				// La distance horizontale de la cible
				"<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Tirs_Realises [i].Position_Cible.DistanceX +
				"</Data></Cell>" +
				// La distance verticale de la cible
				"<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Tirs_Realises [i].Position_Cible.DistanceY +
				"</Data></Cell>" +
				// La taille du projectile
				"<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Tirs_Realises [i].Projectile.Taille +
				"</Data></Cell>" +
				// Le poids du projectile
				"<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Tirs_Realises [i].Projectile.Poids +
				"</Data></Cell>";

			// Si le score est pris en compte, le nombre de points gagnés (-N si la cible a été manquée)
			if (prise_en_compte_score == true) {
				passation += "<Cell><Data ss:Type=\"Number\">";
				if (GameController.Jeu.Reussiste_Tirs [i] == true) 
					passation += nb_points_gagnes_par_cible;
				else
					passation += "-" + nb_points_perdus_par_cible;
				passation += "</Data></Cell>";
			}

			// Le délai pour lancer le projectile
			passation += "<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Config.Delai_lancer_projectile +
				"</Data></Cell>" +
				// Le délai pour évaluer la cible
				"<Cell><Data ss:Type=\"Number\">" +
				GameController.Jeu.Config.Delai_evaluation_cible +
				"</Data></Cell>" +
				// Le temps mis pour tirer
				"<Cell><Data ss:Type=\"Number\">" +
				Math.Round (GameController.Jeu.Temps_Mis_Pour_Tirer [i], 2) +
				"</Data></Cell>" +
				// Le temps mis pour évaluer la cible
				"<Cell><Data ss:Type=\"Number\">" +
				Math.Round (GameController.Jeu.Temps_Mis_Pour_Evaluer [i], 2) +
				"</Data></Cell>" +
				// Le résultat concernant l'atteinte de la cible
				"<Cell><Data ss:Type=\"String\">";
			if (GameController.Jeu.Reussiste_Tirs [i] == true) 
				passation += "Touche";
			else 
				passation += "Manque";
			passation += "</Data></Cell>";

			// On ne stocke pas la valeur d'une évaluation au leap si celle-ci est égale à 99f (cible non évaluée)
			if (GameController.Jeu.Mesures_Taille_Cible [i] != 99f) {
				// L'évaluation de la cible
				passation += "<Cell><Data ss:Type=\"Number\">" +
					Math.Round ((GameController.Jeu.Mesures_Taille_Cible [i] / 10), 2) +
					"</Data></Cell>" +
					// La différence entre l'évaluation et la taille réelle de la cible
					"<Cell><Data ss:Type=\"Number\">" +
					(Math.Round ((GameController.Jeu.Mesures_Taille_Cible [i] / 10), 2) - GameController.Jeu.Tirs_Realises [i].Taille_Cible) +
					"</Data></Cell>" +
					// La valeur absolue du précédent résultat
					"<Cell><Data ss:Type=\"Number\">" +
					(Math.Abs(Math.Round ((GameController.Jeu.Mesures_Taille_Cible [i] / 10), 2) - GameController.Jeu.Tirs_Realises [i].Taille_Cible)) +
					"</Data></Cell>";
			}
			passation += "</Row>";
		}

		// Tous les intitulés "Moyenne" là où il y en a besoin
		passation += "<Row></Row>" +
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
			"</Data></Cell>";
		if (prise_en_compte_score == true) {
			passation += "<Cell><Data ss:Type=\"String\">" +
				"Moyenne" +
				"</Data></Cell>";
		}
			passation += "<Cell><Data ss:Type=\"String\">" +
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
			// On réalise un pourcentage du nombre de touchés/manqués
			"<Cell><Data ss:Type=\"String\">" +
			"Taux de reussite" +
			"</Data></Cell>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Moyenne" +
			"</Data></Cell>" +
			"<Cell></Cell>" +
			"<Cell><Data ss:Type=\"String\">" +
			"Moyenne" +
			"</Data></Cell>" +
			"</Row>" +
			"<Row><Cell></Cell>" +
			// Calcul des moyennes arrondies à 2 chiffres après la virgule pour chaque colonne
			// Le C (column) reste identique car on travaille sur la colonne courante
			// Le R (row) varie afin de prendre en compte la moyenne de l'ensemble des lancers (il faut se baser par rapport à la ligne courante)
			"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
			"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
			"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
			"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
			"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>";
				if (prise_en_compte_score == true)
					passation += "<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>";
			passation += "<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
				// Calcul du taux de réussite (cible touchée/manquée)
				"<Cell><Data ss:Type=\"Number\">";
		if (nbManque == 0) {
			passation += nbReussi;
		}
		else {
			double nR = Convert.ToDouble(nbReussi);
			double nL = Convert.ToDouble(nb_lancers_int);
			passation += Math.Round(nR/nL, 2);
		}

		// S'il y a au moins un lancer avec une évaluation alors on fait la moyenne des évaluations au leap motion ainsi que la moyenne des différences absolues
		if (!aucunLancerAvecEval) {
			passation += "</Data></Cell>" +
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" + 
				"<Cell></Cell>" +
				"<Cell ss:Formula=\"=ROUND(AVERAGE(R[" + premierLancer + "]C:R[" + dernierLancer + "]C), 2)\"><Data ss:Type=\"Number\"></Data> </Cell>" +
				"</Row>" +
				"</Table>" +
				"</Worksheet>";
		}
		// Sinon on met la valeur 0 pour ces champs
		else {
			passation += "</Data></Cell>" +
				"<Cell><Data ss:Type=\"Number\">0</Data></Cell>" + 
				"<Cell></Cell>" +
				"<Cell><Data ss:Type=\"Number\">0</Data></Cell>" +
				"</Row>" +
				"</Table>" +
				"</Worksheet>";
		}

		return passation;
	}

	// Ecriture dans le fichier XML de l'ensemble des statistiques recueillies et calculables
	// Le paramètre indique la passation que l'on va ajouter dans les statistiques (la ligne de celle-ci avec toutes ces informations)
	public String writeStats (int lastInserted) {
		// Nombre de lancers sous forme d'un entier
		int nb_lancers_int = Convert.ToInt32 (nb_lancers);
		// Position des moyennes dans l'onglet de la passation (9 lignes + nb_lancers_int lignes)
		String moyennes = Convert.ToString (9 + nb_lancers_int);
		// Position du premier lancer dans l'onglet de la passation sous forme d'entier
		int premierLancerInt = 7;
		// Précédente valeur sous forme d'une chaine de caractères
		String premierLancer = Convert.ToString (premierLancerInt);

		// Nom du feuillet et taille des colonnes (largeur)
		String stats = "<Worksheet ss:Name=\"Statistiques\">" + 
			"<Table><Column ss:Width=\"140\"/><Column ss:Width=\"90\"/>";

		// Largeur de la colonne "Points obtenus" si le score est pris en compte
		if (prise_en_compte_score == true)
			stats += "<Column ss:Width=\"90\"/>";
				
			// Largeur des autres colonnes 
		stats += "<Column ss:Width=\"130\"/><Column ss:Width=\"140\"/><Column ss:Width=\"70\"/><Column ss:Width=\"90\"/>" +
			"<Column ss:Width=\"100\"/><Column ss:Width=\"70\"/><Column ss:Width=\"190\"/>";

		// Si il y a plus d'un lancer on défini la largeur d'autant de colonnes qu'il n'y a de lancers et une dernière colonne pour regrouper toutes ces valeurs
		// On a ici besoin d'un stockage temporaire de ces valeurs afin de ne pas avoir une formule énorme et compliquée
		if (nb_lancers_int > 1) {
			for (int i = 0; i < nb_lancers_int; i++) {
				stats += "<Column ss:Width=\"270\"/>";	
			}
			stats += "<Column ss:Width=\"130\"/>";
		}

		// Taille de la colonne écart type
		stats += "<Column ss:Width=\"185\"/>" +
			"<Row>" +
			"<Cell></Cell>" +
			// Champ "Taille de la cible"
			"<Cell><Data ss:Type=\"String\">" +
			"Taille de la cible" +
			"</Data></Cell>";

		// Si on prend en compte le score, champ "Points obtenus"
		if (prise_en_compte_score == true) {
			stats += "<Cell><Data ss:Type=\"String\">" +
				"Points obtenus" +
				"</Data></Cell>";
		}
				
		// Champ "Délai imparti pour lancer"
		stats += "<Cell><Data ss:Type=\"String\">" +
			"Delai imparti pour lancer" +
			"</Data></Cell>" +
			// Champ "Délai imparti pour evaluer"
			"<Cell><Data ss:Type=\"String\">" +
			"Delai imparti pour evaluer" +
			"</Data></Cell>" +
			// Champ "Délai lancer"
			"<Cell><Data ss:Type=\"String\">" +
			"Delai lancer" +
			"</Data></Cell>" +
			// Champ "Délai évaluation"
			"<Cell><Data ss:Type=\"String\">" +
			"Delai evaluation" +
			"</Data></Cell>" +
			// Champ "Résultat du lancer"
			"<Cell><Data ss:Type=\"String\">" +
			"Resultat du lancer" +
			"</Data></Cell>" +
			// Champ "Evaluation"
			"<Cell><Data ss:Type=\"String\">" +
			"Evaluation" +
			"</Data></Cell>" +
			// Champ "Différence entre l'évaluation et la taille réelle de la cible"
			"<Cell><Data ss:Type=\"String\">" +
			"Difference (Evaluation - Taille reelle)" +
			"</Data></Cell>";
			// Champ de stockage temporaire du carré de la différence du lancer i - la moyenne des différences
			if (nb_lancers_int > 1) {
				for (int i = 1; i <= nb_lancers_int; i++) {
					stats += "<Cell><Data ss:Type=\"String\">" +
						"Carre de (Difference Lancer" + i + " - Moyenne des differences)" +
						"</Data></Cell>";
				}
				// Champ "Somme de tous ces carrés"
				stats += "<Cell><Data ss:Type=\"String\">" +
					"Somme de tous ces carres" +
					"</Data></Cell>";
			}
			// Champ "Ecart-type des valeurs de différence"
			stats += "<Cell><Data ss:Type=\"String\">" +
			"Ecart-type des valeurs de difference" +
			"</Data></Cell>" +
			"</Row>";
	
		// Cas d'une seule passation
		if (storedPassations.Count == 0) {
			stats += "<Row>" +
				// Numéro de passation
				"<Cell><Data ss:Type=\"String\">" +
				"Passation 1 " +
				"</Data></Cell>" +
				// Moyenne des tailles de cibles
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[0]\"><Data ss:Type=\"Number\"></Data></Cell>";
			// Si on prend en compte le score, moyenne du score
			if (prise_en_compte_score == true) 
				stats += "<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>";
				
			// Moyenne des délais impartis pour lancer
			stats += "<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				// Moyenne des délais impartis pour évaluer la cible
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				// Moyenne des délais réellements mis pour lancer le projectile
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				// Moyenne des délais réellements mis pour évaluer la cible
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				// Taux de réussite des lancers
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				// Moyenne des évaluations au leap motion
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				// Moyenne des valeurs absolues des différences entre l'évaluation et la taille réelle de la cible
				"<Cell ss:Formula=\"=Passation1!R[" + moyennes + "]C[5]\"><Data ss:Type=\"Number\"></Data></Cell>";

				// S'il n'y a qu'un lancer, on met dans la colonne ecart-type la différence absolue du lancer - la moyenne des valeurs absolues des différences (donc 0)
				if (nb_lancers_int == 1) 
					stats += "<Cell ss:Formula=\"=(Passation1!R[" + premierLancer + "]C[4] - RC[-1])\"><Data ss:Type=\"Number\"></Data></Cell>";
				else {
					// On va traiter chaque lancer indépendemment et faire un stockage temporaire comme explicité précédemment
					String lancerSuivant = premierLancer;
					Int32 lancerSuivantInt = premierLancerInt;
					for (int i = 0; i < nb_lancers_int; i++) {
						String c = Convert.ToString (4 - i);
						String rc = Convert.ToString (-1 - i);
						// Carré de la différence du lancer i - la moyenne des différences
						stats += "<Cell ss:Formula=\"=ROUND((Passation1!R[" + lancerSuivant + "]C[" + c + "] - RC[" + rc + "]) * (Passation1!R[" + lancerSuivant + "]C[" + c + "] - RC[" + rc + "]), 2)\"><Data ss:Type=\"Number\"></Data></Cell>";
						lancerSuivantInt = lancerSuivantInt + 1;
						lancerSuivant = Convert.ToString (lancerSuivantInt);
					}

					stats += "<Cell ss:Formula=\"=RC[-1]";
					for (int i = 1; i < nb_lancers_int; i++) {
						String rc = Convert.ToString(-1 - i);
						stats += " + RC[" + rc + "]";
					}
					stats += "\"><Data ss:Type=\"Number\"></Data></Cell>";
					stats += "<Cell ss:Formula=\"=SQRT(RC[-1] / " + nb_lancers_int + ")\"><Data ss:Type=\"Number\"></Data></Cell>";
				}
					
				stats += "</Row>";
		// Sinon on fait la meme chose que précédemment mais en traitant les multiples passations
		} else {
			// On traites les différentes passations existantes (les feuillets existants)
			for (int j = 0; j < storedPassations.Count; j++) {
				String nomPassation = "Passation " + Convert.ToString(storedPassations [j]);
				String nomPassationSansEspace = "Passation" + Convert.ToString(storedPassations [j]);
				moyennes = Convert.ToString ((9 + nb_lancers_int) - j);
				premierLancerInt = 7 - j;
				premierLancer = Convert.ToString (premierLancerInt);

				stats += "<Row>" +
					"<Cell><Data ss:Type=\"String\">" +
					nomPassation + 
					"</Data></Cell>" +
						"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[0]\"><Data ss:Type=\"Number\"></Data></Cell>";
				if (prise_en_compte_score == true) 
					stats += "<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>";
				
				stats += "<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
					"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
						"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
						"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
						"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
						"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
						"<Cell ss:Formula=\"=" + nomPassationSansEspace + "!R[" + moyennes + "]C[5]\"><Data ss:Type=\"Number\"></Data></Cell>";
				if (nb_lancers_int == 1) 
					stats += "<Cell ss:Formula=\"=(" + nomPassationSansEspace + "!R[" + premierLancer + "]C[4] - RC[-1])\"><Data ss:Type=\"Number\"></Data></Cell>";
				else {
					String lancerSuivant = premierLancer;
					Int32 lancerSuivantInt = premierLancerInt;
					for (int i = 0; i < nb_lancers_int; i++) {
						String c = Convert.ToString (4 - i);
						String rc = Convert.ToString (-1 - i);
						stats += "<Cell ss:Formula=\"=ROUND((" + nomPassationSansEspace + "!R[" + lancerSuivant + "]C[" + c + "] - RC[" + rc + "]) * (" + nomPassationSansEspace + "!R[" + lancerSuivant + "]C[" + c + "] - RC[" + rc + "]), 2)\"><Data ss:Type=\"Number\"></Data></Cell>";
						lancerSuivantInt = lancerSuivantInt + 1;
						lancerSuivant = Convert.ToString (lancerSuivantInt);
					}
					
					stats += "<Cell ss:Formula=\"=RC[-1]";
					for (int i = 1; i < nb_lancers_int; i++) {
						String rc = Convert.ToString(-1 - i);
						stats += " + RC[" + rc + "]";
					}
					stats += "\"><Data ss:Type=\"Number\"></Data></Cell>";
					stats += "<Cell ss:Formula=\"=SQRT(RC[-1] / " + nb_lancers_int + ")\"><Data ss:Type=\"Number\"></Data></Cell>";
				}
				
				stats += "</Row>";
			}

			String nomPassationLast = "Passation " + Convert.ToString(lastInserted);
			String nomPassationLastSansEspace = "Passation" + Convert.ToString(lastInserted);
			moyennes = Convert.ToString ((9 + nb_lancers_int) - storedPassations.Count);
			premierLancerInt = 7 - storedPassations.Count;
			premierLancer = Convert.ToString (premierLancerInt);

			stats += "<Row>" +
				"<Cell><Data ss:Type=\"String\">" +
				nomPassationLast +
				"</Data></Cell>" +
				"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[0]\"><Data ss:Type=\"Number\"></Data></Cell>";
			if (prise_en_compte_score == true) 
				stats += "<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>";
			
			stats += "<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
				"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
					"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
					"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
					"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
					"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[4]\"><Data ss:Type=\"Number\"></Data></Cell>" +
					"<Cell ss:Formula=\"=" + nomPassationLastSansEspace + "!R[" + moyennes + "]C[5]\"><Data ss:Type=\"Number\"></Data></Cell>";
			if (nb_lancers_int == 1) 
				stats += "<Cell ss:Formula=\"=(" + nomPassationLastSansEspace + "!R[" + premierLancer + "]C[4] - RC[-1])\"><Data ss:Type=\"Number\"></Data></Cell>";
			else {
				String lancerSuivant = premierLancer;
				Int32 lancerSuivantInt = premierLancerInt;
				for (int i = 0; i < nb_lancers_int; i++) {
					String c = Convert.ToString (4 - i);
					String rc = Convert.ToString (-1 - i);
					stats += "<Cell ss:Formula=\"=ROUND((" + nomPassationLastSansEspace + "!R[" + lancerSuivant + "]C[" + c + "] - RC[" + rc + "]) * (" + nomPassationLastSansEspace + "!R[" + lancerSuivant + "]C[" + c + "] - RC[" + rc + "]), 2)\"><Data ss:Type=\"Number\"></Data></Cell>";
					lancerSuivantInt = lancerSuivantInt + 1;
					lancerSuivant = Convert.ToString (lancerSuivantInt);
				}
				
				stats += "<Cell ss:Formula=\"=RC[-1]";
				for (int i = 1; i < nb_lancers_int; i++) {
					String rc = Convert.ToString(-1 - i);
					stats += " + RC[" + rc + "]";
				}
				stats += "\"><Data ss:Type=\"Number\"></Data></Cell>";
				stats += "<Cell ss:Formula=\"=RC[-1] / " + nb_lancers_int + "\"><Data ss:Type=\"Number\"></Data></Cell>";
			}
			
			stats += "</Row>";
		}

		stats += "</Table>" +
			"</Worksheet>" +
			"</Workbook>";
		
		return stats;
	}

	// Ecriture dans le fichier XML de l'en-tete XML, de la configuration, de l'ensemble des passations et statistiques
	public void writeXML() {
		// Si le jeu est en pretest on lui ajoute cette extension
		if (GameController.Jeu.isPretest) 
			fichierCourant = nom_configuration + "_pretest.xml";
		else
			fichierCourant = nom_configuration + ".xml";

		// Si le fichier n'existe pas alors on le créé et on stocke l'ensemble des informations
		if (!File.Exists (fichierCourant)) {
			FileStream fs = File.Open (fichierCourant, FileMode.Create);
			
			string text;
			
			text = writeHeader();
			
			text += writeConfig();
			
			text += writePassation(1);
			
			text += writeStats(1);
			
			Byte[] info = new UTF8Encoding (true).GetBytes (text);
			fs.Write (info, 0, text.Length);
			
			fs.Close ();
		// Sinon on récupère son contenu ainsi que le numéro de passation à créer
		} else {
			String contenu = "";
			
			contenu = System.IO.File.ReadAllText(fichierCourant);

			// Numéro de passation à créer
			int numPassation = this.getNumPassation (contenu);

			// On sépare tous les feuillets
			string[] lines = Regex.Split(contenu, "</Worksheet>");
			
			String textToWrite = "";

			// On réécrit ce qui reste inchangé
			for (int i = 0; i < (lines.Length - 2); i++) {
				textToWrite += lines[i] + "</Worksheet>";
			}
			
			lines[(lines.Length - 2)] += "</Worksheet>";

			// On créé le feuillet de la dernière passation
			textToWrite += writePassation(numPassation);

			// On ajoute les statistiques de la dernière passation
			textToWrite += writeStats(numPassation);
			
			FileStream fs = File.Open (fichierCourant, FileMode.Create);
			
			Byte[] info = new UTF8Encoding (true).GetBytes (textToWrite);
			fs.Write (info, 0, textToWrite.Length);
			
			fs.Close ();
		}
	}

	// Récupération du numéro de passation courant (la passation à créer dans le fichier XML)
	public int getNumPassation(String contenu) {
		int nbPassations = 0;

		// On va stocker les numéros de passation existants et indiquer quelle est la nouvelle passation à créer (il s'agit de la dernière valeur trouvée + 1)
		String basis = "Passation";

		for (int i = 1; i <= 1000; i++) {
			basis += i;
			if (contenu.Contains (basis)) {
				storedPassations.Add(i);
				nbPassations = i;
			}

			basis = "Passation";
		}
		
		return (nbPassations + 1);
	}
}
