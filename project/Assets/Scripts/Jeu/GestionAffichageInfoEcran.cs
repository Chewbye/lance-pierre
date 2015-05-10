using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Classe utilisee pour initialiser et mettre a jour les informations qui apparaissent a l'ecran lors d'une partie
public class GestionAffichageInfoEcran : MonoBehaviour 
{
	public GameObject infoScore;
	public GameObject infoGagnePerdu;

	public GameObject cible;
	
	// Use this for initialization
	void Start () 
	{
		// INITIALISATION DU TEXTE AFFICHANT LE SCORE
		if(GameController.Jeu.Config.Afficher_le_score)
		{
			Text txt = infoScore.GetComponent<Text>(); 
			txt.text= "Score : " + GameController.Jeu.Score;
		}
		else
		{
			Text txt = infoScore.GetComponent<Text>(); 
			txt.text= "";
		}

		// INITIALISATION DU TEXTE AFFICHANT LES POINTS PERDUS OU GAGNES
		//
	}
	// Update is called once per frame
	void Update () 
	{
		// MISE A JOUR DU TEXTE AFFICHANT LE SCORE
		if(GameController.Jeu.Config.Afficher_le_score)
		{
			Text txt = infoScore.GetComponent<Text>(); 
			txt.text= "Score : " + GameController.Jeu.Score;
		}
		else
		{
			Text txt = infoScore.GetComponent<Text>(); 
			txt.text= "";
		}

		// MISE A JOUR DU TEXTE AFFICHANT LES POINTS PERDUS OU GAGNES
		Vector3 positionCible = cible.transform.position;
		double diametreCible = cible.renderer.bounds.size.x;

		// Changement de la position du texte affichant les points perdus ou gagnes a proximite de la cible
		infoGagnePerdu.transform.position = new Vector3((float)(positionCible.x - diametreCible), (float)(positionCible.y - diametreCible), infoGagnePerdu.transform.position.z);

		// On affiche un message en vert indiquant le nombre de points gagnes
		if(GameController.Jeu.Cible_Touchee && GameController.Jeu.Config.Afficher_le_score) 
		{
			TextMesh textMesh = infoGagnePerdu.GetComponent(typeof(TextMesh)) as TextMesh;
			textMesh.color = Color.green;
			textMesh.text = "+" + GameController.Jeu.Config.Nb_points_gagnes_par_cible + " points";
		}

		// On affiche un message en rouge indiquant le nombre de points perdus
		if(GameController.Jeu.Cible_Manquee && GameController.Jeu.Config.Afficher_le_score) 
		{
			TextMesh textMesh = infoGagnePerdu.GetComponent(typeof(TextMesh)) as TextMesh;
			textMesh.color = Color.red;
			textMesh.text = "-" + GameController.Jeu.Config.Nb_points_perdus_par_cible_manque + " points";
		}

		if(GameController.Jeu.Tir_Fini)
		{
			cible.renderer.enabled = true;
			TextMesh textMesh = infoGagnePerdu.GetComponent(typeof(TextMesh)) as TextMesh;
			textMesh.text = "";
		}
	}
}