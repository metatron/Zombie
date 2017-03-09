using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDataTable : AbstractMasterTable<GunData> {
	private static readonly string FilePath = "Csv/Gun";
	public void Load() { Load(FilePath); }
}