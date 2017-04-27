using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : SingletonMonoBehaviourFast<WallController> {

	public List<GameObject> WallObjects = new List<GameObject>();

	public void Start() {
		InitWallObjects ();
	}

	public void InitWallObjects() {
		int totalWallNum = PlayerData.GetItemNum ("Wall");
		for (int i=0; i<totalWallNum; i++) {
			GameObject wallObject = WallObjects [i];
			wallObject.SetActive (true);
		}
	}
}
