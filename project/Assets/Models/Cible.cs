using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;

public class Cible{
	private float _Taille_Cible;
	
	public float Taille_Cible {
		get {
			return _Taille_Cible;
		}
		set {
			_Taille_Cible = value;
		}
	}


	private float _DistanceX;
	
	public float DistanceX {
		get {
			return _DistanceX;
		}
		set {
			_DistanceX = value;
		}
	}

	private float _DistanceY;
	
	public float DistanceY {
		get {
			return _DistanceY;
		}
		set {
			_DistanceY = value;
		}
	}


	public Cible (){
		_Taille_Cible = 1;
		_DistanceX = 1;
		_DistanceY = 1;
	}
}

