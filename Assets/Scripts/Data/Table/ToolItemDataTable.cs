using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItemDataTable : AbstractMasterTable<ToolItemData> {
	private static readonly string FilePath = "Csv/ToolItem";
	public void Load() { Load(FilePath); }
}