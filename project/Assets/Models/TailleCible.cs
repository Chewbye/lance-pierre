using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;

public class TailleCible{
	private float _Taille;
	
	public float Taille {
		get {
			return _Taille;
		}
		set {
			_Taille = value;
		}
	}	
	
	public TailleCible (){
		_Taille = 1;
	}
	
	public TailleCible (float taille){
		_Taille = taille;
	}
	
	public String toString(){
		String res = "";
		res += "Taille: " + _Taille.ToString() + System.Environment.NewLine;
		return res;
	}
}

