using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDataTable : AbstractMasterTable<FoodData> {
	private static readonly string FilePath = "Csv/Food";
	public void Load() { Load(FilePath); }
}