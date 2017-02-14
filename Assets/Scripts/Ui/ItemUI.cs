using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	public Image itemImg;
	public Text numText;
	public Text nameText;

	private static Dictionary<string, Sprite[]> loadedSpriteDict = new Dictionary<string, Sprite[]>();

	public void InitItemMenu(string atlas, string itemSpriteName, int numOwned) {
		nameText.text = itemSpriteName;
		numText.text = "" + numOwned;

		string weaponImgPath = "Atlases/" + atlas;

		Sprite[] loadedSprites;
		if (!loadedSpriteDict.ContainsKey (atlas)) {
			loadedSprites = Resources.LoadAll<Sprite> (weaponImgPath);
			loadedSpriteDict.Add (atlas, loadedSprites);
		} 
		//load from Dictionary
		else {
			loadedSprites = loadedSpriteDict [atlas];
		}
		itemImg.sprite = System.Array.Find<Sprite> (loadedSprites, (sprite) => sprite.name.Equals (itemSpriteName));
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
	}
}
