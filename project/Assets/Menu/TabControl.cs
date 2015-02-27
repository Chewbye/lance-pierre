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

	private ArrayList tabs = new ArrayList();
	private ArrayList panels = new ArrayList();

    protected virtual void Start()
    {
		//Boucle de récupération des onglets de l'interface
		foreach (Transform tab in tabContainer.GetComponentsInChildren<Transform>()) {
			Button button = tab.GetComponent<Button>();
			if(button){
				Text t = tab.GetComponentInChildren<Text>();
				button.onClick.AddListener(delegate () { this.tabSelect(t.text); });
				tabs.Add(button);
			}
		}

		//Boucle de récupération des panels de l'interface
		foreach (Transform panel in panelContainer.transform) {
			panels.Add(panel.gameObject);
		}
    }

	/**
	 * Listener lorsqu'un onglet est cliqué
	 */
	public void tabSelect(string text){
		Debug.Log (text);
	}
}
