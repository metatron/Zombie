﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothingSystem : MonoBehaviour {
	public enum ClothParts : int {
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

	public enum BaseObj : int {
		HEAD,
		SPINE,
		ARM_L,
		ARM_R,
		HIP,
		LEG_UPPER_L,
		LEG_UPPER_R,
		LEG_LOWER_L,
		LEG_LOWER_R
	}

	//服を置くベースとなるボーンのリスト
	public List<GameObject> BaseObjList = new List<GameObject>();

	public void SetBodyParts(ClothParts type, string id) {
		string ID = GetClothingID (type, id);
		Debug.LogError ("####1: " + ID);
		ClothData clothData = ClothDataTableObject.Instance.Table.All.FirstOrDefault(itemData => itemData.ID == ID);
		Debug.LogError ("####2: " + clothData.Image);

		GameObject clothObj = new GameObject ();
		clothObj.name = ID;

		GameObject baseObj = BaseObjList [(int)BaseObj.HIP]; //base is hip
		switch (type) {
		case ClothParts.HAIR:
		case ClothParts.EYES:
			baseObj = BaseObjList [(int)BaseObj.HEAD];
			break;
		default:
			baseObj = BaseObjList [(int)BaseObj.HIP];
			break;
		}

		clothObj.transform.SetParent (baseObj.transform);
		clothObj.AddComponent<SpriteRenderer> ().sprite = GameManager.Instance.GetSpriteFromPath("ClothAtlas", ID);

		Debug.LogError ("####4: " + clothObj.GetComponent<SpriteRenderer>());
		clothObj.transform.localScale = Vector3.one;
		clothObj.transform.localRotation = Quaternion.Euler (clothData.GetRotationVec ());
		Debug.LogError ("####5: " + clothObj.transform.localRotation);
		clothObj.transform.localPosition = clothData.GetPositionVec ();
		Debug.LogError ("####6: " + clothObj.transform.localPosition);
	}

	public static string GetClothingID(ClothParts type, string id) {
		switch (type) {
		case ClothParts.HAIR:
			return id + "_hair";
		case ClothParts.EYES:
			return id + "_eyes";
		case ClothParts.BODY:
			return id + "_body";
		case ClothParts.ARM_L:
			return id + "_leftarm";
		case ClothParts.ARM_R:
			return id + "_rightarm";
		case ClothParts.HIP:
			return id + "_lowerbody";
		case ClothParts.LEG_UPPER_L:
			return id + "_leftightsupper";
		case ClothParts.LEG_UPPER_R:
			return id + "_righttightsupper";
		case ClothParts.LEG_LOWER_L:
			return id + "_lefttightslower";
		case ClothParts.LEG_LOWER_R:
			return id + "_righttightslower";
		}

		Debug.LogError ("Error getting ClothData ID: " + id + ", Type: " + type);
		return "";
	}
}
