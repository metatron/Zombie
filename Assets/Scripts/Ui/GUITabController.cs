﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/**
 * 
 * 
 * http://qiita.com/GeneralD/items/91055c502668304a3b4b
 * 
 * 
 * 
 */
[AddComponentMenu("UI/Advanced/Tab Controller")]
public class GUITabController : MonoBehaviour {

	[SerializableAttribute]
	public struct TabContentsPair {
		public Button tab;
		public CanvasRenderer contentBody;

		public Optionals optionals;

		public void SetTabInteractable(bool b) {
			tab.interactable = b;
			if (optionals.selectedTab != null) {
				//タブオブジェクトにButtonがアタッチされてるなら半透明にする
				if (optionals.selectedTab.GetComponent<Button> () != null) {
					optionals.selectedTab.GetComponent<Button> ().interactable = !b;
					tab.gameObject.GetComponent<Button> ().interactable = b;

					//ボタンの子オブジェクトのImageも半透明にする
					float alpha = 1.0f;
					if (!tab.gameObject.GetComponent<Button> ().interactable) {
						alpha = 0.5f;
					}

					UnityEngine.UI.Image[] childImageArray = optionals.selectedTab.GetComponentsInChildren<UnityEngine.UI.Image>();
					foreach (UnityEngine.UI.Image childImage in childImageArray) {
						if (optionals.selectedTab.gameObject == childImage.gameObject) {
							continue;
						}
						childImage.color = new Color (childImage.color.r, childImage.color.g, childImage.color.b, alpha);
					}
				}
				//Buttonがない場合みせなくする
				else {
					optionals.selectedTab.gameObject.SetActive (!b);
					tab.gameObject.SetActive(b);
				}
			}
		}
	}

	[SerializableAttribute]
	public struct Optionals {
		public CanvasRenderer selectedTab;
	}

	public List<TabContentsPair> tabContentsPairs;

	// Use this for initialization
	void Start() {
		if (tabContentsPairs != null) {
			tabContentsPairs.ForEach(pair => {
				// initialize tab state
				pair.SetTabInteractable(!pair.contentBody.gameObject.activeSelf);
				// add click listener
				pair.tab.onClick.AddListener(() => {
					// switch active contents
					tabContentsPairs.ForEach(p => p.contentBody.gameObject.SetActive(false));
					pair.contentBody.gameObject.SetActive(true);
					// switch interactable
					tabContentsPairs.ForEach(p => p.SetTabInteractable(true));
					pair.SetTabInteractable(false);
				});
			});
		}
	}
}