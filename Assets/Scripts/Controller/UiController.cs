using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement; 

public class UiController : SingletonMonoBehaviourFast<UiController> {
	public GameObject _dialogPanel;
	public GameObject _mapPanel;

	public Text UnusedExpPoint;

	void Start() {
		_dialogPanel.SetActive (false);
		_mapPanel.SetActive (false);

		UnusedExpPoint.text = "Unused Exp Point:\n" + PlayerData.unusedExpPoints;
	}

	public void OnHomeButtonPressed() {
		TransitionManager.Instance.FadeTo ("HomeScene");
	}

	public void OnBattleButtonPressed() {
		TransitionManager.Instance.FadeTo ("Main");
	}

	/**
	 * 
	 * content配下にある全てのTransformを消す。
	 * 
	 * 
	 */
	public void ResetContent(GameObject content) {
		Transform[] itemUIButtonArray = content.GetComponentsInChildren<Transform> ();
		int length = itemUIButtonArray.Length;
		for(int i=0; i<length; i++) {
			Transform itemUIBtn = itemUIButtonArray [i];
			//content自身は消さない
			if (itemUIBtn.name == "Content") {
				continue;
			}

			Destroy (itemUIBtn.gameObject);
		}
		itemUIButtonArray = null;
	}

	public void OpenDialogPanel(string text, DialogPanel.OnButtonAction okAction, DialogPanel.OnButtonAction cancelAction = null) {
		_dialogPanel.SetActive (true);
		_dialogPanel.GetComponent<DialogPanel> ().OpenDialogPanel (text, okAction, cancelAction);
	}

	public void OpenMapPanel() {
		_mapPanel.SetActive (true);
		_mapPanel.GetComponent<MapPanel> ().InitMapPanel ();
	}

}