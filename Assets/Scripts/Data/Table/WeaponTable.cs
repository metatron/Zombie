using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataTable : AbstractMasterTable<WeaponData> {
	private static readonly string FilePath = "WeaponData";
	public void Load() { Load(FilePath); }
}
