using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour {
	private StageData _stageData;

	public void InitStageObject(StageData stageData) {
		_stageData = stageData;
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
	}
}
