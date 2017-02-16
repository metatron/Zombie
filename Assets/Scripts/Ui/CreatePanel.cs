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

		if (_itemData.GetType () == typeof(SwordData)) {
			itemImage.sprite = GameManager.Instance.GetSpriteFromPath ("WeaponAtlas", _itemData.Image);
			//アスペクトをONのまま横サイズをFixさせながらリサイズ。
			itemImage.preserveAspect = true;
			itemImage.SetNativeSize ();
			Vector2 imgSize = itemImage.GetComponent<RectTransform>().sizeDelta;
			imgSize.x = 100;
			itemImage.GetComponent<RectTransform> ().sizeDelta = imgSize;

			Dictionary<AbstractData.DataType, int> requirementDict = ((SwordData)_itemData).ParseRequirementStr ();
			foreach (AbstractData.DataType dataType in requirementDict.Keys) {
				int value = requirementDict [dataType];
				MaterialItem materialItem = (MaterialItem)Instantiate (materialItemPrefab);
				materialItem.materialNum.text = dataType + " x" + value;
				materialItem.transform.SetParent (MaterialListPos);
				materialItem.transform.localPosition = Vector3.zero;
				materialItem.transform.localScale = Vector3.one;
			}
		}
	}

	public void CloseCreatePanel() {
		MaterialItem[] materialObjectArray = MaterialListPos.GetComponentsInChildren<MaterialItem> ();
		int count = materialObjectArray.Length;
		for (int i = 0; i < count; i++) {
			DestroyImmediate (materialObjectArray [i].gameObject);
		}
		itemImage.sprite = null;

		gameObject.SetActive (false);
	}
}
