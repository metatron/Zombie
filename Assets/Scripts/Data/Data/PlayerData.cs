using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
	//プレイヤーの所持するアイテムとその数。<ID, 総数>
	public static Dictionary<string, int> playerItemDictionary = new Dictionary<string, int>();

	public static void InitPlayerData() {
		playerItemDictionary ["Wood"] = 10;
		playerItemDictionary ["Metal"] = 10;
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
