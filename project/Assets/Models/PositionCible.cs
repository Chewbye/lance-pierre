using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

public class PositionCible{
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


	public PositionCible (){
		_DistanceX = 0;
		_DistanceY = 0;
	}

	public PositionCible (float distanceX, float distanceY){
		_DistanceX = distanceX;
		_DistanceY = distanceY;
	}

	public String toString(){
		String res = "";
		res += "Distance X: " + _DistanceX.ToString() + System.Environment.NewLine;
		res += "Distance Y: " + _DistanceY.ToString() + System.Environment.NewLine;
		return res;
	}
}

