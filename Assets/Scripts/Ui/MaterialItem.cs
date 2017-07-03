using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItem : MonoBehaviour {
	public Image image;
	public Text materialNum;

	public void SetUpMaterialItem(string id) {
		Sprite sp = GameManager.Instance.GetSpriteFromID (id);
		if (sp != null) {
			image.sprite = sp;
		}
	}
}
