using UnityEngine;
using System.Collections;
using System;

public class GestionTailleEcran : MonoBehaviour {

	public double hauteurPixelEcran;
	public double largeurPixelEcran;

	public double hauteurCmEcran;
	public double largeurCmEcran;
	public double diagonaleCmEcran;
	public double diagonalePouceEcran;

	public double ratioLargeurEcran;
	public double ratioHauteurEcran;
	public double ratioDiagonaleEcran;
	public double ratioEcran;

	public double surfaceCmEcran;
	public double surfacePouceEcran;

	public double nombrePixel;
	public double dpi;
	public double dpi2;
	public double tailleCmPixel;
	 

	// Use this for initialization
	void Start () 
	{
		hauteurPixelEcran = Screen.height;
		largeurPixelEcran = Screen.width;
		nombrePixel = hauteurPixelEcran * largeurPixelEcran;
		
		double PGCD = PlusGrandDiviseurCommum (hauteurPixelEcran, largeurPixelEcran);
		
		ratioLargeurEcran = largeurPixelEcran / PGCD;
		ratioHauteurEcran = hauteurPixelEcran / PGCD;
		ratioEcran = ratioLargeurEcran / ratioHauteurEcran;
		ratioDiagonaleEcran = Math.Sqrt (Math.Pow (ratioHauteurEcran, 2) + Math.Pow (ratioLargeurEcran, 2));
		
		largeurCmEcran = (diagonaleCmEcran / ratioDiagonaleEcran) * ratioLargeurEcran;
		hauteurCmEcran = (diagonaleCmEcran / ratioDiagonaleEcran) * ratioHauteurEcran;
		
		surfaceCmEcran = largeurCmEcran * hauteurCmEcran;
		surfacePouceEcran = surfaceCmEcran / (Math.Pow (2.54, 2));
		
		dpi2 = nombrePixel / surfacePouceEcran;
		dpi = Math.Sqrt (dpi2);
		
		tailleCmPixel = largeurCmEcran / largeurPixelEcran;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	static double PlusGrandDiviseurCommum(double a, double b)
	{
		return b == 0 ? a : PlusGrandDiviseurCommum(b, a % b);
	}
}
