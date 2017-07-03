using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceDataTableObject : SingletonMonoBehaviourFast<ExperienceDataTableObject> {
	private ExperienceDataTable _table = new ExperienceDataTable();
	public ExperienceDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	public void InitData() {
		_table.Load ();
		foreach (ExperienceData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.Level)) {
				dataDictionary.Add (data.Level, data);
			}
		}
	}

	public RarityData GetParams(string id) {
		if (dataDictionary.ContainsKey (id)) {
			return (RarityData)dataDictionary [id];
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
