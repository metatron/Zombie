using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDataTableObject : SingletonMonoBehaviourFast<FoodDataTableObject> {
	private FoodDataTable _table = new FoodDataTable();
	public FoodDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (FoodData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public FoodData GetParams(string id) {
		if (dataDictionary.ContainsKey (id)) {
			return (FoodData)dataDictionary [id];
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
