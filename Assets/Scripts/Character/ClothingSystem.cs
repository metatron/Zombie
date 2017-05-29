﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothingSystem : MonoBehaviour {
	public enum BaseParts : int {
		HAIR,			//0
		EYES,
		BODY,
		ARM_L,
		ARM_R,
		HIP,			//5
		LEG_UPPER_L,    
		LEG_UPPER_R,
		LEG_LOWER_L,
		LEG_LOWER_R,	//9


		NONE = 9999
	}

	//服を置くベースとなるボーンのリスト
	public List<GameObject> BodyBaseList = new List<GameObject>();

	public void SetBodyParts(BaseParts type, string id) {
		string ID = GetClothingID (type, id);
		ClothData clothData = ClothDataTableObject.Instance.Table.All.FirstOrDefault(itemData => itemData.ID == ID);

	}

	public static string GetClothingID(BaseParts type, string id) {
		switch (type) {
		case BaseParts.HAIR:
			return id + "_hair";
		case BaseParts.EYES:
			return id + "_eyes";
		case BaseParts.BODY:
			return id + "_body";
		case BaseParts.ARM_L:
			return id + "_leftarm";
		case BaseParts.ARM_R:
			return id + "_rightarm";
		case BaseParts.HIP:
			return id + "_lowerbody";
		case BaseParts.LEG_UPPER_L:
			return id + "_leftightsupper";
		case BaseParts.LEG_UPPER_R:
			return id + "_righttightsupper";
		case BaseParts.LEG_LOWER_L:
			return id + "_lefttightslower";
		case BaseParts.LEG_LOWER_R:
			return id + "_righttightslower";
		}

		Debug.LogError ("Error getting ClothData ID: " + id + ", Type: " + type);
		return "";
	}
}
