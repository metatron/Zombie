using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	public Image itemImg;
	public Text numText;
	public Text nameText;

	public void InitItemMenu(Image itemImg, int numOwned) {
		numText.text = "" + numOwned;
		this.itemImg = itemImg;

		this.itemImg.SetNativeSize ();
	}

	public void OnClickItemMenu() {
		Debug.Log ("*****");
	}
}
