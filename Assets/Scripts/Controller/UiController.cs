using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class UiController : SingletonMonoBehaviourFast<UiController> {
	public Camera uiCamera;

	public void FadeToHouse() {
		Debug.LogError ("*******1");
		//Cameraのトランジション設定
		Camera.main.GetComponent<BlurOptimized>().enabled = true;

		iTween.ValueTo(gameObject,
			iTween.Hash(
				"from", 0f, 
				"to", 10f, 
				"time", 1.5f, 
				"onupdate", "SetValue",
				"oncomplete", "EffectCompleted"
			));

	}

	private void SetValue(float val) {
		Camera.main.GetComponent<BlurOptimized> ().blurSize = val;
	}

	private void EffectCompleted() {
	}

}