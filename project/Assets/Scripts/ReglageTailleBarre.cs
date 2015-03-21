using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReglageTailleBarre : MonoBehaviour {

	public Slider S_Largeur;
	public Slider S_Hauteur;

	protected Texture2D cadre;
	protected Texture2D remplissage;

	// Use this for initialization
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

	void OnGUI()
	{
		Show (Screen.width, Screen.height, (int)S_Largeur.value, (int)S_Hauteur.value);
	}

	public void Show(int x,int y,int tailleTextureWidth,int tailleTextureHeight)
	{
		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y-tailleTextureHeight)/2, tailleTextureWidth, tailleTextureHeight), cadre);
		GUI.DrawTexture (new Rect ((x-tailleTextureWidth)/2, (y-tailleTextureHeight)/2, tailleTextureWidth, tailleTextureHeight), remplissage);
	}

	public void onChangeValueHauteur()
	{
		GameController.Jeu.Config.Hauteur_barre_progression = (int)S_Hauteur.value;
	}

	public void onChangeValueLargeur()
	{
		GameController.Jeu.Config.Largeur_barre_progression = (int)S_Largeur.value;
	}

	public void onClickRetourMenu()
	{
		UnityEngine.Application.LoadLevel ("mainMenu");
	}
}
