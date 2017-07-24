using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatusUi : MonoBehaviour {
	public enum CharStatusType: int {
		NONE,
		HUNGER,
		DEAD
	}

	public void InitCharStatusUi(CharStatusType type, string sortingLayerName="") {
		string spriteName = "";

		switch (type) {
		case CharStatusType.HUNGER:
			spriteName = "hungericon";
			break;
		}

		if (string.IsNullOrEmpty (spriteName)) {
			return ;
		}
		Sprite statusSprite = GetComponent<Sprite> ();
		statusSprite = GameManager.Instance.GetSpriteFromPath ("UiAtlas", spriteName);

		if (!string.IsNullOrEmpty (sortingLayerName)) {
			GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
		}
	}
}
