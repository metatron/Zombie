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
			initedStageUIObj.GetComponent<StageUI> ().InitStageUI (stageData, 
				(stgData) => {
					//Dialog表示（ステージ移動しますか？）
					UiController.Instance.OpenDialogPanel("Goto " + stgData.Name + "?", 
						//はい（InitStage）
						()=> {
							//ハンガーレベル調査。
							//死にそうな人がいればワーニング。
							List<CharaData> hungryCharList = GameManager.Instance.CheckHungerLevel();
							if(hungryCharList.Count > 0) {
								UiController.Instance.OpenDialogPanel2("There are some hungry characters.\nGoing to battle will kill these characters.\nAre you sure?", 
									//はい
									() => {
										//画面切り替え
										CloseMapPanel();
										PlayerData.crntStageID = stgData.ID;
										TransitionManager.Instance.FadeTo ("Main");
									},
									//いいえ
									() => {}
								);
							}

							//誰も腹減ってない
							else {
								CloseMapPanel();
								PlayerData.crntStageID = stgData.ID;
								TransitionManager.Instance.FadeTo ("Main");
							}
						},
						//いいえ（ただ消す）
						()=>{}
					);
				}
			);
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
