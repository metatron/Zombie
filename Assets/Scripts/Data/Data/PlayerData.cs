﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	//プレイヤーの所持するアイテムとその数。<ID, 総数>
	private static Dictionary<string, int> _itemOwnedDictionary = new Dictionary<string, int>();

	//プレイヤーの所持するNPC。<ID, 総数>
	public static Dictionary<string, CharaData> playerNpcDictionary = new Dictionary<string, CharaData>();

	//現在の時点のものをセーブするため、GameManagerではなくこちらに設置。
	public static string crntStageID = "stg1";

	//未使用のExpPoint。これを使用してきゃらを育てる。
	public static int unusedExpPoints = 0;

	public static void InitPlayerData() {
		_itemOwnedDictionary ["Wood"] = 10;
		_itemOwnedDictionary ["Metal"] = 10;


		//TODO テスト的に追加
		if (!playerNpcDictionary.ContainsKey ("npc1")) {
			CharaData chardata = new CharaData ();
			chardata.Name = "Mia";
			chardata.BodyPrefab = "Female1";
			chardata.BattlePosition = 1;
			chardata.SwordID = "swd1";
			chardata.GunID = "gun1";

			playerNpcDictionary.Add ("npc1", chardata);

//			SerializeUtil.XmlSerialize ("char", playerNpcDictionary);
		}
	}

	public static int AddItem(AbstractData itemData) {
		//ない場合は新しく1追加
		if (!_itemOwnedDictionary.ContainsKey (itemData.ID)) {
			_itemOwnedDictionary [itemData.ID] = 1;
		}
		else {
			_itemOwnedDictionary [itemData.ID] += 1;
		}

		return _itemOwnedDictionary [itemData.ID];
	}

	public static int GetItemNum(string id) {
		if(!_itemOwnedDictionary.ContainsKey(id)) {
			return 0;
		}
		return _itemOwnedDictionary[id];
	}

	public static bool UseItem(string id, int num) {
		//持ってないとエラー
		if(!_itemOwnedDictionary.ContainsKey(id)) {
			return false;
		}

		//数以上持ってたら使用してtrueを返す
		if (_itemOwnedDictionary [id] >= num) {
			_itemOwnedDictionary [id] -= num;
			return true;
		}

		//その他エラー
		return false;
	}

	public static bool EquipItem(string id) {
		if (string.IsNullOrEmpty (id)) {
			return false;
		}

		//持ってないとエラー
		if(!_itemOwnedDictionary.ContainsKey(id)) {
			return false;
		}

		//持ってたらOwnedから外してEquippedに追加
		if (_itemOwnedDictionary [id] > 0) {
			_itemOwnedDictionary [id] --;
			return true;
		}

		//その他エラー
		return false;
	}

	public static void UnequipItem(string id) {
		if (string.IsNullOrEmpty (id)) {
			return;
		}

		//持ってないとエラー
		if(!_itemOwnedDictionary.ContainsKey(id)) {
			_itemOwnedDictionary.Add (id, 1);
			return ;
		}

		//持ってたら追加
		_itemOwnedDictionary [id] ++;
	}

	public static int TotalEquipedNum(string id) {
		int count = 0;

		foreach (string charaId in playerNpcDictionary.Keys) {
			CharaData charaData = playerNpcDictionary [charaId];
			if (charaData.SwordID == id || charaData.GunID == id) {
				count++;
			}
		}
		return count;
	}
		
	public static List<CharaData> GetBattleNpcList() {
		List<CharaData> battleList = new List<CharaData> ();
		foreach (CharaData charData in playerNpcDictionary.Values) {
			if (charData.BattlePosition > 0 && !charData.Injured) {
				battleList.Add (charData);
			}
		}
		return battleList;
	}
}
