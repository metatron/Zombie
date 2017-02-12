using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordData : AbstractData {
	public string ID { get; private set; }
	public string Name { get; private set; }
	public float Damage { get; private set; }
	public float ReachLength { get; private set; }
	public string Image { get; private set; }
	public string SwingEffect { get; private set; }

	//exp: 
	//Sword1
	//Metal:3|Wood:1
	public string RequirementStr { get; private set; }

	//上記、RequirementListStrをジェネリックリスト化
	public List<AbstractData.RequirementData> requirementDataList = new List<AbstractData.RequirementData>();

	public void InitSwordData(string[] datas) {
		ID				= (string)datas [0];
		Name			= (string)datas [1];
		Damage			= float.Parse(datas [2]);
		ReachLength		= float.Parse(datas [3]);
		Image			= (string)datas [4];
		SwingEffect		= (string)datas [5];
		RequirementStr	= (string)datas [6];
	}

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
