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
		numText.text = "" + numOwned;
		this.itemImg = itemImg;

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
		Debug.LogError ("*****: " + itemImg.sprite);
	}

	public void SetImageSize() {
		this.itemImg.SetNativeSize ();
		Rect rect = this.GetComponent<Image> ().sprite.rect;
		Debug.LogError ("@@@: " + itemImg + ":" + rect.size + ":" + itemImg.sprite.rect.size);

		float xRatio = rect.size.x/itemImg.sprite.rect.size.x;		//1以下なら画像のXの方がボタンよりでかい
		float yRatio = itemImg.sprite.rect.size.y / rect.size.y;	//1以下なら画像のYの方がボタンよりでかい

		float adjustRatio = Mathf.Max (xRatio, yRatio);
		itemImg.GetComponent<RectTransform> ().localScale = Vector3.one * adjustRatio;
	}

	public void OnClickItemMenu() {
		Debug.Log ("*****");
	}
}
