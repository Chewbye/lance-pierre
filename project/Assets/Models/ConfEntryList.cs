using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class ConfEntryList<T>: ConfEntry{

	[XmlArray("Valeurs")]
	[XmlArrayItem("Valeur")]
	public List<T> Valeurs;
	
	public ConfEntryList(){
		
	}
	
	public ConfEntryList(string attribut, List<T> valeurs){
		this.Attribut = attribut;
		this.Valeur = null;
		this.Valeurs = valeurs;
	}	
}
