using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour {
	private CharaData _charData;

	public Transform MaterialListPos;
	public Image ItemImage;

	//ボタンによってContentの中身を変える。ディフォルトはSword
	public GameObject content;

	public void InitCharStatus(CharaData charData) {
		gameObject.SetActive (true);
		_charData = charData;

		//プレイヤーの場合はPlayerObjectをパネルに登録

		//素材設置
		ResetStatusPanelItems();
	}

	public void CloseStatusPanel() {
		gameObject.SetActive (false);
	}


	private void OnEquiptItem(AbstractData itemData) {
		Debug.LogError ("_charData.SwordID: " + _charData.SwordID + ", itemData.ID: " + itemData.ID);
		//持っていた場合解除
		if (!string.IsNullOrEmpty (_charData.SwordID) && itemData.ID == _charData.SwordID) {
			//解除しますかDialog
			UiController.Instance.OpenDialogPanel ("UnEquip " + itemData.Name + "?", 
				//OKを押した場合、装備解除
				() => {
					_charData.SwordID = "";
					//解除して使用可能にする
					PlayerData.UnequipItem (itemData.ID);
					//数値が変わっていた場合コンテンツリセット
					ResetStatusPanelItems();
				},
				//cancelを押した場合何もなし
				() => {}
			);
		}
		//装備
		else if(PlayerData.GetItemNum(itemData.ID) > 0) {
			UiController.Instance.OpenDialogPanel ("Equip " + itemData.Name + "?", 
				//OKを押した場合、装備
				() => {
					_charData.SwordID = itemData.ID;
					//ストレージから引く
					PlayerData.EquipItem (itemData.ID);
					//数値が変わっていた場合コンテンツリセット
					ResetStatusPanelItems();
				},
				//cancelを押した場合何もなし
				() => {}
			);
		}
	}

	private void ResetStatusPanelItems() {
		CraftUiController.Instance.ResetContent(content);
		CraftUiController.Instance.InitItemObjButton<SwordData> (content,
			//ボタンを押した場合は装備or解除
			(AbstractData itemData) => {
				OnEquiptItem(itemData);
			}
		);
	}

}
