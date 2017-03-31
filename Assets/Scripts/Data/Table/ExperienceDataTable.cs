using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceDataTable : AbstractMasterTable<ExperienceData> {
	private static readonly string FilePath = "Csv/Experience";
	public void Load() { Load(FilePath); }
}