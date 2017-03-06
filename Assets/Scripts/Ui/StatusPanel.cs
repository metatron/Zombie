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
		CraftUiController.Instance.ResetContent(content);
		CraftUiController.Instance.InitSwordObjButton (content,
			//ボタンを押した場合は装備or解除
			(AbstractData itemData) => {
				OnEquiptItem(itemData);
			}
		);
	}

	public void CloseStatusPanel() {
		gameObject.SetActive (false);
	}


	private void OnEquiptItem(AbstractData itemData) {
		//持っていた場合解除
		if (!string.IsNullOrEmpty (_charData.SwordID)) {
			UiController.Instance.OpenDialogPanel ("UnEquip " + itemData.Name + "?", 
				() => {
					_charData.SwordID = "";
					//解除して使用可能にする
					PlayerData.UnequipItem (itemData.ID);
				}
			);
		}
		//装備
		else if(PlayerData.GetItemNum(itemData.ID) > 0) {
			UiController.Instance.OpenDialogPanel ("Equip " + itemData.Name + "?", 
				() => {
					_charData.SwordID = itemData.ID;
					//ストレージから引く
					PlayerData.EquipItem(itemData.ID);
				}
			);

		}
	}

	public void OnSwordEquipBtn() {
		
	}

	public void OnGunEquipBtn() {
	}

	public void OnFoodBtn() {
	}

}
