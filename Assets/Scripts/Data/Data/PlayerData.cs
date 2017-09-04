using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	//プレイヤーのCharaData.ID
	public const string PLAYERID = "player";
	public const int WALLPOSITION_MAX = 11;

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


	public static void InitPlayerData() {
		//保持しているマテリアル
		_itemOwnedDictionary ["wood"] = 100;
		_itemOwnedDictionary ["metal"] = 100;
		_itemOwnedDictionary ["potato"] = 100;

		//プレイヤーキャラの初期化
		playerCharData = CharacterLevelSystem.GenerateCharacterData (1, (int)CharaData.Gender.Male);
		playerCharData.ID = PLAYERID;
		playerCharData.Name = "Player";
		playerCharData.SwordID = "swd2";
		playerCharData.GunID = "gun2";

		//TODO テスト的に追加
		if (playerNpcDictionary.Count == 0) {
			CharaData chardata = CharacterLevelSystem.GenerateCharacterData (1, (int)CharaData.Gender.Female);
			chardata.Name = "Mia";
			chardata.BodyPrefab = "Female1";
			chardata.SwordID = "swd1";
			chardata.GunID = "";
			Debug.LogError ("NPC1: " + chardata.ClothDataStr);

			playerNpcDictionary.Add (chardata.ID, chardata);

			chardata = CharacterLevelSystem.GenerateCharacterData (1, (int)CharaData.Gender.Female);
			chardata.Name = "Mia";
			chardata.BodyPrefab = "Female1";
			chardata.SwordID = "";
			chardata.GunID = "gun1";
			Debug.LogError ("NPC1: " + chardata.ClothDataStr);

			playerNpcDictionary.Add (chardata.ID, chardata);


////			SerializeUtil.XmlSerialize ("char", playerNpcDictionary);
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
			Debug.LogError ("Player does not have item: " + id);
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
			if (charData.BattlePosition >= 0 && !charData.IsDead) {
				battleList.Add (charData);
			}
		}
		return battleList;
	}

	public static void AddNpcData(CharaData npcData) {
		playerNpcDictionary.Add (npcData.ID, npcData);
	}


	//=========================== BattlePosition系ファンクション ===========================//

	public static string GetBattlePosNpcId(int pos) {
		CharaData charaData = playerNpcDictionary.Values.FirstOrDefault (tmpCharData => tmpCharData.BattlePosition == pos);
		if (charaData == null) {
			return "";
		}
		return charaData.ID;
	}

	public static void SetBattlePosNpcId(int pos, CharaData charData) {
		charData.BattlePosition = pos;
	}

	public static void UnSetBattlePosNpcId(int pos) {
		foreach (CharaData tmpCarData in playerNpcDictionary.Values) {
			if (tmpCarData.BattlePosition == pos) {
				tmpCarData.BattlePosition = -1;
				break;
			}
		}
	}
}
