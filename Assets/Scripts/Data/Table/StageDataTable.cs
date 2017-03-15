using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataTable : AbstractMasterTable<StageData> {
	private static readonly string FilePath = "Csv/Stage";
	public void Load() { Load(FilePath); }
}