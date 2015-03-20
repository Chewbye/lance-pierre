using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuTableManager : MonoBehaviour {

	public GameObject Table_positions_cibles;
	public UnityEngine.GameObject prefabRowTableCible;

	public GameObject Table_tailles_cibles;
	public UnityEngine.GameObject prefabRowTableTaillesCible;

	public GameObject Table_tailles_projectiles;
	public UnityEngine.GameObject prefabRowTableTaillesProjectiles;

	public GameObject textNBLancers;

	// Use this for initialization
	void Start () {
		//Ajout des listeners à chaque champs de la première ligne de la table des positions des cibles
		addListenersToRow(Table_positions_cibles.transform.GetChild(1), onValueChangeTablePositionCible, removeRowTablePositionCible, 0);

		//Ajout des listeners à chaque champs de la première ligne de la table des tailles des cibles
		addListenersToRow(Table_tailles_cibles.transform.GetChild(1), onValueChangeTableTailleCible, removeRowTableTailleCible, 0);

		//Ajout des listeners à chaque champs de la première ligne de la table des tailles des projectiles
		addListenersToRow(Table_tailles_projectiles.transform.GetChild(1), onValueChangeTableTailleProjectile, removeRowTableTailleProjectile, 0);

		//Affichage des valeurs par défaut du fichier de configuration pour le tableau des positions des cibles
		for(int i=0; i<GameController.Jeu.Config.Positions_Cibles.Count; i++){
			setValueAt(Table_positions_cibles, i+1, 1, GameController.Jeu.Config.Positions_Cibles[i].DistanceX);
			setValueAt(Table_positions_cibles, i+1, 2, GameController.Jeu.Config.Positions_Cibles[i].DistanceY);
		}

		//Affichage des valeurs par défaut du fichier de configuration pour le tableau des tailles des cibles
		for(int i=0; i<GameController.Jeu.Config.Tailles_Cibles.Count; i++){
			setValueAt(Table_tailles_cibles, i+1, 1, GameController.Jeu.Config.Tailles_Cibles[i]);
		}

		//Affichage des valeurs par défaut du fichier de configuration pour le tableau des tailles des projectiles
		for(int i=0; i<GameController.Jeu.Config.Projectiles.Count; i++){
			setValueAt(Table_tailles_projectiles, i+1, 1, GameController.Jeu.Config.Projectiles[i].Taille);
			setValueAt(Table_tailles_projectiles, i+1, 2, GameController.Jeu.Config.Projectiles[i].Poids);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	/**
	 * Modifie une valeur d'un champ de type InputField dans le tableau
	 * @param tableau tableau auquel on veut modifier la valeur d'un champ
	 * @param row ligne dans laquelle le champ à modifier est positionné
	 * @param col colonne dans laquelle le champ à modifier est positionné
	 * @param val nouvelle valeur à afficher
	 */
	public void setValueAt(GameObject tableau, int row, int col, float val){
		tableau.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text = val.ToString();
	}

	/**
	 * Modifie la valeur d'un attribut de la liste Table_positions_cibles en fonction du champ modifié dans le GUI
	 * @param row ligne dans laquelle le champ a été modifié
	 * @param col colonne dans laquelle le champ a été modifié
	 */
	public void onValueChangeTablePositionCible(int row, int col){
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

	/**
	 * Modifie la valeur d'un attribut de la liste Table_tailles_cibles en fonction du champ modifié dans le GUI
	 * @param row ligne dans laquelle le champ a été modifié
	 * @param col colonne dans laquelle le champ a été modifié
	 */
	public void onValueChangeTableTailleCible(int row, int col){
		float value;
		if (float.TryParse (Table_tailles_cibles.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text, out value)) {
			GameController.Jeu.Config.Tailles_Cibles[row-1] = value;
		}
		
		Debug.Log (GameController.Jeu.Config);
	}

	/**
	 * Modifie la valeur d'un attribut de la liste Table_tailles_projectiles en fonction du champ modifié dans le GUI
	 * @param row ligne dans laquelle le champ a été modifié
	 * @param col colonne dans laquelle le champ a été modifié
	 */
	public void onValueChangeTableTailleProjectile(int row, int col){
		float value;
		if (float.TryParse (Table_tailles_projectiles.transform.GetChild (row).gameObject.transform.GetChild(col).gameObject.transform.GetChild(0).GetComponent<InputField>().text, out value)) {
			//GameController.Jeu.Config.Tailles_Projectiles[row-1] = value;
			switch (col) {
			case 1:
				GameController.Jeu.Config.Projectiles[row-1].Taille = value;
				break;
			case 2:
				GameController.Jeu.Config.Projectiles[row-1].Poids = value;
				break;
			}
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
		rectTransform.localScale = new Vector3 (1, 1, 1);
		return button;
	}

	/**
	 * Supprime la ligne situé à la position row de la table Table
	 * Supprime l'élément de type T à la liste ModeleList situé à la position row - 1 (décallage car la première ligne de Table est le nom des colonnes)
	 * @param Table tableau auquel on veut ajouter une ligne
	 * @param modele liste auquelle on veut ajouter un élément
	 * @param row indice de la ligne à supprimer dans Table
	 * @param fieldChangeMethod méthode appellée lorsqu'une case du tableau est modifiée
	 * @param removeRowMethod méthode appellée lorsqu'un bouton de suppression d'une ligne est appellée
	 */
	public void removeRowFromTable<T>(ref GameObject Table, List<T> ModeleList, int row,  Action<int, int> fieldChangeMethod, Action<int> removeRowMethod){
		Destroy (Table.transform.GetChild (row).gameObject); //Destruction de la ligne graphiquement

		if(ModeleList.Count > 0)
			ModeleList.RemoveAt(row - 1);

		//Remise à niveau des identifiants de chaque ligne
		for (int i=row; i<Table.transform.childCount-1; i++) {
			int newID = i - 1;
			Table.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = newID.ToString();
			
			addListenersToRow(Table.transform.GetChild(i).gameObject.transform, fieldChangeMethod, removeRowMethod, -1);
		}
		
		//Mise à jour du nombre de lancers
		textNBLancers.GetComponent<Text>().text = GameController.Jeu.Config.updateNB_Lancers ().ToString();
	}

	/**
	 * Détruit une ligne de la table Table_positions_cibles
	 */
	public void removeRowTablePositionCible(int row){
		removeRowFromTable (ref Table_positions_cibles, GameController.Jeu.Config.Positions_Cibles, row, onValueChangeTablePositionCible, removeRowTablePositionCible);
	}


	/**
	 * Détruit une ligne de la table Table_tailles_cibles
	 */
	public void removeRowTableTailleCible(int row){
		removeRowFromTable (ref Table_tailles_cibles, GameController.Jeu.Config.Tailles_Cibles, row, onValueChangeTableTailleCible, removeRowTableTailleCible);
	}

	/**
	 * Détruit une ligne de la table Table_tailles_projectiles
	 */
	public void removeRowTableTailleProjectile(int row){
		removeRowFromTable (ref Table_tailles_projectiles, GameController.Jeu.Config.Projectiles, row, onValueChangeTableTailleProjectile, removeRowTableTailleProjectile);
	}

	/**
	 * Ajoute ou modifie un listener à chaque champ de la ligne row
	 * @row ligne à modifier
	 * @fieldChangeMethod méthode à appeler lors de la modification d'un champ de type texte
	 * @removeRowMethod méthode à appeler pour le dernier champ qui est le bouton de suppression d'une ligne
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
	 * Ajoute une ligne à la table Table
	 * Ajoute un listener à chaque champ de cette ligne
	 * Ajoute un élément de type T à la liste modèle en utilisant le constructeur par défaut de la classe T
	 * @param Table tableau auquel on veut ajouter une ligne
	 * @param modele liste auquelle on veut ajouter un élément
	 * @param prefabRow prefab de la ligne à ajouter au tableau
	 * @param fieldChangeMethod méthode appellée lorsqu'une case du tableau est modifiée
	 * @param removeRowMethod méthode appellée lorsqu'un bouton de suppression d'une ligne est appellée
	 */
	public GameObject addRow<T>(ref GameObject Table, List<T> modele, GameObject prefabRow, Action<int, int> fieldChangeMethod, Action<int> removeRowMethod) where T : new(){
		Transform boutonAjout = Table.transform.GetChild (Table.transform.childCount-1);
		//Récupération dernière ligne du tableau
		Transform lastRow = Table.transform.GetChild (Table.transform.childCount-2);

		//Création de la nouvelle ligne du tableau
		UnityEngine.GameObject newRowTableCibles = CreateRowCible (prefabRow, Table, new Vector2 (0, 0), new Vector2 (0, 0));
		
		//Récupération du numéro de cible précédent
		string lastNumCibleString = lastRow.gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;

		int lastNumCible;
		if (int.TryParse (lastNumCibleString, out lastNumCible)) {
			lastNumCible = (Table.transform.childCount-3) + 1;
			//Modification du numéro de cible de la nouvelle ligne en l'incrémentant
			newRowTableCibles.gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = lastNumCible.ToString();
		}
		
		boutonAjout.SetAsLastSibling (); //Descend le bouton d'ajout à la fin du tableau
		
		//Ajout des listeners à chaque champs
		addListenersToRow(newRowTableCibles.transform, fieldChangeMethod, removeRowMethod, 0);
		
		//Modification du modèle Jeu
		if(modele.Count>0)
		modele.Add (new T ());
		
		//Mise à jour du nombre de lancers
		textNBLancers.GetComponent<Text>().text = GameController.Jeu.Config.updateNB_Lancers ().ToString();

		return newRowTableCibles;
	}

	/** 
	 * Ajoute une ligne à la table des positions des cibles
	 */
	public void onClickAjouterUnePositionCible(){
		addRow<PositionCible> (ref Table_positions_cibles, GameController.Jeu.Config.Positions_Cibles, prefabRowTableCible, onValueChangeTablePositionCible, removeRowTablePositionCible);
	}

	/** 
	 * Ajoute une ligne à la table des tailles des cibles
	 */
	public void onClickAjouterUneTailleCible(){
		addRow<float> (ref Table_tailles_cibles, GameController.Jeu.Config.Tailles_Cibles, prefabRowTableTaillesCible, onValueChangeTableTailleCible, removeRowTableTailleCible);
	}

	/** 
	 * Ajoute une ligne à la table des tailles des cibles
	 */
	public void onClickAjouterUneTailleProjectile(){
		addRow<Projectile> (ref Table_tailles_projectiles, GameController.Jeu.Config.Projectiles, prefabRowTableTaillesProjectiles, onValueChangeTableTailleProjectile, removeRowTableTailleProjectile);
	}


	/**
	 * Créé ou supprime autant de lignes de chaque table pour qu'elles aientt chacune autant de ligne que leur modèle
	 */
	public void initTablesForNewConfig(){

		/** SUPPRESSION DES LIGNES EN TROP DANS CHAQUE TABLE **/
		int cursor = (Table_positions_cibles.transform.childCount - 2);
		int end = (Table_positions_cibles.transform.childCount - 1) - ((Table_positions_cibles.transform.childCount - 1) - GameController.Jeu.Config.Positions_Cibles.Count);
		while (cursor > end) {
			removeRowFromTable (ref Table_positions_cibles, new List<PositionCible> (), cursor, onValueChangeTablePositionCible, removeRowTablePositionCible);
			cursor--;
		}

		cursor = (Table_tailles_cibles.transform.childCount - 2);
		end = (Table_tailles_cibles.transform.childCount - 1) - ((Table_tailles_cibles.transform.childCount - 1) - GameController.Jeu.Config.Tailles_Cibles.Count);
		while (cursor > end) {
			removeRowFromTable (ref Table_tailles_cibles, new List<float>(), cursor, onValueChangeTableTailleCible, removeRowTableTailleCible);
			cursor--;
		}

		cursor = (Table_tailles_projectiles.transform.childCount - 2);
		end = (Table_tailles_projectiles.transform.childCount - 1) - ((Table_tailles_projectiles.transform.childCount - 1) - GameController.Jeu.Config.Projectiles.Count);
		while (cursor > end) {
			removeRowFromTable (ref Table_tailles_projectiles, new List<Projectile>(), cursor, onValueChangeTableTailleProjectile, removeRowTableTailleProjectile);
			cursor--;
		}


		/** AJOUT DES LIGNES MANQUANTES DANS CHAQUE TABLE **/
		int nbRowsToAdd = GameController.Jeu.Config.Positions_Cibles.Count - (Table_positions_cibles.transform.childCount - 2);
		for (int i=0; i<nbRowsToAdd; i++) {
			GameObject newRow = addRow<PositionCible> (ref Table_positions_cibles, new List<PositionCible>(), prefabRowTableCible, onValueChangeTablePositionCible, removeRowTablePositionCible);
		}

		nbRowsToAdd = GameController.Jeu.Config.Tailles_Cibles.Count - (Table_tailles_cibles.transform.childCount - 2);
		for (int i=0; i<nbRowsToAdd; i++) {
			GameObject newRow = addRow<float> (ref Table_tailles_cibles, new List<float>(), prefabRowTableTaillesCible, onValueChangeTableTailleCible, removeRowTableTailleCible);
		}

		nbRowsToAdd = GameController.Jeu.Config.Projectiles.Count - (Table_tailles_projectiles.transform.childCount - 2);
		for (int i=0; i<nbRowsToAdd; i++) {
			GameObject newRow = addRow<Projectile> (ref Table_tailles_projectiles, new List<Projectile>(), prefabRowTableTaillesProjectiles, onValueChangeTableTailleProjectile, removeRowTableTailleProjectile);
		}
	}

	/**
	 * Met à jour les valeurs de toutes les tables à partir de leur modèles
	 */
	public void refreshGUITables(){
		initTablesForNewConfig ();


		//Affichage des valeurs par défaut du fichier de configuration pour le tableau des positions des cibles
		for(int i=0; i<GameController.Jeu.Config.Positions_Cibles.Count; i++){
			setValueAt(Table_positions_cibles, i+1  , 1, GameController.Jeu.Config.Positions_Cibles[i].DistanceX);
			setValueAt(Table_positions_cibles, i+1  , 2, GameController.Jeu.Config.Positions_Cibles[i].DistanceY);
		}

		//Affichage des valeurs par défaut du fichier de configuration pour le tableau des tailles des cibles
		for(int i=0; i<GameController.Jeu.Config.Tailles_Cibles.Count; i++){
			setValueAt(Table_tailles_cibles, i+1  , 1, GameController.Jeu.Config.Tailles_Cibles[i]);
		}

		//Affichage des valeurs par défaut du fichier de configuration pour le tableau des tailles des projectiles
		for(int i=0; i<GameController.Jeu.Config.Projectiles.Count; i++){
			setValueAt(Table_tailles_projectiles, i+1, 1, GameController.Jeu.Config.Projectiles[i].Taille);
			setValueAt(Table_tailles_projectiles, i+1, 2, GameController.Jeu.Config.Projectiles[i].Poids);
		}

	}
}
