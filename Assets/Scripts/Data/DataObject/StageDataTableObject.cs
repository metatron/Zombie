using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataTableObject : SingletonMonoBehaviourFast<StageDataTableObject> {
	private StageDataTable _table = new StageDataTable();
	public StageDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (StageData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public StageData GetParams(string id) {
		return (StageData)dataDictionary [id];
	}
		

	public bool isInitialized() {
		//データがない場合はfalse
		if (_table.All == null || _table.All.Count == 0) {
			return false;
		}
		return true;
	}


}
