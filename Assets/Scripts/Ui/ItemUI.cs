using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	public Image itemImg;
	public Text numText;
	public Text nameText;

	private AbstractData _itemData;

	public void InitItemMenu(string atlas, AbstractData itemData, int numOwned) {
		_itemData = itemData;
		nameText.text = _itemData.Image;
		numText.text = "" + numOwned;

		itemImg.sprite = GameManager.Instance.GetSpriteFromPath (atlas, itemData.Image);
	}

	public void SetItemImageSize() {
		//なぜかこれを最初しとかないと修正されない。
		this.itemImg.SetNativeSize ();
		//preserveAspectはOnなので、xだけ変更してやればyは勝手に修正される。
		Vector2 imgSize = itemImg.GetComponent<RectTransform>().sizeDelta;
		imgSize.x = transform.parent.GetComponent<GridLayoutGroup> ().cellSize.x;
		itemImg.GetComponent<RectTransform> ().sizeDelta = imgSize;
	}

	/**
	 * 
	 * 武器などの場合必要素材などを表示させてクラフトするかどうかを確認。
	 * 
	 * 
	 */
	public void OnClickItemMenu() {
		CraftUiController.Instance.createPanel.GetComponent<CreatePanel> ().InitItemCraftingData (_itemData);
	}
}
