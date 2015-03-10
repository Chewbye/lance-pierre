using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;

public class TripletTirs
{
	private Projectile _Projectile;
	public Projectile Projectile 
	{
		get {
			return _Projectile;
		}
		set {
			_Projectile = value;
		}
	}
	
	private PositionCible _Position_Cible;
	public PositionCible Position_Cible 
	{
		get {
			return _Position_Cible;
		}
		set {
			_Position_Cible = value;
		}
	}

	private float _Taille_Cible;
	public float Taille_Cible 
	{
		get {
			return _Taille_Cible;
		}
		set {
			_Taille_Cible = value;
		}
	}

	public TripletTirs ()
	{
		_Projectile = null;
		_Position_Cible = null;
		_Taille_Cible = 0;
	}

	public TripletTirs (Projectile proj, PositionCible pos, float tailleCible)
	{
		_Projectile = proj;
		_Position_Cible = pos;
		_Taille_Cible = tailleCible;
	}

	public String toString(){
		String res = "";
		res += "_Projectile X: " + _Projectile.ToString() + System.Environment.NewLine;
		res += "_Position_Cible Y: " + _Position_Cible.ToString() + System.Environment.NewLine;
		res += "_Taille_Cible Y: " + _Taille_Cible + System.Environment.NewLine;
		return res;
	}
}

