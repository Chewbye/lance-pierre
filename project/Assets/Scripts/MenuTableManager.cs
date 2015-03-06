﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuTableManager : MonoBehaviour {

	public GameObject Table_positions_cibles;
	public UnityEngine.GameObject prefabRowTableCible;

	public GameObject Table_tailles_cibles;
	public UnityEngine.GameObject prefabRowTableTaillesCible;

	// Use this for initialization
	void Start () {
		//Ajout des listeners à chaque champs de la première ligne de la table des positions
		addListenersToRow(Table_positions_cibles.transform.GetChild(1), onValueChangeTablePositionCible, removeRowTablePositionCible, 0);

		//Ajout des listeners à chaque champs de la première ligne de la table des tailles
		addListenersToRow(Table_tailles_cibles.transform.GetChild(1), onValueChangeTableTailleCible, removeRowTableTailleCible, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onValueChangeTablePositionCible(int row, int col){
		Debug.Log (row + " " + col);
		Debug.Log (Table_positions_cibles.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text);
		float value;
		if (float.TryParse (Table_positions_cibles.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text, out value)) {
			switch (col) {
			case 1:
				GameController.Jeu.Config.Positions_Cibles[row-1].DistanceX = value;
				break;
			case 2:
				GameController.Jeu.Config.Positions_Cibles[row-1].DistanceY = value;
				break;
			}
		}

		Debug.Log (GameController.Jeu.Config);
	}

	public void onValueChangeTableTailleCible(int row, int col){
		Debug.Log (row + " " + col);
		Debug.Log (Table_tailles_cibles.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text);

		float value;
		if (float.TryParse (Table_tailles_cibles.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text, out value)) {
			GameController.Jeu.Config.Tailles_Cibles[row-1].Taille = value;
		}
		
		Debug.Log (GameController.Jeu.Config);
	}

	/** 
	 * Créé une ligne pouvant etre ajoutée dans le tableau des cibles 
	 * @return la ligne pouvant etre ajoutée dans le tableau des cibles 
	 **/
	public static UnityEngine.GameObject CreateRowCible(UnityEngine.GameObject prefabRowTableCible, GameObject canvas, Vector2 cornerTopRight, Vector2 cornerBottomLeft)
	{
		var button = UnityEngine.GameObject.Instantiate(prefabRowTableCible, Vector3.zero, Quaternion.identity) as UnityEngine.GameObject;
		var rectTransform = button.GetComponent<RectTransform>();
		rectTransform.SetParent(canvas.transform);
		rectTransform.anchorMax = cornerTopRight;
		rectTransform.anchorMin = cornerBottomLeft;
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
		return button;
	}

	/**
	 * Détruit une ligne de la table Table_cibles
	 */
	public void removeRowTablePositionCible(int row){
		Debug.Log (row);
		Destroy (Table_positions_cibles.transform.GetChild (row).gameObject); //Destruction de la ligne graphiquement
		GameController.Jeu.Config.Positions_Cibles.RemoveAt(row - 1);
		//Remise à niveau des identifiants de chaque ligne
		for (int i=row; i<Table_positions_cibles.transform.childCount-1; i++) {
			int newID = i - 2;
			//Debug.Log(newID + " " + Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text);
			Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = newID.ToString();

			addListenersToRow(Table_positions_cibles.transform.GetChild(i).gameObject.transform, onValueChangeTablePositionCible, removeRowTablePositionCible, -1);



			/*
			//Modification du listener des champs des lignes
			for (int j=1; j<Table_positions_cibles.transform.GetChild(j).gameObject.transform.childCount - 1; j++) {
				int colnum = j;
				int rownum = i - 1;
				InputField.OnChangeEvent submitEvent = new InputField.OnChangeEvent ();
				submitEvent.AddListener (delegate {
					onValueChangeTablePositionCible (rownum, colnum);
				});
				Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild (j).gameObject.transform.GetChild (0).GetComponent<InputField> ().onValueChange = submitEvent; 
			}
			
			//Modification du listener du bouton de suppression d'une ligne
			int rownumButton = i - 1;
			UnityEngine.UI.Button.ButtonClickedEvent onclickEvent = new UnityEngine.UI.Button.ButtonClickedEvent ();
			onclickEvent.AddListener (delegate {
				removeRowTablePositionCible (rownumButton);
			});
			Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild (Table_positions_cibles.transform.GetChild(i).gameObject.transform.childCount - 1).gameObject.transform.GetChild (0).GetComponent<UnityEngine.UI.Button> ().onClick = onclickEvent; 
			*/
		}

		//Mise à jour du nombre de lancers
		GameController.Jeu.Config.updateNB_Lancers ();
	}


	/**
	 * Détruit une ligne de la table Table_cibles
	 */
	public void removeRowTableTailleCible(int row){
		Debug.Log (row);
		Destroy (Table_tailles_cibles.transform.GetChild (row).gameObject); //Destruction de la ligne graphiquement
		GameController.Jeu.Config.Tailles_Cibles.RemoveAt(row - 1);
		//Remise à niveau des identifiants de chaque ligne
		for (int i=row; i<Table_tailles_cibles.transform.childCount-1; i++) {
			int newID = i - 2;
			//Debug.Log(newID + " " + Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text);
			Table_tailles_cibles.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = newID.ToString();
			
			addListenersToRow(Table_tailles_cibles.transform.GetChild(i).gameObject.transform, onValueChangeTableTailleCible, removeRowTableTailleCible, -1);
			
			
			
			/*
			//Modification du listener des champs des lignes
			for (int j=1; j<Table_positions_cibles.transform.GetChild(j).gameObject.transform.childCount - 1; j++) {
				int colnum = j;
				int rownum = i - 1;
				InputField.OnChangeEvent submitEvent = new InputField.OnChangeEvent ();
				submitEvent.AddListener (delegate {
					onValueChangeTablePositionCible (rownum, colnum);
				});
				Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild (j).gameObject.transform.GetChild (0).GetComponent<InputField> ().onValueChange = submitEvent; 
			}
			
			//Modification du listener du bouton de suppression d'une ligne
			int rownumButton = i - 1;
			UnityEngine.UI.Button.ButtonClickedEvent onclickEvent = new UnityEngine.UI.Button.ButtonClickedEvent ();
			onclickEvent.AddListener (delegate {
				removeRowTablePositionCible (rownumButton);
			});
			Table_positions_cibles.transform.GetChild(i).gameObject.transform.GetChild (Table_positions_cibles.transform.GetChild(i).gameObject.transform.childCount - 1).gameObject.transform.GetChild (0).GetComponent<UnityEngine.UI.Button> ().onClick = onclickEvent; 
			*/
		}

		//Mise à jour du nombre de lancers
		GameController.Jeu.Config.updateNB_Lancers ();
	}

	/**
	 * Ajoute ou modifie un listener à chaque champ de la ligne row
	 * @row ligne à modifier
	 * @fieldChangeMethod méthode à appeler lors de la modification d'un champ de type texte
	 * @removeRowMethod à appeler pour le dernier champ qui est le bouton de suppression d'une ligne
	 * @decallage mettre cette valeur valeur à -1 si cette méthode est appelée depuis la méthode de suppression d'une ligne (décallant les indexs de la table de 1)
	 */
	public void addListenersToRow(Transform row, Action<int, int> fieldChangeMethod, Action<int> removeRowMethod, int decallage){
		// Ajout du listener à chaque champs de type texte de la nouvelle ligne
		for (int i=1; i<row.gameObject.transform.childCount - 1; i++) {
			int colnum = i;
			int rownum = row.GetSiblingIndex() + decallage;
			Debug.Log(rownum + " " + row.GetSiblingIndex());
			InputField.OnChangeEvent submitEvent = new InputField.OnChangeEvent ();
			submitEvent.AddListener (delegate {
				fieldChangeMethod (rownum, colnum);
			});
			row.gameObject.transform.GetChild (i).gameObject.transform.GetChild (0).GetComponent<InputField> ().onValueChange = submitEvent; 
		}
		
		//Ajout du listener du bouton de suppression d'une ligne
		int rownumButton = row.GetSiblingIndex() + decallage;
		UnityEngine.UI.Button.ButtonClickedEvent onclickEvent = new UnityEngine.UI.Button.ButtonClickedEvent ();
		onclickEvent.AddListener (delegate {
			removeRowMethod (rownumButton);
		});
		row.gameObject.transform.GetChild (row.gameObject.transform.childCount - 1).gameObject.transform.GetChild (0).GetComponent<UnityEngine.UI.Button> ().onClick = onclickEvent; 
	}

	/** 
	 * Ajoute une ligne à la table des positions des cibles
	 */
	public void onClickAjouterUnePositionCible(){
		Transform boutonAjout = Table_positions_cibles.transform.GetChild (Table_positions_cibles.transform.childCount-1);
		//Récupération dernière ligne du tableau
		Transform lastRow = Table_positions_cibles.transform.GetChild (Table_positions_cibles.transform.childCount-2);
		
		//Création de la nouvelle ligne du tableau
		UnityEngine.GameObject newRowTableCibles = CreateRowCible (prefabRowTableCible, Table_positions_cibles, new Vector2 (0, 0), new Vector2 (0, 0));
		
		//Récupération du numéro de cible précédent
		string lastNumCibleString = lastRow.gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
		
		int lastNumCible;
		if (int.TryParse (lastNumCibleString, out lastNumCible)) {
			lastNumCible ++;
			//Modification du numéro de cible de la nouvelle ligne en l'incrémentant
			newRowTableCibles.gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = lastNumCible.ToString();
		}
				
		boutonAjout.SetAsLastSibling (); //Descend le bouton d'ajout à la fin du tableau

		//Ajout des listeners à chaque champs
		addListenersToRow(newRowTableCibles.transform, onValueChangeTablePositionCible, removeRowTablePositionCible, 0);

		//Modification du modèle Jeu
		GameController.Jeu.Config.Positions_Cibles.Add (new PositionCible (0, 0));

		//Mise à jour du nombre de lancers
		GameController.Jeu.Config.updateNB_Lancers ();
	}

	/** 
	 * Ajoute une ligne à la table des tailles des cibles
	 */
	public void onClickAjouterUneTailleCible(){
		Transform boutonAjout = Table_tailles_cibles.transform.GetChild (Table_tailles_cibles.transform.childCount-1);

		//Récupération dernière ligne du tableau
		Transform lastRow = Table_tailles_cibles.transform.GetChild (Table_tailles_cibles.transform.childCount-2);
		
		//Création de la nouvelle ligne du tableau
		UnityEngine.GameObject newRowTableCibles = CreateRowCible (prefabRowTableTaillesCible, Table_tailles_cibles, new Vector2 (0, 0), new Vector2 (0, 0));
		
		//Récupération du numéro de cible précédent
		string lastNumCibleString = lastRow.gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
		
		int lastNumCible;
		if (int.TryParse (lastNumCibleString, out lastNumCible)) {
			lastNumCible ++;
			//Modification du numéro de cible de la nouvelle ligne en l'incrémentant
			newRowTableCibles.gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = lastNumCible.ToString();
		}
		
		boutonAjout.SetAsLastSibling (); //Descend le bouton d'ajout à la fin du tableau
		
		//Ajout des listeners à chaque champs
		addListenersToRow(newRowTableCibles.transform, onValueChangeTableTailleCible, removeRowTableTailleCible, 0);
		
		//Modification du modèle Jeu
		GameController.Jeu.Config.Tailles_Cibles.Add (new TailleCible (0));

		//Mise à jour du nombre de lancers
		GameController.Jeu.Config.updateNB_Lancers ();
	}
}
