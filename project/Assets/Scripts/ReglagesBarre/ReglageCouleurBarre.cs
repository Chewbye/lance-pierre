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

	// Use this for initialization
	void Start () {

		S_R.value = GameController.Jeu.Config.Couleur_barre.r;
		S_G.value = GameController.Jeu.Config.Couleur_barre.g; 
		S_B.value = GameController.Jeu.Config.Couleur_barre.b;
		S_A.value = GameController.Jeu.Config.Couleur_barre.a;

		couleurBarreDefault = new Color32(95, 222, 95, 255);

		couleurBarre = new Color32 ((byte)S_R.value, (byte)S_G.value, (byte)S_B.value, (byte)S_A.value);

		cadre = Resources.Load ("BarreVide") as Texture2D;
		remplissage = Resources.Load ("Remplissage") as Texture2D;

		changeColor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		changeColor ();
		Show (Screen.width, Screen.height, GameController.Jeu.Config.Largeur_barre_progression, GameController.Jeu.Config.Hauteur_barre_progression);
	}

	public void changeColor()
	{
		Color c = new Color32(95, 222, 95, 0);

		for (int j=0; j<remplissage.width; j++) {
			for (int i=0; i<remplissage.height; i++)
			{
				if (!remplissage.GetPixel(i,j).Equals(c))
				{
					remplissage.SetPixel(i,j, couleurBarre);
				}

			}
		}

		remplissage.Apply ();
	}

	public void Show(int x,int y,int tailleTextureWidth,int tailleTextureHeight)
	{
		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y-tailleTextureHeight)/2, tailleTextureWidth, tailleTextureHeight), cadre);
		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y-tailleTextureHeight)/2, tailleTextureWidth, tailleTextureHeight), remplissage);
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
