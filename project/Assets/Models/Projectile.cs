using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;

public class Projectile{
	private float _Taille;
	
	public float Taille {
		get {
			return _Taille;
		}
		set {
			_Taille = value;
		}
	}	

	private float _Poids;
	public float Poids {
		get {
			return _Poids;
		}
		set {
			_Poids = value;
		}
	}	
	
	public Projectile (){
		_Taille = 1;
		_Poids = 1;
	}
	
	public Projectile (float taille, float poids){
		_Taille = taille;
		_Poids = poids;
	}
	
	public String toString(){
		String res = "";
		res += "Taille: " + _Taille.ToString() + System.Environment.NewLine;
		res += "Poids: " + _Poids.ToString() + System.Environment.NewLine;
		return res;
	}
}

