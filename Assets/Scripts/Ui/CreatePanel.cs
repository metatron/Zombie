using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour {
	private AbstractData _itemData;

	public Transform MaterialListPos;
	public Image itemImage;

	public MaterialItem materialItemPrefab;

	public void InitItemCraftingData(AbstractData itemData) {
		gameObject.SetActive (true);
		_itemData = itemData;

		string atlasName = "";
		if (_itemData.GetType () == typeof(SwordData)) {
			atlasName = "WeaponAtlas";
		}

		//アイテムの画像設置
		itemImage.sprite = GameManager.Instance.GetSpriteFromPath (atlasName, _itemData.Image);
		//アスペクトをONのまま横サイズをFixさせながらリサイズ。
		itemImage.preserveAspect = true;
		itemImage.SetNativeSize ();
		Vector2 imgSize = itemImage.GetComponent<RectTransform>().sizeDelta;
		imgSize.x = 100;
		itemImage.GetComponent<RectTransform> ().sizeDelta = imgSize;

		//素材設置<ID, 数>
		Dictionary<string, int> requirementDict = _itemData.ParseRequirementStr ();
		foreach (string id in requirementDict.Keys) {
			int value = requirementDict [id];
			MaterialItem materialItem = (MaterialItem)Instantiate (materialItemPrefab);
			materialItem.materialNum.text = id + " x" + value;
			materialItem.transform.SetParent (MaterialListPos);
			materialItem.transform.localPosition = Vector3.zero;
			materialItem.transform.localScale = Vector3.one;
		}
	}

	public void CloseCreatePanel() {
		MaterialItem[] materialObjectArray = MaterialListPos.GetComponentsInChildren<MaterialItem> ();
		int count = materialObjectArray.Length;
		for (int i = 0; i < count; i++) {
			DestroyImmediate (materialObjectArray [i].gameObject);
		}
		itemImage.sprite = null;

		//数値更新
		CraftUiController.Instance.ResetAllContent();

		gameObject.SetActive (false);
	}


	public void OnCreateItem() {
		//作成に必要なもの<ID, 数>
		Dictionary<string, int> requirementDict = _itemData.ParseRequirementStr ();

		//作れるかチェック
		bool canCreate = true;
		foreach (string reqDataId in requirementDict.Keys) {
			//自分の持ってる数
			int numOwn = PlayerData.GetItemNum(reqDataId);
			if (numOwn >= requirementDict [reqDataId]) {
				canCreate &= true;
			}
			//数が足りないのでbreak;
			else {
				canCreate = false;
			}
		}

		//特定のアイテムの作成条件をチェック
		canCreate = this.canCreateItem();

		//作れるなら素材消費
		if (canCreate) {
			foreach (string reqDataId in requirementDict.Keys) {
				//自分の持ってる数
				PlayerData.UseItem(reqDataId, requirementDict [reqDataId]);
			}

			//PlayerDataに追加
			PlayerData.AddItem(_itemData);
		}//if
	}


	/**
	 * 
	 * 特定のアイテムだけ作成を制限する。
	 * 例えばWallは5つまでしか持てない。
	 * 
	 */
	private bool canCreateItem() {
		//Wallは5つしか持てない
		if (_itemData.ID.ToLower() == "wall") {
			if (PlayerData.GetItemNum("wall") < 5) {
				return true;
			} else {
				return false;
			}
		}

		//通常は作成可能
		return true;
	}
}
