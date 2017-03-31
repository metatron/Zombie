using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityDataTable : AbstractMasterTable<RarityData> {
	private static readonly string FilePath = "Csv/Rarity";
	public void Load() { Load(FilePath); }
}