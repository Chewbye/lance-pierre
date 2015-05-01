using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * ** ReglageTailleBarre **
 * 
 * Classe qui permet d'agir sur la scène de configuration de la taille de la barre de progression
 */

public class ReglageTailleBarre : MonoBehaviour {

	public Slider S_Largeur; // Slider permettant de régler la largeur de la barre de progression
	public Slider S_Hauteur; // Slider permettant de régler la hauteur de la barre de progression

	protected Texture2D cadre; // texture du contour de la barre de progression
	protected Texture2D remplissage; // texture du contenu de la barre de progression

	// Use this for initialization
	/*
	 * Création et initilisation des différents attributs et composants
	 */ 
	void Start () {

		S_Largeur.minValue = 0;
		S_Largeur.maxValue = Screen.width;
		S_Hauteur.minValue = 0;
		S_Hauteur.maxValue = Screen.height;

		S_Largeur.value = GameController.Jeu.Config.Largeur_barre_progression;
		S_Hauteur.value = GameController.Jeu.Config.Hauteur_barre_progression;

		cadre = Resources.Load ("BarreVide") as Texture2D;
		remplissage = Resources.Load ("Remplissage") as Texture2D;
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (S_Largeur.value + "_" + S_Hauteur.value + "\n");
	}

	/*
	 * Création graphique des textures en fonction de la largeur et de la hauteur choisient.
	 */
	void OnGUI()
	{
		Show (Screen.width, Screen.height, (int)S_Largeur.value, (int)S_Hauteur.value);
	}

	/*
	 * ** Show **
	 * 
	 * Création graphique de la barre de progression
	 */
	public void Show(int x,int y,int tailleTextureWidth,int tailleTextureHeight)
	{
		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y-tailleTextureHeight)/2, tailleTextureWidth, tailleTextureHeight), cadre);
		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y-tailleTextureHeight)/2, tailleTextureWidth, tailleTextureHeight), remplissage);
	}

	/*
	 * ** GESTION DES EVENEMENTS SUR LES SLIDERS largeur et hauteur **
	 */
	public void onChangeValueHauteur()
	{
		GameController.Jeu.Config.Hauteur_barre_progression = (int)S_Hauteur.value;
	}

	public void onChangeValueLargeur()
	{
		GameController.Jeu.Config.Largeur_barre_progression = (int)S_Largeur.value;
	}

	/*
	 * ** GESTION DU CLIQUE SUR LE BOUTON "retour au menu" **
	 * 
	 */ 
	public void onClickRetourMenu()
	{
		UnityEngine.Application.LoadLevel ("mainMenu");
	}
}
