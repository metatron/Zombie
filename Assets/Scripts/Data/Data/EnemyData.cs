using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData {
	public string Prefab { get; set; }
	public int HP { get; set; }
	public float Speed { get; set; }
	public float Interval { get; set; }

	/**
	 * 
	 * 死んだ際得られるExpPoint
	 * 
	 */
	public int GetExpPoint() {
		StageData crntStage = GameManager.Instance.CurrentStageObject.GetComponent<StageObject> ().StageData;
		int point = (int)Mathf.Max (1, HP * crntStage.GetIdInt () / 10.0f);
//		Debug.LogError ("****************Get ExpPoint: " + point);
		return point;
	}
}
