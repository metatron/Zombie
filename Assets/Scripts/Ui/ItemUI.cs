using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	public Image itemImg;
	public Text numText;
	public Text nameText;

	private Sprite[] loadedSprites;

	public void InitItemMenu(string atlas, string itemSpriteName, int numOwned) {
		numText.text = "" + numOwned;
		this.itemImg = itemImg;


		string weaponImgPath = "Atlases/" + atlas;
		loadedSprites = Resources.LoadAll<Sprite> (weaponImgPath);
		itemImg.sprite = System.Array.Find<Sprite> (loadedSprites, (sprite) => sprite.name.Equals (itemSpriteName));

		this.itemImg.SetNativeSize ();
	}

	public void OnClickItemMenu() {
		Debug.Log ("*****");
	}
}
