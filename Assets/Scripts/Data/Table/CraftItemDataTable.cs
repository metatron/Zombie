using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemDataTable : AbstractMasterTable<CraftItemData> {
	private static readonly string FilePath = "Csv/CraftItem";
	public void Load() { Load(FilePath); }
}