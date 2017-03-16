using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageUI : MonoBehaviour {
	private StageData _stageData;
	public Text stageName;

	public void InitStageUI(StageData stageData) {
		_stageData = stageData;
		stageName.text = _stageData.Name;
	}


}
