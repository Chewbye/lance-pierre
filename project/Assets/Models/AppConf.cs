using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

public class AppConf{
	private string _LastConfName;
	public string LastConfName {
		get {
			return _LastConfName;
		}
		set {
			_LastConfName = value;
		}
	}

	public AppConf(){
		_LastConfName = "";
	}

	/* Sauvegarde la configuration du jeu actuel dans le fichier path */
	public void saveConfig(string path){
		XmlSerializer xs = new XmlSerializer(typeof(AppConf));
		using (StreamWriter wr = new StreamWriter(path))
		{
			xs.Serialize(wr, this);
		}
	}
}