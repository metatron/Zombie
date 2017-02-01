using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement; 

public class UiController : SingletonMonoBehaviourFast<UiController> {

	public void OnHomeButtonPressed() {
		TransitionManager.Instance.FadeTo ("HomeScene");
	}

	public void OnBattleButtonPressed() {
		TransitionManager.Instance.FadeTo ("Main");
	}
}