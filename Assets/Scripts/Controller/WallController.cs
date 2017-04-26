using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : SingletonMonoBehaviourFast<WallController> {

	public List<GameObject> WallObjects = new List<GameObject>();

	public void InitWallObjects() {
		int totalWallNum = PlayerData.availableBattlePosition;
		for (int i=0; i<totalWallNum; i++) {
			GameObject wallObject = WallObjects [i];
			wallObject.SetActive (true);
		}
	}
}
