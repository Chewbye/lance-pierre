using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Configuration")]
public class ConfContainer
{
	[XmlArray("ConfEntries"),XmlArrayItem("ConfEntry")]
	public ConfEntry[] ConfEntries;
	
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(ConfContainer));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static ConfContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(ConfContainer));
		UnityEngine.Debug.Log(path);
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ConfContainer;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static ConfContainer LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(ConfContainer));
		return serializer.Deserialize(new StringReader(text)) as ConfContainer;
	}
}