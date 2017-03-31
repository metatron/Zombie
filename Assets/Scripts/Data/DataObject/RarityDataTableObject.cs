using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityDataTableObject : SingletonMonoBehaviourFast<RarityDataTableObject> {
	private RarityDataTable _table = new RarityDataTable();
	public RarityDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (RarityData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.Rarity)) {
				dataDictionary.Add (data.Rarity, data);
			}
		}
	}

	public RarityData GetParams(string id) {
		return (RarityData)dataDictionary [id];
	}
		

	public bool isInitialized() {
		//データがない場合はfalse
		if (_table.All == null || _table.All.Count == 0) {
			return false;
		}
		return true;
	}


}
