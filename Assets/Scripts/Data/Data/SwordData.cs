using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : AbstractData {
	public float Damage { get; private set; }
	public float ReachLength { get; private set; }
	public string SwingEffect { get; private set; }

	//exp: 
	//Sword1
	//Metal:3|Wood:1
	public string RequirementStr { get; private set; }

	//上記、RequirementListStrをジェネリックリスト化
	public List<AbstractData.RequirementData> requirementDataList = new List<AbstractData.RequirementData>();

	public void InitRequirementList() {
		string[] splitRequirementList = RequirementStr.Split ('|');
		foreach (string tmpMaterial in splitRequirementList) {
			string[] data = tmpMaterial.Split (':');
			switch (data [0].ToLower()) {
			case "metal":
				break;
			case "wood":
				break;
			case "cloth":
				break;
			case "Metal":
				break;
			}
		}
	}
}
