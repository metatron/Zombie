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
		_itemData = itemData;

		if (_itemData.GetType () == typeof(SwordData)) {
			Dictionary<AbstractData.DataType, int> requirementDict = ((SwordData)_itemData).ParseRequirementStr ();
			foreach (AbstractData.DataType dataType in requirementDict.Keys) {
				int value = requirementDict [dataType];
				MaterialItem materialItem = (MaterialItem)Instantiate (materialItemPrefab);
				materialItem.materialNum.text = dataType + " x" + value;
				materialItem.image.sprite = GameManager.Instance.GetSpriteFromPath ("WeaponAtlas", itemData.Image);
			}
		}
	}
}
