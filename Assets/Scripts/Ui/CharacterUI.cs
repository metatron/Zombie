using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	public GameObject charImg;
	public Text numText;
	public Text nameText;

	private CharaData _charaData;

	public void InitCharaUI(CharaData charData) {
		_charaData = charData;
		GameManager.Instance.InitCharObject<NpcObject> (charData);
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
