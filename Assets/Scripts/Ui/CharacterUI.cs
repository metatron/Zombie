using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	public Text numText;
	public Text nameText;

	private CharaData _charaData;

	public void InitCharaUI<T>(CharaData charData) where T: AbstractCharacterObject {
		_charaData = charData;
//		GameManager.Instance.InitCharObject<T> (charData);

		if (string.IsNullOrEmpty (charData.ClothDataStr)) {
			return;
		}

		GameObject thumbnailObj = UiController.Instance.GetCharThumbnail(charData);
		thumbnailObj.transform.SetParent (gameObject.transform);
		thumbnailObj.transform.localPosition = new Vector3(0, -28.0f, 0);
		thumbnailObj.transform.localScale = new Vector3(40.0f, 40.0f, 40.0f);

		//UIがの上にサムネが来てしまうので対処
		UiController.Instance.characterThumbnailList.Add(thumbnailObj);

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

		//開いた際にサムネのレイヤーを変える
		foreach (GameObject thumbnail in UiController.Instance.characterThumbnailList) {
			Utils.ChangeSpriteRendererLayer (thumbnail, 0); //0: Default
		}
	}
}
