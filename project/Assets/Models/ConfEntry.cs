using System.Xml;
using System.Xml.Serialization;

public class ConfEntry{
	[XmlAttribute("name")]
	public string Attribut;

	public string Valeur;

	public ConfEntry(){

	}

	public ConfEntry(string attribut, string valeur){
		this.Attribut = attribut;
		this.Valeur = valeur;
	}
}
