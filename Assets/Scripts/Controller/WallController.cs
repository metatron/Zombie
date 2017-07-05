using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : SingletonMonoBehaviourFast<WallController> {
	public static int MAX_WALL_NUM = 10;

	public List<GameObject> WallObjects = new List<GameObject>();

	public void Start() {
		InitWallObjects ();
	}

	public void InitWallObjects() {
		//0はプレイヤーの隣。常にON状態
		int totalWallNum = PlayerData.GetItemNum ("wall") + 1;
		for (int i=0; i<totalWallNum; i++) {
			GameObject wallObject = WallObjects [i];
			wallObject.SetActive (true);
		}
	}

	/**
	 * 
	 * NPCの位置を決めるのに必要。
	 * GameManaterのInitNpcObjectで使用。
	 * 
	 */
	public GameObject GetWallObject(int pos) {
		return WallObjects [pos];
	}
}
