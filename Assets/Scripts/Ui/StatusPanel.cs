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
		ResetStatusPanelItems<SwordData>();
	}

	public void CloseStatusPanel() {
		gameObject.SetActive (false);
	}

	public void OnSwordEquip() {
		ResetStatusPanelItems<SwordData>();
	}

	public void OnGunEquip() {
		ResetStatusPanelItems<GunData>();
	}


	private void OnEquiptItem<T>(AbstractData itemData) where T: AbstractData {
		Debug.LogError ("itemData.ID: " + itemData.ID);
		//============= Swordの装備・解除 =============//
		if (typeof(T) == typeof(SwordData)) {
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
						ResetStatusPanelItems<T> ();
					},
				//cancelを押した場合何もなし
					() => {}
				);
			}
			//装備
			else if (PlayerData.GetItemNum (itemData.ID) > 0) {
				UiController.Instance.OpenDialogPanel ("Equip " + itemData.Name + "?", 
				//OKを押した場合、装備
					() => {
						//装備がある場合解除
						if (_charData.SwordID != "") {
							PlayerData.UnequipItem (_charData.SwordID);
						}

						_charData.SwordID = itemData.ID;
						//ストレージから引く
						PlayerData.EquipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
				//cancelを押した場合何もなし
					() => {}
				);
			}
		}

		//============= Gunの装備・解除 =============//
		else if (typeof(T) == typeof(GunData)) {
			//持っていた場合解除
			if (!string.IsNullOrEmpty (_charData.GunID) && itemData.ID == _charData.GunID) {
				//解除しますかDialog
				UiController.Instance.OpenDialogPanel ("UnEquip " + itemData.Name + "?", 
					//OKを押した場合、装備解除
					() => {
						_charData.GunID = "";
						//解除して使用可能にする
						PlayerData.UnequipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
					//cancelを押した場合何もなし
					() => {}
				);
			}
			//装備
			else if (PlayerData.GetItemNum (itemData.ID) > 0) {
				UiController.Instance.OpenDialogPanel ("Equip " + itemData.Name + "?", 
					//OKを押した場合、装備
					() => {
						//装備がある場合解除
						if (_charData.GunID != "") {
							PlayerData.UnequipItem (_charData.GunID);
						}

						_charData.GunID = itemData.ID;
						//ストレージから引く
						PlayerData.EquipItem (itemData.ID);
						//数値が変わっていた場合コンテンツリセット
						ResetStatusPanelItems<T> ();
					},
					//cancelを押した場合何もなし
					() => {}
				);
			}
		}
	}

	private void ResetStatusPanelItems<T>() where T: AbstractData {
		UiController.Instance.ResetContent(content);
		CraftUiController.Instance.InitItemObjButton<T> (content,
			//ボタンを押した場合は装備or解除
			(AbstractData itemData) => {
				OnEquiptItem<T>(itemData);
			}
		);
	}

}
