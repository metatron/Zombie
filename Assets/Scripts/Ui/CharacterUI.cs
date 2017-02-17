using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	public GameObject charImg;
	public Text numText;
	public Text nameText;

	private AbstractCharacterObject _charaData;

	public void InitCharaUI(string atlas, AbstractCharacterObject charData) {
		_charaData = charData;
		nameText.text = charData.gameObject.name;

		charImg = charData.gameObject;
	}

	public void SetCharacterImageSize() {
	}

	/**
	 * 
	 * キャラクターの詳細を開き、装備の確認変更、食事をさせます。
	 * 
	 */
	public void OnClickCharUI() {
	}
}
