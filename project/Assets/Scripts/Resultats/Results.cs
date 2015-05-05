using UnityEngine;
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

	// Variables utiles pour l'affichage des résultats dans la scène "results"
	public Text gravite;
	public Text rigidite_lancepierre;
	public Text nb_lancers;
	public Text nb_series;
	public Text nb_positions;
	public Text nb_tailles_cibles;
	public Text nb_tailles_projectiles;
	public Toggle afficher_le_score;
	public Text nb_points_gagnes_par_cible;
	public Text nb_points_perdus_par_cible;
	public Text delai_lancer_projectile;
	public Text delai_evaluation_cible;
	public Text delai_validation_mesure;
	public Text marge_stabilisation_leap;
	public Text condition_test;
	public Text delai_avant_disparition_cible;
	public Text delai_avant_evaluation_cible;
	public Text intitule_delai_avant_disparition_cible;
	public Text intitule_delai_avant_evaluation_cible;
	public Toggle prise_en_compte_score;
	public Text hauteur_lance_pierre;
	public Text position_X_LP;
	public Text position_Y_LP;
	public Text nom_config;
	public Text num_participant;
	public Text age_participant;
	public Text sexe_participant;
	public Text main_forte_participant;
	public Text exp_participant;
	public Toggle afficher_barre_progression;
	public Text couleur_cible;
	public Text hauteur_barre_progression;
	public Text intitule_hauteur_barre_progression;
	public Text largeur_barre_progression;
	public Text intitule_largeur_barre_progression;
	public Toggle afficher_effet_destruct;
	public Text intitule_couleur_barre_progression;
	public Text couleur_barre_progression;

	// Cadre situé en arrière-plan de l'élément susnommé
	public GameObject bg_delai_avant_disparition_cible;
	public GameObject bg_delai_avant_evaluation_cible;
	public GameObject bg_hauteur_barre_progression;
	public GameObject bg_largeur_barre_progression;
	public GameObject bg_couleur_barre_progression;
	
	// Récapitulatif
	public Text score_final;
	public Text nombre_cibles_touchees;
	public Text nombre_cibles_manquees;
	public Text reussis_nombre_evaluations_plus_ou_moins_25mm;
	public Text reussis_nombre_evaluations_plus_ou_moins_50mm;
	public Text reussis_nombre_evaluations_plus_ou_moins_150mm;
	public Text manques_nombre_evaluations_plus_ou_moins_25mm;
	public Text manques_nombre_evaluations_plus_ou_moins_50mm;
	public Text manques_nombre_evaluations_plus_ou_moins_150mm;

	string fichierCourant;
	
	// Use this for initialization
	void Start () {
		if (GameController.Jeu == null) 
			GameController.Jeu = new Jeu ();

		string nomFichier; 
		if (GameController.Jeu.isPretest) 
			nomFichier = GameController.Jeu.Config.Name + "_pretest.xml";
		else 
			nomFichier = GameController.Jeu.Config.Name + ".xml";
		fichierCourant = nomFichier;

		setFields ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Création et modification des champs de la scène "results"
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
		nb_points_perdus_par_cible.text = Convert.ToString(GameController.Jeu.Config.Nb_points_perdus_par_cible_manque);
		delai_lancer_projectile.text = Convert.ToString(GameController.Jeu.Config.Delai_lancer_projectile);
		delai_evaluation_cible.text = Convert.ToString(GameController.Jeu.Config.Delai_evaluation_cible);
		delai_validation_mesure.text = Convert.ToString (GameController.Jeu.Config.Delai_validation_mesure_cible);
		marge_stabilisation_leap.text = Convert.ToString (GameController.Jeu.Config.Marge_stabilisation_validation_cible);
		prise_en_compte_score.isOn = GameController.Jeu.Config.Prise_en_compte_du_score;
		hauteur_lance_pierre.text = Convert.ToString (GameController.Jeu.Config.Taille_Hauteur_Catapulte);
		position_X_LP.text = Convert.ToString (GameController.Jeu.Config.Distance_X_Catapulte);
		position_Y_LP.text = Convert.ToString (GameController.Jeu.Config.Distance_Y_Catapulte);
		nom_config.text = Convert.ToString (GameController.Jeu.Config.Name);
		num_participant.text = Convert.ToString (GameController.Jeu.Participant.Numero);
		age_participant.text = Convert.ToString (GameController.Jeu.Participant.Age);
		sexe_participant.text = Convert.ToString (GameController.Jeu.Participant.Sexe);
		main_forte_participant.text = Convert.ToString (GameController.Jeu.Participant.MainDominante);
		exp_participant.text = Convert.ToString (GameController.Jeu.Participant.PratiqueJeuxVideo);
		couleur_cible.text = Convert.ToString (GameController.Jeu.Config.Couleur_cible);
		afficher_barre_progression.isOn = GameController.Jeu.Config.Affichage_barre_progression;
		afficher_effet_destruct.isOn = GameController.Jeu.Config.Afficher_effet_destruction_cible;

		if (afficher_barre_progression.isOn == true) {
			intitule_hauteur_barre_progression.text = "Hauteur de la barre de progression :";
			hauteur_barre_progression.text = Convert.ToString (GameController.Jeu.Config.Hauteur_barre_progression);
			intitule_largeur_barre_progression.text = "Largeur de la barre de progression :";
			largeur_barre_progression.text = Convert.ToString (GameController.Jeu.Config.Largeur_barre_progression);
			intitule_couleur_barre_progression.text = "Couleur de la barre :";
			couleur_barre_progression.text = Convert.ToString(GameController.Jeu.Config.Couleur_barre.r) + ", " + Convert.ToString(GameController.Jeu.Config.Couleur_barre.g) + ", " + Convert.ToString(GameController.Jeu.Config.Couleur_barre.b) + ", " + Convert.ToString(GameController.Jeu.Config.Couleur_barre.a);
		} else {
			bg_hauteur_barre_progression.SetActive(false);
			bg_largeur_barre_progression.SetActive(false);
			bg_couleur_barre_progression.SetActive(false);
		}
		if (GameController.Jeu.Config.Condition_De_Memoire) {
			condition_test.text = "M";
			intitule_delai_avant_disparition_cible.text = "Délai avant disparition de la cible :";
			intitule_delai_avant_evaluation_cible.text = "Délai avant évaluation de la cible :";
			delai_avant_disparition_cible.text = Convert.ToString (GameController.Jeu.Config.Delai_avant_disparition_cible);
			delai_avant_evaluation_cible.text = Convert.ToString (GameController.Jeu.Config.Delai_avant_evaluation_cible);
		}
		else {
			bg_delai_avant_disparition_cible.SetActive(false);
			bg_delai_avant_evaluation_cible.SetActive(false);

			if (GameController.Jeu.Config.Condition_De_Controle) {
				condition_test.text = "C";
			} else if (GameController.Jeu.Config.Condition_De_Perception) {
				condition_test.text = "P";
			}
		}

		if (prise_en_compte_score.isOn == true) {
			//Assignation des valeurs par traitement des résultats obtenus durant la partie
			score_final.text = Convert.ToString (GameController.Jeu.Score); 
			int nbReussie = 0;
			int nbManquee = 0;
			foreach (bool essai in GameController.Jeu.Reussiste_Tirs) {
				if (essai == true)
					nbReussie++;
				else
					nbManquee++;
			}
			nombre_cibles_touchees.text = Convert.ToString (nbReussie);
			nombre_cibles_manquees.text = Convert.ToString (nbManquee);
		} else {
			score_final.text = Convert.ToString (0);
			nombre_cibles_manquees.text = Convert.ToString (0);
			nombre_cibles_touchees.text = Convert.ToString (0);
		}

		int reussisNbEvalPlusOuMoins25mm = 0;
		int reussisNbEvalPlusOuMoins50mm = 0;
		int reussisNbEvalPlusOuMoins150mm = 0;
		int manquesNbEvalPlusOuMoins25mm = 0;
		int manquesNbEvalPlusOuMoins50mm = 0;
		int manquesNbEvalPlusOuMoins150mm = 0;

		for (int i = 0; i < GameController.Jeu.Mesures_Taille_Cible.Count; i++) {
			double evalCourante = Math.Round((GameController.Jeu.Mesures_Taille_Cible[i] / 10), 2);
			float tailleCourante = GameController.Jeu.Tirs_Realises[i].Taille_Cible;
			double diffCourante = evalCourante - tailleCourante;

			if ((((diffCourante <= 0.25) & diffCourante >= 0) | ((diffCourante >= -0.25) & diffCourante <= 0)) & (GameController.Jeu.Reussiste_Tirs[i] == true)) {
				reussisNbEvalPlusOuMoins25mm++;
			}
			else if ((((diffCourante <= 0.50) & (diffCourante >= 0)) | (diffCourante >= -0.50 & diffCourante <= 0))  & (GameController.Jeu.Reussiste_Tirs[i] == true)) {
				reussisNbEvalPlusOuMoins50mm++;
			}
			else if (GameController.Jeu.Reussiste_Tirs[i] == true) {
				reussisNbEvalPlusOuMoins150mm++;
			}
			else if ((((diffCourante <= 0.25) & (diffCourante >= 0)) | (diffCourante >= -0.25 & diffCourante <= 0)) & (GameController.Jeu.Reussiste_Tirs[i] == false)) {
				manquesNbEvalPlusOuMoins25mm++;
			}
			else if ((((diffCourante <= 0.50) & (diffCourante >= 0))  | (diffCourante >= -0.50 & diffCourante <= 0)) & (GameController.Jeu.Reussiste_Tirs[i] == false)) {
				manquesNbEvalPlusOuMoins50mm++;
			}
			else {
				manquesNbEvalPlusOuMoins150mm++;
			}
		}

		reussis_nombre_evaluations_plus_ou_moins_25mm.text = Convert.ToString (reussisNbEvalPlusOuMoins25mm);
		reussis_nombre_evaluations_plus_ou_moins_50mm.text = Convert.ToString (reussisNbEvalPlusOuMoins50mm);
		reussis_nombre_evaluations_plus_ou_moins_150mm.text = Convert.ToString (reussisNbEvalPlusOuMoins150mm);
		manques_nombre_evaluations_plus_ou_moins_25mm.text = Convert.ToString (manquesNbEvalPlusOuMoins25mm);
		manques_nombre_evaluations_plus_ou_moins_50mm.text = Convert.ToString (manquesNbEvalPlusOuMoins50mm);
		manques_nombre_evaluations_plus_ou_moins_150mm.text = Convert.ToString (manquesNbEvalPlusOuMoins150mm);
	}

	public void onClickOpenXML() {
		Process.Start (fichierCourant);
	}

	// Bouton permettant d'accéder au répertoire contenant l'ensemble des résultats (il s'agit du répertoire courant)
	public void onClickResultsAccess() {
		System.Diagnostics.Process.Start( ".");
	}
	
	public void onMenu() {
		GameController.Jeu.isPretest = false;
		Application.LoadLevel ("menu");
	}

	public void onSujetSuivant() {
		resetAll ();
		GameController.Jeu.isPretest = false;
		Application.LoadLevel ("menuSecondaire");
	}

	public void resetAll() {
		GameController.Jeu.newGame ();
	}
}
