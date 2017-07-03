using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClothDataTableObject : SingletonMonoBehaviourFast<ClothDataTableObject> {
	private ClothDataTable _table = new ClothDataTable();
	public ClothDataTable Table { get { return _table; } set {_table = value; } }

	private Dictionary<string, AbstractData> dataDictionary = new Dictionary<string, AbstractData>();

	private List<string> groupIdList = new List<string> ();
	private List<string> maleGroupIdList = new List<string> ();
	private List<string> femaleGroupIdList = new List<string> ();

	//キー：GroupID, データ：List<CloghData>
	private Dictionary<string, List<ClothData>> clothListByGroupId = new Dictionary<string, List<ClothData>>();

	public void InitData() {
		_table.Load ();
		foreach (ClothData data in _table.All) {
			if (!dataDictionary.ContainsKey (data.ID)) {
				dataDictionary.Add (data.ID, data);
			}
		}
	}

	public ClothData GetParams(string id) {
		if (dataDictionary.ContainsKey (id)) {
			return (ClothData)dataDictionary [id];
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

	public List<ClothData> GetClothListByGroupId(string groupId) {
		foreach (ClothData data in _table.All) {
			//GroupIDが存在しない場合はClothリストをつくって追加
			if (!clothListByGroupId.ContainsKey (data.GroupID)) {
				List<ClothData> groupClothDataList = new List<ClothData> ();
				groupClothDataList.Add (data);
				clothListByGroupId.Add (data.GroupID, groupClothDataList);
			} 
			//GroupIDが存在した場合はdata.IDがClothリストに存在しなかった場合はリストに追加
			else {
				ClothData foundCloth = clothListByGroupId [data.GroupID].FirstOrDefault (tmpClothData => tmpClothData.ID == data.ID);
				if (foundCloth == null) {
					clothListByGroupId [data.GroupID].Add (data);
				}
			}
		}

		return clothListByGroupId [groupId];
	}


	/**
	 * 
	 * 服のグループIDのリストを返す。
	 * 自動服生成で必要。
	 * 
	 */
	public List<string> GetGroupIdList() {
		if (groupIdList.Count > 0) {
			return groupIdList;
		}

		foreach (ClothData data in _table.All) {
			if (groupIdList.Contains (data.GroupID)) {
				continue;
			}
			groupIdList.Add (data.GroupID);
		}

		return groupIdList;
	}

	/**
	 * 
	 * 性別のGroupIDのリストを取得。
	 * 
	 */
	public List<string> GetGroupIdList(CharaData.Gender gender) {
		if (gender == CharaData.Gender.Male && maleGroupIdList.Count > 0) {
			return maleGroupIdList;
		}
		if (gender == CharaData.Gender.Female && femaleGroupIdList.Count > 0) {
			return femaleGroupIdList;
		}

		GetGroupIdList ();
		foreach (string groupId in groupIdList) {
			if (groupId.Contains ("mc")) {
				maleGroupIdList.Add (groupId);
			} 
			else {
				femaleGroupIdList.Add (groupId);
			}
		}

		if (gender == CharaData.Gender.Male) {
			return maleGroupIdList;
		}

		return femaleGroupIdList;
	}

	/**
	 * 
	 * type別にグループIDのリストを選出。
	 * TODO: スピード
	 * 
	 * 
	 */
	public List<string> GetGroupIdList(CharaData.Gender gender, ClothingSystem.ClothParts type) {
		string searchGroup = "mc";
		if (gender == CharaData.Gender.Female) {
			searchGroup = "fc";
		}

		List<string> groupList = new List<string> ();
		foreach (ClothData data in _table.All) {
			//男女で区別
			if (!data.Name.Contains (searchGroup)) {
				continue;
			}

			//探してるtypeかどうかチェック
			string searchingID = ClothingSystem.GetClothingID (type, data.GroupID);
			if (data.ID != searchingID) {
				continue;
			}

			groupList.Add (data.GroupID);
		}

		return groupList;
	}

}