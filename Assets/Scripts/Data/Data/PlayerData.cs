using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	//プレイヤーのCharaData.ID
	public const string PLAYERID = "player";

	//プレイヤーの所持するアイテムとその数。<ID, 総数>
	private static Dictionary<string, int> _itemOwnedDictionary = new Dictionary<string, int>();

	//プレイヤーの所持するNPC。<ID, 総数>
	public static Dictionary<string, CharaData> playerNpcDictionary = new Dictionary<string, CharaData>();

	//プレイヤーのキャラデータ
	public static CharaData playerCharData;

	//現在の時点のものをセーブするため、GameManagerではなくこちらに設置。
	public static string crntStageID = "stg1";

	//未使用のExpPoint。これを使用してきゃらを育てる。
	public static int unusedExpPoints = 10000;

	//NPC設置可能な場所とその有効性 <position, npcID> position = 0はプレイヤーの隣のポジション。トータル11
	public static Dictionary<int, string> npcBattlePositionDictionary = new Dictionary<int, string>();


	public static void InitPlayerData() {
		//保持しているマテリアル
		_itemOwnedDictionary ["Wood"] = 10;
		_itemOwnedDictionary ["Metal"] = 10;

		//プレイヤーキャラの初期化
		playerCharData = CharacterLevelSystem.GenerateCharacterData (1);
		playerCharData.ID = PLAYERID;
		playerCharData.Name = "Player";
		playerCharData.SwordID = "swd2";
		playerCharData.GunID = "gun2";

		//TODO テスト的に追加
		if (playerNpcDictionary.Count == 0) {
			CharaData chardata = CharacterLevelSystem.GenerateCharacterData (1);
			chardata.Name = "Mia";
			chardata.BodyPrefab = "Female1";
			chardata.BattlePosition = 1;
			chardata.SwordID = "swd1";
			chardata.GunID = "gun1";

			playerNpcDictionary.Add (chardata.ID, chardata);

//			SerializeUtil.XmlSerialize ("char", playerNpcDictionary);
		}

		//バトルポジションの初期化
		for(int i=0; i<11; i++) {
			npcBattlePositionDictionary.Add(i, "");
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

	public static void AddNpcData(CharaData npcData) {
		playerNpcDictionary.Add (npcData.ID, npcData);
	}


	//=========================== BattlePosition系ファンクション ===========================//

	public static string getBattlePosNpcId(int pos) {
		return npcBattlePositionDictionary [pos];
	}
}
