using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReglageCouleurBarre : MonoBehaviour {

	public Slider S_R;
	public Slider S_G;
	public Slider S_B;
	public Slider S_A;

	public Text Text_R;
	public Text Text_G;
	public Text Text_B;
	public Text Text_A;

	public Texture2D pickColor;
	public Texture2D pickAlpha;

	public Color couleurBarre;
	public Color couleurBarreDefault;

	public Texture2D cadre;
	public Texture2D remplissage;
	public Texture2D textureDeFond;

	public int posX;
	public int posY;
	public int largeur;
	public int hauteurPickColor;
	public int hauteurPickAlpha;

	// Use this for initialization
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
		posY = 10;
		largeur = pickColor.width;
		hauteurPickColor = pickColor.height;
		hauteurPickAlpha = pickAlpha.height;


		changeColor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		changeColor ();
		Show (Screen.width, Screen.height, Screen.width, Screen.height/2);

		GUI.DrawTexture (new Rect (posX, posY, largeur, hauteurPickColor), pickColor);
		GUI.DrawTexture (new Rect (posX, posY + hauteurPickColor, largeur, hauteurPickAlpha), pickAlpha);

		Event e = Event.current;
		bool isLeftMBtnClicked = e.type == EventType.mouseUp;

		if (isLeftMBtnClicked) {
			Vector2 v = e.mousePosition;

			int vx = (int)v.x;
			int vy = (int)v.y;

			if (clickOnPickAlpha(v))
			{
				Color couleurAlpha = pickAlpha.GetPixel(vx - posX,hauteurPickAlpha - (vy - (posY + hauteurPickColor)));

				Debug.Log("clickOnPickAlpha - " + v.ToString() + " " + couleurAlpha.ToString());
	
				float codeCorrespondant = couleurAlpha.r * 255f;

				S_A.value = (int) Math.Round((double)codeCorrespondant);
			}
			else if (clickOnPickColor(v))
			{
				Color couleurColor = pickColor.GetPixel(vx - posX,hauteurPickColor - (vy - posY));

				Debug.Log("clickOnPickColor - " + v.ToString() + " " + couleurColor.ToString() + " - " + (vx - posX) + " " + (vy - posY));

				float codeCorrespondantR = couleurColor.r * 255f;
				float codeCorrespondantG = couleurColor.g * 255f;
				float codeCorrespondantB = couleurColor.b * 255f;

				//Debug.Log(codeCorrespondantR.ToString() + " " + codeCorrespondantG.ToString() + " " + codeCorrespondantB.ToString());
				
				S_R.value = (int) Math.Round((double)codeCorrespondantR);
				S_G.value = (int) Math.Round((double)codeCorrespondantG);
				S_B.value = (int) Math.Round((double)codeCorrespondantB);
			}
			else{
				Debug.Log("clockOn Other - " + v.ToString());
			}

			//Debug.Log(v.ToString());
		}
	}

	public void changeColor()
	{

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
	}

	public Boolean clickOnPickColor(Vector2 v)
	{
		return ((v.x >= posX && v.x <= posX + largeur) && (v.y >= posY && v.y <= posY + hauteurPickColor)); 
	}

	public Boolean clickOnPickAlpha(Vector2 v)
	{
		return ((v.x >= posX && v.x <= posX + largeur) && (v.y >= posY + hauteurPickColor && v.y <= posY + hauteurPickColor + hauteurPickAlpha)); 
	}

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
