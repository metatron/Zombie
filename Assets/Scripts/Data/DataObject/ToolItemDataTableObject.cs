using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItemDataTableObject : SingletonMonoBehaviourFast<ToolItemDataTableObject> {
	private ToolItemDataTable _table = new ToolItemDataTable();
	public ToolItemDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (ToolItemData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public ToolItemData GetParams(string id) {
		if (dataDictionary.ContainsKey (id)) {
			return (ToolItemData)dataDictionary [id];
		}
		return null;
	}
		

	public bool isInitialized() {
		//データがない場合はfalse
		if (_table.All == null || _table.All.Count == 0) {
			return false;
		}
		return true;
	}


}
