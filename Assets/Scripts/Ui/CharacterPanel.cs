using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterPanel : MonoBehaviour {
	public GameObject content;

	public GameObject charUIPrefab;

	public void InitCharacterList() {
		ResetContent ();

		//add Player
		GameObject instantiatedPrefab = (GameObject)Instantiate (charUIPrefab);
		instantiatedPrefab.GetComponent<CharacterUI> ().InitCharaUI <NpcObject>(PlayerData.playerCharData);
		instantiatedPrefab.transform.SetParent (content.transform);
		instantiatedPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
		Vector3 tmpPos = instantiatedPrefab.GetComponent<RectTransform>().localPosition;
		tmpPos.z = -10;
		instantiatedPrefab.GetComponent<RectTransform>().localPosition = tmpPos;

		//add NPCs
		for (int i = 0; i < PlayerData.playerNpcDictionary.Count; i++) {
			string key = PlayerData.playerNpcDictionary.Keys.ToArray() [i];
			CharaData charData = PlayerData.playerNpcDictionary [key];

			instantiatedPrefab = (GameObject)Instantiate (charUIPrefab);
			instantiatedPrefab.GetComponent<CharacterUI> ().InitCharaUI <NpcObject>(charData);
			instantiatedPrefab.transform.SetParent (content.transform);
			instantiatedPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
			tmpPos = instantiatedPrefab.GetComponent<RectTransform>().localPosition;
			tmpPos.z = -10;
			instantiatedPrefab.GetComponent<RectTransform>().localPosition = tmpPos;

		}
	}

	private void ResetContent() {
		CharacterUI[] charaUIButtonArray = content.GetComponentsInChildren<CharacterUI> ();
		int length = charaUIButtonArray.Length;
		for(int i=0; i<length; i++) {
			CharacterUI charaUIBtn = charaUIButtonArray [i];
			Destroy (charaUIBtn.gameObject);
		}
		charaUIButtonArray = null;
	}

	public void OnCloseCharacterPanel() {
		gameObject.SetActive (false);

		//取っておいたサムネをすべて破棄
		UiController.Instance.ResetCharThumbnail();
	}

}
