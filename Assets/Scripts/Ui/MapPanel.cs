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
		foreach (AbstractData stageData in stageList) {
			GameObject initedStageUIObj = (GameObject)Instantiate (stageUIPrefab);

		}
	}

	private void ResetMapContents() {
		CraftUiController.Instance.ResetContent (content);
	}

	public void CloseMapPanel() {
		gameObject.SetActive (false);
	}
}
