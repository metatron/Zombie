using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	public GameObject charImg;
	public Text numText;
	public Text nameText;

	private CharaData _charaData;

	public void InitCharaUI<T>(CharaData charData) where T: AbstractCharacterObject {
		_charaData = charData;
//		GameManager.Instance.InitCharObject<T> (charData);
	}

	public void SetCharacterImageSize() {
	}

	/**
	 * 
	 * キャラクターの詳細を開き、装備の確認変更、食事。
	 * 
	 */
	public void OnClickCharUI() {
		CraftUiController.Instance.statusPanel.GetComponent<StatusPanel> ().InitCharStatus (_charaData);
	}
}
