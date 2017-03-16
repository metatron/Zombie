
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDataTableObject : SingletonMonoBehaviourFast<GunDataTableObject> {
	private GunDataTable _table = new GunDataTable();
	public GunDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (GunData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public GunData GetParams(string id) {
		return (GunData)dataDictionary [id];
	}
		

	public bool isInitialized() {
		//データがない場合はfalse
		if (_table.All == null || _table.All.Count == 0) {
			return false;
		}
		return true;
	}
}
