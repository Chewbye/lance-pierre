using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompteARebours : MonoBehaviour {

	public float tempsRestant = GameController.Jeu.Config.Delai_lancer_projectile; // En seconde
	Text txt;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		tempsRestant -= Time.deltaTime;
		if (tempsRestant <= 0.0f)
		{
			Debug.Log("Temps écoulé !");
			Reset ();
		}
	}

	void Reset () 
	{
		// On indique que le tir courant est manqué
		GameController.Jeu.Reussiste_Tirs.Add(false);
		
		// On incrémente le tir courant
		GameController.Jeu.Tir_courant++;
		
		//	On recharge la meme scène
		Application.LoadLevel (Application.loadedLevel);
	}
}
