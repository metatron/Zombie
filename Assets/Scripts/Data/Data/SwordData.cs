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

	public Dictionary<AbstractData.DataType, int> ParseRequirementStr() {
		if (RequirementStr == null || RequirementStr == "" || RequirementStr.Length == 0) {
			return null;
		}
		Dictionary<AbstractData.DataType, int> requirementDict = new Dictionary<AbstractData.DataType, int> ();

		string[] requirementList = RequirementStr.Split ('|');
		foreach (string requirementNumStr in requirementList) {
			string[] requirementNum = requirementNumStr.Split (':');
			AbstractData.DataType type = (AbstractData.DataType)Enum.Parse (typeof(AbstractData.DataType), requirementNum [0], true);
			int num = Int32.Parse (requirementNum [1]);
			requirementDict.Add (type, num);
		}

		return requirementDict;
	}
}
