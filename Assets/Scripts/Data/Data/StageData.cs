﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : AbstractData {
	public string BG { get; private set; }
	public string Spawn1 { get; private set; }
	public string Spawn2 { get; private set; }
	public string Spawn3 { get; private set; }
	public float TimeTillBoss { get; private set; }
	public string SpawnBoss { get; private set; }
	public string Drops { get; private set; }

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
