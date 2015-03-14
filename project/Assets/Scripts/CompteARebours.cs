using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompteARebours : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		GameController.Jeu.Temps_Restant_Courant = GameController.Jeu.Config.Delai_lancer_projectile;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameController.Jeu.Temps_Restant_Courant -= Time.deltaTime;
		if (GameController.Jeu.Temps_Restant_Courant <= 0.0f)
		{
			Debug.Log("Temps écoulé !");
			Reset ();
		}
	}

	void Reset () 
	{
		// On indique que le tir courant est manqué
		GameController.Jeu.Reussiste_Tirs.Add(false);

		// On indique le temps mis par le joueur pour tirer (ici -1 car le joueur n'a pas tiré)
		GameController.Jeu.Temps_Mis_Pour_Tirer.Add(-1);

		// On incrémente le tir courant
		GameController.Jeu.Tir_courant++;
		
		//	On recharge la meme scène
		Application.LoadLevel (Application.loadedLevel);
	}
}
