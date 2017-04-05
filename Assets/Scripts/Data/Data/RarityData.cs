using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityData: AbstractData {
	public enum RarityType: int {
		C 		= 1,
		B,
		A,
		S,
		L		//5
	}
	public string Rarity { get; set; } //RarityDataTableObjectでのIDがstringで読み込むようになってるため
	public int MaxLevel { get; set; }
	public float CompoCostRatio { get; set; }
}
