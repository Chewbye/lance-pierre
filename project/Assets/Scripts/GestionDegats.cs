using UnityEngine;
using System.Collections;

public class GestionDegats : MonoBehaviour {

	public int hitPoints = 2;					//	The amount of damage our target can take
	public Sprite damagedSprite;				//	The reference to our "damaged" sprite
	public float damageImpactSpeed;				//	The speed threshold of colliding objects before the target takes damage
	
	
	private int currentHitPoints;				//	The current amount of health our target has taken
	private float damageImpactSpeedSqr;			//	The square value of Damage Impact Speed, for efficient calculation
	private SpriteRenderer spriteRenderer;		//	The reference to this GameObject's sprite renderer
	
	void Start () {
		//	Get the SpriteRenderer component for the GameObject's Rigidbody
		spriteRenderer = GetComponent <SpriteRenderer> ();

		//	Initialize the Hit Points
		currentHitPoints = hitPoints;

		//	Calculate the Damage Impact Speed Squared from the Damage Impact Speed
		damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
	}
	
	void OnCollisionEnter2D (Collision2D collision) 
	{
		// On indique que le tir courant est réussi
		GameController.Jeu.Reussiste_Tirs.Add(true);

		// On incrémente le tir courant
		GameController.Jeu.Tir_courant++;

		Debug.Log(GameController.Jeu);

		//On recharge la meme scène
		Application.LoadLevel (Application.loadedLevel);
	}
}
