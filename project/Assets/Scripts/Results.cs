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
	public GameObject bg_delai_avant_disparition_cible;
	public GameObject bg_delai_avant_evaluation_cible;
	public GameObject position_gravite;
	public GameObject poition_rigidite_lancepierre;
	public GameObject position_nb_lancers;
	public GameObject position_nb_series;
	public GameObject position_nb_positions;
	public GameObject position_nb_tailles_cibles;
	public GameObject position_nb_tailles_projectiles;
	public GameObject position_afficher_le_score;
	public GameObject position_nb_points_gagnes_par_cible;
	public GameObject position_nb_points_perdus_par_cible;
	public GameObject position_delai_lancer_projectile;
	public GameObject position_delai_evaluation_cible;
	public GameObject position_delai_validation_mesure;
	public GameObject position_marge_stabilisation_leap;
	public GameObject position_condition_test;
	
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
			int y_modif = 25;
			Vector3 v = position_gravite.transform.position;
			position_gravite.transform.position = new Vector3(v.x, v.y-y_modif);

			v = poition_rigidite_lancepierre.transform.position;
			poition_rigidite_lancepierre.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_lancers.transform.position;
			position_nb_lancers.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_series.transform.position;
			position_nb_series.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_positions.transform.position;
			position_nb_positions.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_tailles_cibles.transform.position;
			position_nb_tailles_cibles.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_tailles_projectiles.transform.position;
			position_nb_tailles_projectiles.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_afficher_le_score.transform.position;
			position_afficher_le_score.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_points_gagnes_par_cible.transform.position;
			position_nb_points_gagnes_par_cible.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_nb_points_perdus_par_cible.transform.position;
			position_nb_points_perdus_par_cible.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_delai_lancer_projectile.transform.position;
			position_delai_lancer_projectile.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_delai_evaluation_cible.transform.position;
			position_delai_evaluation_cible.transform.position = new Vector3(v.x, v.y-y_modif);
			
			v = position_delai_validation_mesure.transform.position;
			position_delai_validation_mesure.transform.position = new Vector3(v.x, v.y-y_modif);
			
			v = position_marge_stabilisation_leap.transform.position;
			position_marge_stabilisation_leap.transform.position = new Vector3(v.x, v.y-y_modif);

			v = position_condition_test.transform.position;
			position_condition_test.transform.position = new Vector3(v.x, v.y-y_modif);

			if (GameController.Jeu.Config.Condition_De_Controle) {
				condition_test.text = "C";
			} else if (GameController.Jeu.Config.Condition_De_Perception) {
				condition_test.text = "P";
			}
		}

		//Assignation des valeurs par traitement des résultats obtenus durant la partie
		score_final.text = Convert.ToString(GameController.Jeu.Score); 
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
		//Ici on comptera le nombre de valeurs correspondantes
		/*nombre_evaluations_moins_1cm.text = "2"; //- 1cm
		nombre_evaluations_environ_2cm.text = "5"; //entre 1 et 2,4cm
		nombre_evaluations_environ_3cm.text = "10"; //entre 2,5 et 3,4cm
		nombre_evaluations_environ_4cm.text = "3"; //entre 3,5 et 4,4cm
		nombre_evaluations_environ_5cm.text = "0"; //entre 4,5 et 4,9cm
		nombre_evaluations_sup_5cm_inf_10cm.text = "0"; //entre 5 et 9,9cm
		nombre_evaluations_sup_10cm.text = "0"; //+ 10cm
		temps_evaluation_cible.text = "2"; //Ici on fera une moyenne des temps*/
	}

	public void onClickOpenXML() {
		Process.Start (fichierCourant);
	}
	
	public void onClickOpenAllXML() {
		Process.Start (fichierCourant); // à modifier par la suite
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
