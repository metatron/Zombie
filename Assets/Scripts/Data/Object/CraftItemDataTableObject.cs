using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemDataTableObject : SingletonMonoBehaviourFast<CraftItemDataTableObject> {
	private CraftItemDataTable _table = new CraftItemDataTable();
	public CraftItemDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (CraftItemData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public CraftItemData GetParams(string id) {
		return (CraftItemData)dataDictionary [id];
	}
		

	public bool isInitialized() {
		//データがない場合はfalse
		if (_table.All == null || _table.All.Count == 0) {
			return false;
		}
		return true;
	}


}
