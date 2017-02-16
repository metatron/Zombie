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
}
