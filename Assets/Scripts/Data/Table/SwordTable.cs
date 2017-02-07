using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDataTable : AbstractMasterTable<SwordData> {
	private static readonly string FilePath = "SwordData";
	public void Load() { Load(FilePath); }
}
