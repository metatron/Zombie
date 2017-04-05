using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityData: AbstractData {
	public string Rarity { get; set; } //RarityDataTableObjectでのIDがstringで読み込むようになってるため
	public int MaxLevel { get; set; }
	public float CompoCostRatio { get; set; }
	public int GachaRatio { get; set; }
}
