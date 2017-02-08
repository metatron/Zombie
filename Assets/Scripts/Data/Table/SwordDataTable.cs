using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDataTable : AbstractMasterTable<SwordData> {
	private static readonly string FilePath = "Csv/Sword";
	public void Load() { Load(FilePath); }
}