using UnityEngine;
using System.Collections;

/*
 * ** BarreProgression **
 * 
 * Classe contenant une liste de méthode permettant de gérer la barre de progression
 */

public class BarreProgression{

	protected Texture2D cadre; // texture qui representera le contour de la barre de progression
	protected Texture2D remplissage; // texture qui representera le contenu de la barre de progression
	protected Texture2D affichage; // texture qui representera le contenu de la barre de progression à un instant T
	
	protected Color[,] memoireRemplissage; // tableau de couleur qui contiendra l'état des pixels du contenu à un instant T
	
	protected int sizeX; // largeur de la texture "remplissage" de la barre
	protected int sizeY; // hauteur de la texture "remplissage" de la barre
	
	protected float Min; // valeur minimal de la barre 
	protected float Max; // valeur max de la barre 

	protected float lastValeur; // variable qui contiendra la dernière valeur de la barre enregistré
	protected float valeur; // variable qui contiendra la valeur courante de la barre enregistré

	protected int largeur; // largeur de la barre de progression
	protected int hauteur; // hauteur de la barre de progression


	/*
	 * ** ACCESSEURS en consultations/modifications **
	 */
	public Texture2D Cadre {
		get {
			return this.cadre;
		}
		set {
			cadre = value;
		}
	}

	public Texture2D Remplissage {
		get {
			return this.remplissage;
		}
		set {
			remplissage = value;
		}
	}

	public Texture2D Affichage {
		get {
			return this.affichage;
		}
		set {
			affichage = value;
		}
	}

	public Color[,] MemoireRemplissage {
		get {
			return this.memoireRemplissage;
		}
		set {
			memoireRemplissage = value;
		}
	}

	public int SizeX {
		get {
			return this.sizeX;
		}
		set {
			sizeX = value;
		}
	}

	public int SizeY {
		get {
			return this.sizeY;
		}
		set {
			sizeY = value;
		}
	}

	public float Valeur {
		get {
			return this.valeur;
		}
		set {
			valeur = value;
		}
	}

	public float LastValeur {
		get {
			return this.lastValeur;
		}
		set {
			lastValeur = value;
		}
	}

	public int Largeur {
		get {
			return largeur;
		}
		set {
			largeur = value;
		}
	}

	public int Hauteur {
		get {
			return hauteur;
		}
		set {
			hauteur = value;
		}
	}

	/*
	 * ** CONSTRUCTEUR **
	 */
	public BarreProgression(string nomTextureCadre, string nomTextureRemplissage, float min, float max)
	{
		this.cadre = Resources.Load (nomTextureCadre) as Texture2D; // charge la texture d'une image par avec son nom passé en paramètre
		this.remplissage = Resources.Load (nomTextureRemplissage) as Texture2D; // charge la texture d'une image par avec son nom passé en paramètre

		this.sizeX = this.remplissage.width;
		this.sizeY = this.remplissage.height;

		this.affichage = new Texture2D (this.sizeX, this.sizeY, TextureFormat.ARGB32, false); // création de la texture du contenu de la barre qui changera en fonction du temps

		this.Min = min;
		this.Max = max;

		this.largeur = GameController.Jeu.Config.Largeur_barre_progression;
		this.hauteur = GameController.Jeu.Config.Hauteur_barre_progression;

		this.memoireRemplissage = new Color[this.sizeX, this.sizeY];

		Color c = new Color32(95, 222, 95, 0); // pixel hors contenu de l'image qui représente le "contenu" de la barre

		// mise en mémoire du contenu 
		for (int j=0; j<this.sizeY; j++) {
			for (int i=0; i<this.sizeX; i++)
			{
				if (!remplissage.GetPixel(i,j).Equals(c)) // si on lit un pixel qui est dans le contenu
				{
					this.memoireRemplissage[i,j] = GameController.Jeu.Config.Couleur_barre; // on change le pixel (la couleur) avec celui-ci définit dans la configuration
				}
				else
				{
					this.memoireRemplissage[i,j] = this.remplissage.GetPixel(i,j);
				}
			}
		}

		// on met à jour la structure
		this.Update (true);
	}

	/*
	 * ** Update **
	 * 
	 * met à jour le contenu de la barre à un instant T
	 */ 
	public void Update(bool force)
	{
		// on calcul un ratio qui nous permet de délimiter le contenu de la barre par rapport à la valeur courante
		float ratio = this.valeur / this.Max;
		int pixelValue = (int)(this.sizeX * ratio);

		// On vérifie si oui ou non on arrive à la valeur Max de barre
		if (pixelValue == this.lastValeur && !force) {
			return;
		}

		// mise en mémoire des pixels par rapport à la valeur courante (instant T)
		Color[] block = new Color[this.sizeX * this.sizeY];

		int count = 0;
		for (int j=0; j<this.sizeY; j++) {
			for (int i=0; i<this.sizeX; i++)
			{
				if (i<=pixelValue)
				{
					block[count] = this.memoireRemplissage[i,j];
				}
				else
				{
					block[count] = new Color(0,0,0,0);
				}

				count++;
			}
		}

		// mise à jour graphique de la texture du contenu
		this.affichage.SetPixels(block);
		this.affichage.Apply();

		// on sauvegarde la valeur courante
		this.lastValeur = pixelValue;
	}

	/*
	 * ** Show **
	 * 
	 * Création des représentations graphiques des différentes textures en fonction des coordonnées x et y passées en paramètre
	 */
	public void Show(int x, int y)
	{
		GUI.DrawTexture (new Rect ((x-this.largeur)/2, ((y-this.hauteur)/2) + (y/3) + (y/10), this.largeur, this.hauteur), this.cadre);
		GUI.DrawTexture (new Rect ((x-this.largeur)/2, ((y-this.hauteur)/2) + (y/3) + (y/10), this.largeur, this.hauteur), this.affichage);
	}


}
