using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterPanel : MonoBehaviour {
	public GameObject content;

	public GameObject charUIPrefab;

	public void InitCharacterList() {
		for (int i = 0; i < PlayerData.playerNpcDictionary.Count; i++) {
			string key = PlayerData.playerNpcDictionary.Keys.ToArray() [i];
			CharaData charData = PlayerData.playerNpcDictionary [key];

			GameObject instantiatedPrefab = (GameObject)Instantiate (charUIPrefab);
			instantiatedPrefab.GetComponent<CharacterUI> ().InitCharaUI <NpcObject>(charData);
			instantiatedPrefab.transform.SetParent (content.transform);
			instantiatedPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
			Vector3 tmpPos = instantiatedPrefab.GetComponent<RectTransform>().localPosition;
			tmpPos.z = -10;
			instantiatedPrefab.GetComponent<RectTransform>().localPosition = tmpPos;

		}
	}
}
