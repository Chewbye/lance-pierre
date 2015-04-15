using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReglageCouleurBarre : MonoBehaviour {

	public Slider S_R;
	public Slider S_G;
	public Slider S_B;
	public Slider S_A;

	public Texture2D pickColor;
	public Texture2D pickAlpha;

	public Color couleurBarre;
	public Color couleurBarreDefault;

	public Texture2D cadre;
	public Texture2D remplissage;
	public Texture2D textureDeFond;

	// Use this for initialization
	void Start () {

		S_R.value = GameController.Jeu.Config.Couleur_barre.r;
		S_G.value = GameController.Jeu.Config.Couleur_barre.g; 
		S_B.value = GameController.Jeu.Config.Couleur_barre.b;
		S_A.value = GameController.Jeu.Config.Couleur_barre.a;

		couleurBarreDefault = new Color32(95, 222, 95, 255);

		couleurBarre = new Color32 ((byte)S_R.value, (byte)S_G.value, (byte)S_B.value, (byte)S_A.value);

		cadre = Resources.Load ("BarreVideCouleur") as Texture2D;
		remplissage = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		textureDeFond = new Texture2D(1, 1, TextureFormat.ARGB32, false);

		changeColor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		changeColor ();
		Show (Screen.width, Screen.height, Screen.width, Screen.height/2);
	}

	public void changeColor()
	{
//		Color c = new Color32(95, 222, 95, 0);
//
//		for (int j=0; j<remplissage.width; j++) {
//			for (int i=0; i<remplissage.height; i++)
//			{
//				if (!remplissage.GetPixel(i,j).Equals(c))
//				{
//					remplissage.SetPixel(i,j, couleurBarre);
//				}
//
//			}
//		}
		textureDeFond.SetPixel (0, 0, cadre.GetPixel (0, 0));
		remplissage.SetPixel (0, 0, couleurBarre);
		textureDeFond.Apply ();
		remplissage.Apply ();
	}

	public void Show(int x,int y,int tailleTextureWidth,int tailleTextureHeight)
	{
		int largeur = 512;
		int hauteur = 256;

		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y)/2, tailleTextureWidth, tailleTextureHeight), textureDeFond);
		GUI.DrawTexture (new Rect ((x-largeur)/2, ((y-hauteur)/2) + (y/4), largeur, hauteur), remplissage);
		GUI.DrawTexture (new Rect ((x-largeur)/2, ((y-hauteur)/2) + (y/4), largeur, hauteur), cadre);
		//GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y)/2, tailleTextureWidth, tailleTextureHeight), remplissage);
		//GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y)/2, tailleTextureWidth, tailleTextureHeight), cadre);
	}

	public void onChangeValueS_R()
	{
		float RValue = (((float)S_R.value) / 255f);
		couleurBarre.r = (float)Math.Round(RValue,3,MidpointRounding.AwayFromZero);
	}

	public void onChangeValueS_G()
	{
		float GValue = (((float)S_G.value) / 255f);
		couleurBarre.g = (float)Math.Round(GValue,3,MidpointRounding.AwayFromZero);
	}

	public void onChangeValueS_B()
	{
		float BValue = (((float)S_B.value) / 255f);
		couleurBarre.b = (float)Math.Round(BValue,3,MidpointRounding.AwayFromZero);
	}

	public void onChangeValueS_A()
	{
		float AValue = (((float)S_A.value) / 255f);
		couleurBarre.a = (float)Math.Round(AValue,3,MidpointRounding.AwayFromZero);
	}

	public void onClickButtonColorDefault()
	{
		couleurBarre = couleurBarreDefault;
		GameController.Jeu.Config.Couleur_barre = new Color32(95, 222, 95, 255);
		S_R.value = GameController.Jeu.Config.Couleur_barre.r;
		S_G.value = GameController.Jeu.Config.Couleur_barre.g;
		S_B.value = GameController.Jeu.Config.Couleur_barre.b;
		S_A.value = GameController.Jeu.Config.Couleur_barre.a;
	}

	public void onClickButtonRetourMenu()
	{
		GameController.Jeu.Config.Couleur_barre = couleurBarre;
		UnityEngine.Application.LoadLevel ("mainMenu");
	}
	


}
