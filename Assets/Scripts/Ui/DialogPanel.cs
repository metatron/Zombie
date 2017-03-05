using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour {
	public Text dialogText;
	public GameObject cancelBtnObj;

	public delegate void OnButtonAction();
	public OnButtonAction okBtnAction;
	public OnButtonAction cancelBtnAction;

	public void CloseDialogPanel() {
		gameObject.SetActive (false);
	}

	public void OpenDialogPanel(string text, OnButtonAction okAction, OnButtonAction cancelAction = null) {
		dialogText.text = text;

		okBtnAction = okAction;

		//キャンセルボタンが設定されてない場合は非表示
		if (cancelAction == null) {
			cancelBtnObj.SetActive (false);
		} else {
			cancelBtnObj.SetActive (true);
			cancelBtnAction = cancelAction;
		}
	}

	public void OnOKButton() {
		if (okBtnAction != null) {
			okBtnAction ();
		}
		CloseDialogPanel ();
	}

	public void OnCancelButton() {
		if (cancelBtnAction != null) {
			cancelBtnAction ();
		}
		CloseDialogPanel ();
	}

}
