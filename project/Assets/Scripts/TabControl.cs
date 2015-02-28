using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class TabControl : MonoBehaviour
{
    [SerializeField]
    private GameObject panelContainer = null;
    [SerializeField]
    private GameObject tabContainer = null;

	private List<Button> tabs = new List<Button>();
	private List<GameObject> panels = new List<GameObject>();

	private int currentPanel = 0;

    protected virtual void Start(){
		int i = 0;
		//Boucle de récupération des onglets de l'interface
		//foreach (Transform tab in tabContainer.GetComponentsInChildren<Transform>()) {
		foreach (Transform tab in tabContainer.transform) {
			Button button = tab.GetComponent<Button>();
			if(button){
				Text t = tab.GetComponentInChildren<Text>();
				int pos = i;
				button.onClick.AddListener(delegate () { this.tabSelect(pos); });
				tabs.Add(button);
			}
			i++;
		}

		//Boucle de récupération des panels de l'interface
		foreach (Transform panel in panelContainer.transform) {
			panels.Add(panel.gameObject);
		}
    }

	/**
	 * Listener lorsqu'un onglet est cliqué
	 */
	public void tabSelect(int tabPos){
		panels [tabPos].SetActive (true);
		panels [currentPanel].SetActive (false);
		currentPanel = tabPos;
		Debug.Log (tabPos);
	}
}
