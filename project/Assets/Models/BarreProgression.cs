using UnityEngine;
using System.Collections;

public class BarreProgression{

	protected Texture2D cadre;
	protected Texture2D remplissage;
	protected Texture2D affichage;
	
	protected Color[,] memoireRemplissage;
	
	protected int sizeX;
	protected int sizeY;
	
	protected float Min;
	protected float Max;

	protected float lastValeur;
	protected float valeur;

	protected int largeur;
	protected int hauteur;

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

	public BarreProgression(string nomTextureCadre, string nomTextureRemplissage, float min, float max)
	{
		this.cadre = Resources.Load (nomTextureCadre) as Texture2D;
		this.remplissage = Resources.Load (nomTextureRemplissage) as Texture2D;

		this.sizeX = this.remplissage.width;
		this.sizeY = this.remplissage.height;

		this.affichage = new Texture2D (this.sizeX, this.sizeY, TextureFormat.ARGB32, false);

		this.Min = min;
		this.Max = max;

		this.largeur = GameController.Jeu.Config.Largeur_barre_progression;
		this.hauteur = GameController.Jeu.Config.Hauteur_barre_progression;

		this.memoireRemplissage = new Color[this.sizeX, this.sizeY];

		for (int j=0; j<this.sizeY; j++) {
			for (int i=0; i<this.sizeX; i++)
			{
				this.memoireRemplissage[i,j] = this.remplissage.GetPixel(i,j);
			}
		}

		this.Update (true);
	}

	public void Update(bool force)
	{
		float ratio = this.valeur / this.Max;
		int pixelValue = (int)(this.sizeX * ratio);

		if (pixelValue == this.lastValeur && !force) {
			return;
		}

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

		this.affichage.SetPixels(block);
		this.affichage.Apply();

		this.lastValeur = pixelValue;
	}

	public void Show(int x, int y)
	{
		GUI.DrawTexture (new Rect ((x-this.largeur)/2, (y-this.hauteur)/2, this.largeur, this.hauteur), this.cadre);
		GUI.DrawTexture (new Rect ((x-this.largeur)/2, (y-this.hauteur)/2, this.largeur, this.hauteur), this.affichage);
	}


}
