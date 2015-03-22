using UnityEngine;
using System.Collections;

public class GestionPostCollision : MonoBehaviour 
{
	public GameObject projectile;

	private float tempsRestant;
	
	void Start () 
	{
		tempsRestant = 2;
	}

	void Update () 
	{
		if(GameController.Jeu.Cible_Touchee)
		{
			renderer.enabled = false;
			projectile.renderer.enabled = false;
			tempsRestant -= Time.deltaTime;
			if (tempsRestant <= 0.0f)
			{
				//On recharge la meme scène
				GameController.Jeu.Cible_Touchee = false;
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if(GameController.Jeu.Cible_Manquee)
		{

		}
	}
}
