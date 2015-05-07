using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/*
 * ** ReglageCouleurBarre **
 * 
 * Classe qui permet d'agir sur la scène de configuration de la couleur de la barre de progression
 */

public class ReglageCouleurBarre : MonoBehaviour {

	public Slider S_R; // slider permettant de régler le niveau de rouge
	public Slider S_G; // slider permettant de régler le niveau de vert
	public Slider S_B; // slider permettant de régler le niveau de bleue
	public Slider S_A; // slider permettant de régler le niveau de radiation alpha

	public Text Text_R; // libelle du slider de niveau de rouge
	public Text Text_G; // libelle du slider de niveau de vert
	public Text Text_B; // libelle du slider de niveau de bleue
	public Text Text_A; // libelle du slider de niveau de radiation alpha

	public Texture2D pickColor; // texture correspondant à l'ensemble des couleurs que l'on peut choisir
	public Texture2D pickAlpha; // texture correspondant à l'ensemble des radiations alpha

	public Color couleurBarre; // variable qui contiendra la couleur de la barre
	public Color couleurBarreDefault; // couleur de la barre par default

	public Texture2D cadre; // texture du contour de la barre de progression
	public Texture2D remplissage; // texture du contenu de la barre de progression
	public Texture2D textureDeFond; // texture de fond de la barre de progression

	public int posX; // position x de la texture pickColor
	public int posY; // position y de la texture pickColor
	public int largeur; // largeur de la texture pickColor
	public int hauteurPickColor; // hauteur de la texture pickColor
	public int hauteurPickAlpha; // hauteur de la texture pickAlpha
	
	// Use this for initialization
	/*
	 * Création et initilisation des différents attributs et composants
	 */ 
	void Start () {
	
		S_R.value = GameController.Jeu.Config.Couleur_barre.r;
		S_G.value = GameController.Jeu.Config.Couleur_barre.g; 
		S_B.value = GameController.Jeu.Config.Couleur_barre.b;
		S_A.value = GameController.Jeu.Config.Couleur_barre.a;

		couleurBarreDefault = new Color32(95, 222, 95, 255);

		couleurBarre = new Color32 ((byte)S_R.value, (byte)S_G.value, (byte)S_B.value, (byte)S_A.value);

		pickColor = Resources.Load ("hsv_space") as Texture2D;
		pickAlpha = Resources.Load ("alpha_gradient") as Texture2D;
		cadre = Resources.Load ("BarreVideCouleur") as Texture2D;
		remplissage = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		textureDeFond = new Texture2D(1, 1, TextureFormat.ARGB32, false);

		posX = 80;
		posY = 5;
		largeur = pickColor.width;
		hauteurPickColor = pickColor.height;
		hauteurPickAlpha = pickAlpha.height;


		changeColor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Création graphique des textures et gestion des evenements sur le clique sur les textures pickColor ou pickAlpha
	 */
	void OnGUI()
	{
		changeColor (); // Application du changement de couleur fait précédemment

		Show (Screen.width, Screen.height, Screen.width, Screen.height/2); // création graphique de la barre de progression

		// création graphique des textures pickColor et pickAlpha
		GUI.DrawTexture (new Rect (posX, posY, largeur, hauteurPickColor), pickColor);
		GUI.DrawTexture (new Rect (posX, posY + hauteurPickColor, largeur, hauteurPickAlpha), pickAlpha);

		// on sauvegarde l'evenement courant
		Event e = Event.current;
		bool isLeftMBtnClicked = e.type == EventType.mouseUp;

		// on verifie que c'est bien un clique gauche sur la souris qui a été detecté
		if (isLeftMBtnClicked) {

			// on prend la position de ce clique
			Vector2 v = e.mousePosition;

			int vx = (int)v.x;
			int vy = (int)v.y;

			// si le clique s'est fait sur la texture pickAlpha alors on traite juste le niveau de radiation alpha de la couleur
			if (clickOnPickAlpha(v))
			{
				Color couleurAlpha = pickAlpha.GetPixel(vx - posX,hauteurPickAlpha - (vy - (posY + hauteurPickColor)));

				Debug.Log("clickOnPickAlpha - " + v.ToString() + " " + couleurAlpha.ToString());
	
				float codeCorrespondant = couleurAlpha.r * 255f;

				S_A.value = (int) Math.Round((double)codeCorrespondant);
			}
			else if (clickOnPickColor(v)) // si le clique s'est fait sur la texture pickColor alors on traite juste les niveaux R, G et B 
			{
				Color couleurColor = pickColor.GetPixel(vx - posX,hauteurPickColor - (vy - posY));

				Debug.Log("clickOnPickColor - " + v.ToString() + " " + couleurColor.ToString() + " - " + (vx - posX) + " " + (vy - posY));

				float codeCorrespondantR = couleurColor.r * 255f;
				float codeCorrespondantG = couleurColor.g * 255f;
				float codeCorrespondantB = couleurColor.b * 255f;

				S_R.value = (int) Math.Round((double)codeCorrespondantR);
				S_G.value = (int) Math.Round((double)codeCorrespondantG);
				S_B.value = (int) Math.Round((double)codeCorrespondantB);
			}
			else{
				Debug.Log("clockOn Other - " + v.ToString());
			}
		}
	}

	/*
	 * ** changeColor **
	 * 
	 * Changement graphique de la couleur de la barre 
	 */
	public void changeColor()
	{
		textureDeFond.SetPixel (0, 0, cadre.GetPixel (0, 0));
		remplissage.SetPixel (0, 0, couleurBarre);
		textureDeFond.Apply ();
		remplissage.Apply ();
	}

	/*
	 * ** Show **
	 * 
	 * Création graphique de la barre de progression
	 */
	public void Show(int x,int y,int tailleTextureWidth,int tailleTextureHeight)
	{
		int largeur = 512;
		int hauteur = 256;

		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y)/2, tailleTextureWidth, tailleTextureHeight), textureDeFond);
		GUI.DrawTexture (new Rect ((x-largeur)/2, ((y-hauteur)/2) + (y/4), largeur, hauteur), remplissage);
		GUI.DrawTexture (new Rect ((x-largeur)/2, ((y-hauteur)/2) + (y/4), largeur, hauteur), cadre);
	}

	/*
	 * ** clickOnPickColor **
	 * 
	 * Retourne vrai si la position du clique se trouve sur la zone de la texture pickColor
	 */
	public Boolean clickOnPickColor(Vector2 v)
	{
		return ((v.x >= posX && v.x <= posX + largeur) && (v.y >= posY && v.y <= posY + hauteurPickColor)); 
	}

	/*
	 * ** clickOnPickAlpha **
	 * 
	 * Retourne vrai si la position du clique se trouve sur la zone de la texture pickAlpha
	 */
	public Boolean clickOnPickAlpha(Vector2 v)
	{
		return ((v.x >= posX && v.x <= posX + largeur) && (v.y >= posY + hauteurPickColor && v.y <= posY + hauteurPickColor + hauteurPickAlpha)); 
	}

	/*
	 * ** GESTION DES EVENEMENTS SUR LES SLIDERS R, G, B et A **
	 */
	public void onChangeValueS_R()
	{
		Text_R.text = "R : " + (int)S_R.value;
		float RValue = (((float)S_R.value) / 255f);
		couleurBarre.r = (float)Math.Round(RValue,3,MidpointRounding.AwayFromZero);
	}

	public void onChangeValueS_G()
	{
		Text_G.text = "G : " + (int)S_G.value;
		float GValue = (((float)S_G.value) / 255f);
		couleurBarre.g = (float)Math.Round(GValue,3,MidpointRounding.AwayFromZero);
	}

	public void onChangeValueS_B()
	{
		Text_B.text = "B : " + (int)S_B.value;
		float BValue = (((float)S_B.value) / 255f);
		couleurBarre.b = (float)Math.Round(BValue,3,MidpointRounding.AwayFromZero);
	}

	public void onChangeValueS_A()
	{
		Text_A.text = "A : " + (int)S_A.value;
		float AValue = (((float)S_A.value) / 255f);
		couleurBarre.a = (float)Math.Round(AValue,3,MidpointRounding.AwayFromZero);
	}

	/*
	 * ** GESTION DU CLIQUE SUR LE BOUTON "couleur par default" **
	 * 
	 */ 
	public void onClickButtonColorDefault()
	{
		couleurBarre = couleurBarreDefault;
		GameController.Jeu.Config.Couleur_barre = new Color32(95, 222, 95, 255);
		S_R.value = GameController.Jeu.Config.Couleur_barre.r;
		S_G.value = GameController.Jeu.Config.Couleur_barre.g;
		S_B.value = GameController.Jeu.Config.Couleur_barre.b;
		S_A.value = GameController.Jeu.Config.Couleur_barre.a;
	}

	/*
	 * ** GESTION DU CLIQUE SUR LE BOUTON "retour au menu" **
	 * 
	 */ 
	public void onClickButtonRetourMenu()
	{
		GameController.Jeu.Config.Couleur_barre = couleurBarre;
		UnityEngine.Application.LoadLevel ("mainMenu");
	}
	


}
