﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement; 

public class UiController : SingletonMonoBehaviourFast<UiController> {
	public GameObject _dialogPanel;
	public GameObject _mapPanel;
	public GameObject _notifyPanel;

	//hungerチェックの時等、dialogPanelのあとチェックが走る場合等に使用。
	public GameObject _dialogPanel2;

	public Text UnusedExpPoint;

	//CharacterPanelのサムネ。（UIの上に来てしまうやつの対処
	public List<GameObject> characterThumbnailList = new List<GameObject>();

	void Start() {
		_dialogPanel.SetActive (false);
		_mapPanel.SetActive (false);

		UpdateExpPointTextUI ();
	}

	public void OnHomeButtonPressed() {
		TransitionManager.Instance.FadeTo ("HomeScene");
	}

	public void OnBattleButtonPressed() {
		if (GameManager.Instance.CheckHungerLevel (true).Count > 0) {
			UiController.Instance.OpenDialogPanel ("There are some hungery characters.\nGoing to Battle will kill them.\nAre you sure?", 
				() => {
					TransitionManager.Instance.FadeTo ("Main");
				},
				() => {}
			);
		} 
		else {
			TransitionManager.Instance.FadeTo ("Main");
		}
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

	public void ForceCloseDialogPanel() {
		_dialogPanel.SetActive (false);
	}

	/**
	 * 
	 * DialogPanel1の後チェック等が走る場合に使用。
	 * 通常はOpenDialogPanelで。
	 * 
	 */
	public void OpenDialogPanel2(string text, DialogPanel.OnButtonAction okAction, DialogPanel.OnButtonAction cancelAction = null) {
		_dialogPanel2.SetActive (true);
		_dialogPanel2.GetComponent<DialogPanel> ().OpenDialogPanel (text, okAction, cancelAction);
	}

	public void ForceCloseDialogPanel2() {
		_dialogPanel2.SetActive (false);
	}



	public void OpenMapPanel() {
		_mapPanel.SetActive (true);
		_mapPanel.GetComponent<MapPanel> ().InitMapPanel ();
	}


	public void UpdateExpPointTextUI() {
		UnusedExpPoint.text = "Unused Exp Point:\n" + PlayerData.unusedExpPoints;
	}

	public GameObject GetCharThumbnail(CharaData charData) {
		string prefabPath = "Prefabs/Characters/Female1";
		if (charData.IsDead) {
			prefabPath = "Prefabs/Effect/Grave";
		}

		GameObject thumbnailObj = (GameObject)Instantiate ((GameObject)Resources.Load (prefabPath)) as GameObject;

		if (!charData.IsDead) {
			ClothingSystem.SetCharThumbnail (charData.ClothDataStr, thumbnailObj);
		}
		//死んでたらsortingLayerをかえるだけ
		else {
			Utils.ChangeSpriteRendererLayer(thumbnailObj, 5, "NpcPos1"); //UI: 5
		}

		return thumbnailObj;
	}

	public void ResetCharThumbnail() {
		foreach (GameObject thumbnail in characterThumbnailList) {
			Destroy (thumbnail);
		}
		characterThumbnailList.Clear ();
	}


	/**
	 * 連打でアイテム作成等でポップアップを表示させたくない場合に使用。
	 * 
	 * 
	 */
	public void NotifyPop(string info, Image img = null) {
		GameObject notifyUiObj = (GameObject)Instantiate((GameObject)Resources.Load ("Prefabs/Ui/NotifyUi")) as GameObject;
		notifyUiObj.GetComponent<NotifyUi> ().Initialize (info, img);

		//スライドさせる

		notifyUiObj.transform.SetParent (_notifyPanel.transform);
		RectTransform notifyTransform = notifyUiObj.GetComponent<RectTransform> ();
		notifyTransform.localPosition = new Vector3 (520.0f, 75.0f, 0.0f);
		notifyTransform.localScale = Vector3.one;

		iTween.MoveTo(notifyUiObj,
			iTween.Hash(
				"position", new Vector3(0.0f, 75.0f, 0.0f), 
				"time", 1.5f, 
				"islocal", true,
				"easetype", iTween.EaseType.easeOutCubic,
				"oncompletetarget", gameObject,
				"oncomplete", "NotifyPopMoveIn",
				"oncompleteparams", notifyUiObj
			));
	}

	private void NotifyPopMoveIn(Object notifyUiObj) {
		Debug.LogError ("NotifyPopFinish: " + notifyUiObj);
		iTween.MoveTo((GameObject)notifyUiObj,
			iTween.Hash(
				"position", new Vector3(-520.0f, 75.0f, 0.0f), 
				"time", 1.5f, 
				"islocal", true,
				"easetype", iTween.EaseType.easeInCubic,
				"oncompletetarget", gameObject,
				"oncomplete", "NotifyPopFinish",
				"oncompleteparams", notifyUiObj
			));
	}

	private void NotifyPopFinish(Object notifyUiObj) {
		Destroy ((GameObject)notifyUiObj);
	}
}