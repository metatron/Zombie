using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour {
	private StageData _stageData;

	public List<Spawner> spawnerList = new List<Spawner>();

	public void InitStageObject(StageData stageData) {
		_stageData = stageData;
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;

		//Spawner1
		if(!string.IsNullOrEmpty(stageData.Spawn1)) {
			EnemyData enemy = ParseEnemyData (stageData.Spawn1);
			spawnerList[0].StartSpawning (enemy);
			spawnerList[0].spawn = true;
		}

		//Spawner2
		if(!string.IsNullOrEmpty(stageData.Spawn2)) {
			EnemyData enemy = ParseEnemyData (stageData.Spawn2);
			spawnerList[1].StartSpawning (enemy);
			spawnerList[1].spawn = true;
		}

		//Spawner3
		if(!string.IsNullOrEmpty(stageData.Spawn2)) {
			EnemyData enemy = ParseEnemyData (stageData.Spawn2);
			spawnerList[2].StartSpawning (enemy);
			spawnerList[2].spawn = true;
		}

	}

	/**
	 * 
	 * Stage.csvの、Spawn#を元に、
	 * Spawner#のデータ用EnemyDataを作成。
	 * 
	 */
	private EnemyData ParseEnemyData(string enemyDataStr) {
		if (string.IsNullOrEmpty (enemyDataStr)) {
			return null;
		}
		Debug.LogError ("***************************enemyDataStr: " + enemyDataStr);

		string[] stage1Splited = enemyDataStr.Split ('|');

		EnemyData enemy = new EnemyData ();
		//Enemy Prefab
		Debug.LogError("@@@@@@@@@@@@@@@@@1: " + stage1Splited [0]);
		enemy.Prefab = stage1Splited [0];
		//HP
		string hpStr = stage1Splited [1].Split(':')[1];
		Debug.LogError("@@@@@@@@@@@@@@@@@2: " + hpStr);
		enemy.HP = Int32.Parse (hpStr);
		//Speed
		string speedStr = stage1Splited [2].Split(':')[1];
		Debug.LogError("@@@@@@@@@@@@@@@@@3: " + speedStr);
		enemy.Speed = float.Parse (speedStr);

		return enemy;
	}
}
