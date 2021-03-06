﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


/**
 * 
 * 
 * http://qiita.com/kyubuns/items/bcbe92a18dffea684fbc
 * 
 * 
 */
public class AbstractMasterTable<T> where T : AbstractData, new() {
	protected List<T> masters;
	public List<T> All { get { return masters; } }

	protected string[] headerElements;
	public string[] HeaderElements { get { return headerElements; } }

	public void Load(string filePath) {
		var text = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
		text = text.Trim().Replace("\r", "") + "\n";
		var lines = text.Split('\n').ToList();

		// header
		headerElements = lines[0].Split(',');
		lines.RemoveAt(0); // header
		Debug.Log ("filePath: " + filePath + ", headerElements count: " + (headerElements.Length) + ", lines count: " + lines.Count);

		// body
		masters = new List<T>();
		foreach(var line in lines) ParseLine(line, headerElements);
	}

	private void ParseLine(string line, string[] headerElements) {
		var elements = line.Split(',');
		if(elements.Length == 1) return;
		if(elements.Length != headerElements.Length) {
			Debug.LogWarning(string.Format("can't load: {0}", line));
			return;
		}

		var param = new Dictionary<string, string>();
		for(int i=0;i<elements.Length;++i) param.Add(headerElements[i], elements[i]);
		var master = new T();
		master.Load(param);
		masters.Add(master);
	}
}


public class AbstractData {
	//プロジェクト毎に変更する値
	public string ID { get; protected set; }
	public string Name { get; protected set; }
	public string Image { get; protected set; }
	//exp: 
	//Sword1
	//metal:3|wood:1
	public string RequirementStr { get; private set; }

	/**
	 * 
	 * RequirementStrをIDずつの数に分けてDictionaryに纏めて返す。
	 * 
	 */
	public Dictionary<string, int> ParseRequirementStr() {
		if (RequirementStr == null || RequirementStr == "" || RequirementStr.Length == 0) {
			return null;
		}
		//<ID, 数>
		Dictionary<string, int> requirementDict = new Dictionary<string, int> ();

		string[] requirementList = RequirementStr.Split ('|');
		foreach (string requirementNumStr in requirementList) {
			string[] requirementNum = requirementNumStr.Split (':');
			string id = requirementNum [0];
			int num = Int32.Parse (requirementNum [1]);
			requirementDict.Add (id, num);
		}

		return requirementDict;
	}

	public void Load(Dictionary<string, string> param) {
		foreach(string key in param.Keys) SetField (key, param[key]);
	}

	private void SetField(string key, string value) {
		PropertyInfo propertyInfo = this.GetType().GetProperty(key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

		if(propertyInfo.PropertyType == typeof(int))         propertyInfo.SetValue(this, int.Parse(value), null);
		else if(propertyInfo.PropertyType == typeof(string)) propertyInfo.SetValue(this, value, null);
		else if(propertyInfo.PropertyType == typeof(double)) propertyInfo.SetValue(this, double.Parse(value), null);
		else if(propertyInfo.PropertyType == typeof(float)) propertyInfo.SetValue(this, float.Parse(value), null);
		// 他の型にも対応させたいときには適当にここに。enumとかもどうにかなりそう。
	}
}