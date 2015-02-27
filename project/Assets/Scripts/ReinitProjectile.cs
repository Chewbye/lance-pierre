using UnityEngine;
using System.Collections;

public class ReinitProjectile : MonoBehaviour {

	public Rigidbody2D projectile;			//	The rigidbody of the projectile
	public float resetSpeed = 0.025f;		//	The angular velocity threshold of the projectile, below which our game will reset
	
	private float resetSpeedSqr;			//	The square value of Reset Speed, for efficient calculation
	private SpringJoint2D spring;			//	The SpringJoint2D component which is destroyed when the projectile is launched
	
	void Start ()
	{
		//	Calculate the Resset Speed Squared from the Reset Speed
		resetSpeedSqr = resetSpeed * resetSpeed;

		//	Get the SpringJoint2D component through our reference to the GameObject's Rigidbody
		spring = projectile.GetComponent <SpringJoint2D>();
	}
	
	void Update () {
		if (GameController.Jeu.Tir_courant == GameController.Jeu.Config.Nb_lancers) {
			Application.LoadLevel ("finDeTest");
		} else {
			//	If we hold down the "R" key...
			if (Input.GetKeyDown (KeyCode.R)) {
				//	... call the Reset() function
				Reset ();
			}

			//	If the spring had been destroyed (indicating we have launched the projectile) and our projectile's velocity is below the threshold...
			if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr) {
				//	... call the Reset() function
				Reset ();
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		//	If the projectile leaves the Collider2D boundary...
		if (other.rigidbody2D == projectile) {
			//	... call the Reset() function
			Reset ();
		}
	}
	
	void Reset () {
		// On incrémente le nombre de cibles manquées
		GameController.Jeu.Nb_cible_manquees ++;

		// On indique que le tir courant est manqué
		GameController.Jeu._Un_tir[GameController.Jeu.Tir_courant] = false;

		// On incrémente le tir courant
		GameController.Jeu.Tir_courant++;

		Debug.Log(GameController.Jeu);

		//	On recharge la meme scène
		Application.LoadLevel (Application.loadedLevel);
	}
}
