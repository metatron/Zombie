using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageUI : MonoBehaviour {
	private StageData _stageData;
	public Text stageName;

	public delegate void ClickStageAction(AbstractData itemData);

	public ClickStageAction _clickStageAction;

	public void InitStageUI(StageData stageData, ClickStageAction clckStgAction) {
		_stageData = stageData;
		stageName.text = _stageData.Name;
		_clickStageAction = clckStgAction;
	}

	public void OnClickStageUI() {
		_clickStageAction (_stageData);
	}


}
