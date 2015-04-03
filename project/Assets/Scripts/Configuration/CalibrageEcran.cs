using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CalibrageEcran : MonoBehaviour {

	public GameObject projectile;
	Slider tailleSlider;
	double ratioEchelle;

	// Use this for initialization
	void Start () 
	{
		GameObject tempSlider = GameObject.Find("Slider");
		if (tempSlider != null) 
		{
			GameObject balle = GameObject.Find("Balle");
			if (balle != null) 
			{
				tailleSlider = tempSlider.GetComponent<Slider>();
				if (tailleSlider != null)
				{
					ratioEchelle = GameController.Jeu.Config.Ratio_echelle;
					tailleSlider.value = (float) ratioEchelle;
					balle.transform.localScale = new Vector3((float) ratioEchelle, (float)ratioEchelle, (float)ratioEchelle);
					//Debug.Log(ratioEchelle);
				}
				else
				{
					Debug.Log("ERREUR : Aucun SliderComponent trouvé");
				}
			}
			else
			{
				Debug.Log("ERREUR : Balle introuvable");
			}
		} 
		else 
		{
			Debug.Log ("ERREUR : Slider introuvable");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject tempSlider = GameObject.Find("Slider");
		if (tempSlider != null) 
		{
			GameObject balle = GameObject.Find("Balle");
			if (balle != null) 
			{
				tailleSlider = tempSlider.GetComponent<Slider>();
				if (tailleSlider != null)
				{
					ratioEchelle = tailleSlider.value;
					balle.transform.localScale = new Vector3((float) ratioEchelle, (float)ratioEchelle, (float)ratioEchelle);
					GameController.Jeu.Config.Ratio_echelle = ratioEchelle;
					Debug.Log(GameController.Jeu.Config);
					//Debug.Log(ratioEchelle);
				}
				else
				{
					Debug.Log("ERREUR : Aucun SliderComponent trouvé");
				}
			}
			else
			{
				Debug.Log("ERREUR : Balle introuvable");
			}
		} 
		else 
		{
			Debug.Log ("ERREUR : Slider introuvable");
		}
	}

	void OnChangeValue()
	{
	
	}
}
