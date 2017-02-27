using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
	//プレイヤーの所持するアイテムとその数。<ID, 総数>
	public static Dictionary<string, int> playerItemDictionary = new Dictionary<string, int>();

	//プレイヤーの所持するNPC。<ID, 総数>
	public static Dictionary<string, CharaData> playerNpcDictionary = new Dictionary<string, CharaData>();

	public static void InitPlayerData() {
		playerItemDictionary ["Wood"] = 10;
		playerItemDictionary ["Metal"] = 10;

		//TODO テスト的に追加
		if (!playerNpcDictionary.ContainsKey ("npc1")) {
			CharaData chardata = new CharaData ();
			chardata.BodyPrefab = "Female1";
			chardata.BattlePosition = 1;
			chardata.SwordID = "swd1";
			chardata.GunID = "Gun01";

			playerNpcDictionary.Add ("npc1", chardata);
		}
	}

	public static void AddItem(AbstractData itemData) {
		//ない場合は新しく1追加
		if (!playerItemDictionary.ContainsKey (itemData.ID)) {
			playerItemDictionary [itemData.ID] = 1;
		}
		else {
			playerItemDictionary [itemData.ID] += 1;
		}
	}
}
