using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothDataTable : AbstractMasterTable<ClothData> {
	private static readonly string FilePath = "Csv/Cloth";
	public void Load() { Load(FilePath); }
}