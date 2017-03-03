using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	public Image itemImg;
	public Text numText;
	public Text nameText;

	private AbstractData _itemData;

	public delegate void ClickItemAction(AbstractData itemData);

	public ClickItemAction _clickItemAction;


	public void InitItemMenu(string atlas, AbstractData itemData, string numOwned) {
		_itemData = itemData;
		nameText.text = _itemData.Name;
		numText.text = numOwned;

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
	 * クラフト画面での一覧は、武器などの場合必要素材などを表示させてクラフトするかどうかを確認。
	 * キャラクターステータス画面での一覧でクリックした場合は装備のON/OFF or 食べ物だった場合はたべる。
	 * 
	 * クラフト：CraftUiController.InitSwordObjButtonで使用。
	 * 
	 * 
	 */
	public void OnClickItemMenu() {
		_clickItemAction (_itemData);
	}

}
