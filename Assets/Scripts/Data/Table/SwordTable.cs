using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTable : MonoBehaviour {
	public class SwordDataTable : AbstractMasterTable<SwordData> {
		private static readonly string FilePath = "Csv/Sword.csv";
		public void Load() { Load(FilePath); }
	}
}
