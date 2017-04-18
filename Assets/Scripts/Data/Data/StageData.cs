using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : AbstractData {
	public string BG { get; private set; }
	public string Spawn1 { get; private set; }		//Enemy01|HP:3|SPEED:1|INTERVAL:1.5
	public string Spawn2 { get; private set; }
	public string Spawn3 { get; private set; }
	public float TimeTillBoss { get; private set; }
	public string SpawnBoss { get; private set; }	//Enemy01|HP:30|SPEED:1
	public string Drops { get; private set; }		//Metal:0.01|Wood:0.01
	public string Npc { get; private set; }			//Rarity:Gender

	/**
	 * 
	 * stgのIDからInt型を得る。
	 * 
	 */
	public int GetIdInt() {
		int stgNum = Int32.Parse(ID.Replace("stg", ""));
		return stgNum;
	}
}
