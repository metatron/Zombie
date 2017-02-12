﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDataTableObject : SingletonMonoBehaviourFast<SwordDataTableObject> {
	private SwordDataTable _table = new SwordDataTable();
	public SwordDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (SwordData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public SwordData GetParams(string id) {
		return (SwordData)dataDictionary [id];
	}
		



}