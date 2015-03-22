using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffichageGagnePerdu : MonoBehaviour 
{
	public GameObject cible;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// CHANGEMENT DE LA POSITION DU TEXTE DE REUSSITE A PROXIMITE DE LA CIBLE
		Vector3 positionCible = cible.transform.position;
		double diametreCible = cible.renderer.bounds.size.x;

		transform.position = new Vector3((float)(positionCible.x - diametreCible), (float)(positionCible.y - diametreCible), transform.position.z);

		if(GameController.Jeu.Cible_Touchee)
		{
			TextMesh textMesh = GetComponent(typeof(TextMesh)) as TextMesh;
			textMesh.color = Color.green;
			textMesh.text = "+" + GameController.Jeu.Config.Nb_points_gagnes_par_cible + " points";
		}

		if(GameController.Jeu.Cible_Manquee)
		{
			TextMesh textMesh = GetComponent(typeof(TextMesh)) as TextMesh;
			textMesh.color = Color.red;
			textMesh.text = "-" + GameController.Jeu.Config.Nb_points_perdus_par_cible_manque + " points";
		}
	}
}
