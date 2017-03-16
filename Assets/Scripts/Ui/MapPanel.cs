using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanel : MonoBehaviour {
	public GameObject content;

	public GameObject stageUIPrefab;

	public void InitMapPanel() {
		ResetMapContents ();
		List<StageData> stageList = StageDataTableObject.Instance.Table.All;

		//ItemUIをInstantiateし、値をセットし、contentに追加。
		foreach (StageData stageData in stageList) {
			GameObject initedStageUIObj = (GameObject)Instantiate (stageUIPrefab);
			initedStageUIObj.GetComponent<StageUI> ().InitStageUI (stageData);
			initedStageUIObj.transform.SetParent (content.transform);
			initedStageUIObj.GetComponent<RectTransform> ().localScale = Vector3.one;
			initedStageUIObj.GetComponent<RectTransform> ().localPosition = new Vector3 (0.0f, 0.0f, -10.0f);
		}
	}

	private void ResetMapContents() {
		UiController.Instance.ResetContent (content);
	}

	public void CloseMapPanel() {
		gameObject.SetActive (false);
	}
}
