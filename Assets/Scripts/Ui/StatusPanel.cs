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
		ResetContent();
		CraftUiController.Instance.InitSwordObjButton (content,
			(AbstractData itemData) => {
				CraftUiController.Instance.createPanel.GetComponent<CreatePanel> ().InitItemCraftingData (itemData);
			}
		);
	}

	public void CloseStatusPanel() {
		gameObject.SetActive (false);
	}


	public void OnEquiptItem() {
	}

	public void OnSwordEquipBtn() {
		
	}

	public void OnGunEquipBtn() {
	}

	public void OnFoodBtn() {
	}

	private void ResetContent() {
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
}
