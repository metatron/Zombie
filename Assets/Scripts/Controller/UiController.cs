using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement; 

public class UiController : SingletonMonoBehaviourFast<UiController> {
	public GameObject _dialogPanel;

	void Start() {
		_dialogPanel.SetActive (false);
	}

	public void OnHomeButtonPressed() {
		TransitionManager.Instance.FadeTo ("HomeScene");
	}

	public void OnBattleButtonPressed() {
		TransitionManager.Instance.FadeTo ("Main");
	}

	public void OpenDialogPanel(string text, DialogPanel.OnButtonAction okAction, DialogPanel.OnButtonAction cancelAction = null) {
		_dialogPanel.SetActive (true);
		_dialogPanel.GetComponent<DialogPanel> ().OpenDialogPanel (text, okAction, cancelAction);
	}


}