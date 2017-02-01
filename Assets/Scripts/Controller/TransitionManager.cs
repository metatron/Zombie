using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : SingletonMonoBehaviourFast<TransitionManager> {
	public const float MAX_BLURSIZE 		= 10.0f;
	public const int   MAX_BLURITERATIONS 	= 4;

	private static Vector3 destPosition 		= new Vector3 (-12.0f, 0.0f, -25.0f);
	private static Vector3 originPosition	= new Vector3 (0.0f, 0.0f, -25.0f);

	private string nextSceneName = "";

	public Image transitionPanel;

	void Start() {
		CompletingTransition ();
	}

	public void FadeTo(string sceneName) {
		nextSceneName = sceneName;
		transitionPanel.color = new Color (0.0f, 0.0f, 0.0f, 0.0f);

		//ブラー効果
		iTween.ValueTo(gameObject,
			iTween.Hash(
				"from", 0f, 
				"to", 1.0f, 
				"time", 1.5f, 
				"onupdate", "SetValue",
				"oncomplete", "EffectCompleted"
			));

		//MainCameraスライド
		iTween.MoveTo(Camera.main.gameObject,
			iTween.Hash(
				"position", destPosition, 
				"time", 1.5f, 
				"islocal", false
			));
	}

	private void SetValue(float val) {
		transitionPanel.color = new Color (0.0f, 0.0f, 0.0f, val);
	}

	private void EffectCompleted() {
		SceneManager.LoadScene (nextSceneName);
	}

	/**
	 * 
	 * シーン移動で切り替わったあとの戻り
	 * 
	 * 
	 */
	public void CompletingTransition() {
		//初期化
		Camera.main.transform.position = destPosition;

		//ブラー効果
		iTween.ValueTo(gameObject,
			iTween.Hash(
				"from", 1.0f, 
				"to", 0f, 
				"time", 1.5f, 
				"onupdate", "SetValue",
				"oncomplete", "TransitionCompleted"
			));

		//MainCameraスライド
		iTween.MoveTo(Camera.main.gameObject,
			iTween.Hash(
				"position", originPosition, 
				"time", 1.5f, 
				"islocal", false
			));
	}

	private void TransitionCompleted() {
	}
}
