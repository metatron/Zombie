using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour {
	private AbstractCharacterObject _charData;

	public Transform MaterialListPos;
	public Image itemImage;

	public void InitCharStatus(AbstractCharacterObject charData) {
		gameObject.SetActive (true);
		_charData = charData;

		//プレイヤーの場合はPlayerObjectをパネルに登録

		//素材設置
	}

	public void CloseStatusPanel() {
		gameObject.SetActive (false);
	}


	public void OnEquiptItem() {
	}
}
