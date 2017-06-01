using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		return (ClothData)dataDictionary [id];
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
			} 
			//GroupIDが存在した場合はdata.IDがClothリストに存在しなかった場合はリストに追加
			else {
				List<ClothData> groupClothDataList = clothListByGroupId [data.GroupID];
				bool included = false;
				foreach (ClothData clothData in groupClothDataList) {
					if (clothData.ID == data.ID) {
						included = true;
						break;
					}
				}
				if (!included) {
					groupClothDataList.Add (data);
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


}
