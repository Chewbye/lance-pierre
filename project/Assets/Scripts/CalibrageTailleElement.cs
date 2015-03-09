using UnityEngine;
using System.Collections;

public class CalibrageTailleElement : MonoBehaviour 
{
	public GameObject balle;
	public GameObject cible;

	// Use this for initialization
	void Start () 
	{
		double ratioEchelle = GameController.Jeu.Config.Ratio_echelle;

		CircleCollider2D myCollider = balle.transform.GetComponent<CircleCollider2D>();
		float radiusTemp = myCollider.radius;

		balle.transform.localScale = new Vector3((float) ratioEchelle, (float)ratioEchelle, (float)ratioEchelle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
