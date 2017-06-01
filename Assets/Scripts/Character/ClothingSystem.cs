using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothingSystem : MonoBehaviour {
	public enum ClothParts : int {
		HAIR,			//0
		EYES,
		BODY,
		CHEST,
		ARM_L,
		ARM_R,
		HIP,			//6
		LEG_UPPER_L,    
		LEG_UPPER_R,
		LEG_LOWER_L,
		LEG_LOWER_R,	//10
		SHOES,


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

	//靴
	public List<SpriteRenderer> ShoesList = new List<SpriteRenderer>();

	/**
	 * 
	 * 服をセットする。
	 * 指定しない限りその部分は裸。
	 * 靴はidは関係ない。
	 * 
	 * ClothDataTableに登録されてないと表示されません。
	 * 
	 */
	public void SetClothParts(ClothParts type, string id, Color? color = null) {
		if (type == ClothParts.SHOES) {
			foreach (SpriteRenderer shoe in ShoesList) {
				shoe.color = color ?? Color.white;
			}
			return;
		}

		//データベースからデータを読み込み。
		string ID = GetClothingID (type, id);
		ClothData clothData = ClothDataTableObject.Instance.Table.All.FirstOrDefault(itemData => itemData.ID == ID);

		if (clothData == null) {
			Debug.LogError ("ERROR Cloth does not exists. TYPE: " + type + ", id: " + id);
			return;
		}

		GameObject clothObj = new GameObject ();
		clothObj.name = ID;

		GameObject baseObj = BaseObjList [(int)BaseObj.HIP]; //base is hip
		switch (type) {
		case ClothParts.HAIR:
		case ClothParts.EYES:
			baseObj = BaseObjList [(int)BaseObj.HEAD];
			break;
		case ClothParts.BODY:
		case ClothParts.CHEST:
			baseObj = BaseObjList [(int)BaseObj.SPINE];
			break;
		case ClothParts.ARM_L:
			baseObj = BaseObjList [(int)BaseObj.ARM_L];
			break;
		case ClothParts.ARM_R:
			baseObj = BaseObjList [(int)BaseObj.ARM_R];
			break;
		case ClothParts.HIP:
			baseObj = BaseObjList [(int)BaseObj.HIP];
			break;
		case ClothParts.LEG_UPPER_L:
			baseObj = BaseObjList [(int)BaseObj.LEG_UPPER_L];
			break;
		case ClothParts.LEG_UPPER_R:
			baseObj = BaseObjList [(int)BaseObj.LEG_UPPER_R];
			break;
		case ClothParts.LEG_LOWER_L:
			baseObj = BaseObjList [(int)BaseObj.LEG_LOWER_L];
			break;
		case ClothParts.LEG_LOWER_R:
			baseObj = BaseObjList [(int)BaseObj.LEG_LOWER_R];
			break;
		default:
			baseObj = BaseObjList [(int)BaseObj.HIP];
			break;
		}

		clothObj.transform.SetParent (baseObj.transform);
		clothObj.AddComponent<SpriteRenderer> ().sprite = GameManager.Instance.GetSpriteFromPath("ClothAtlas", ID);
		clothObj.GetComponent<SpriteRenderer> ().sortingOrder = clothData.OrderInLayer;
		clothObj.GetComponent<SpriteRenderer> ().color = color ?? Color.white;

		clothObj.transform.localScale = Vector3.one;
		clothObj.transform.localRotation = Quaternion.Euler (clothData.GetRotationVec ());
		clothObj.transform.localPosition = clothData.GetPositionVec ();
	}

	public static string GetClothingID(ClothParts type, string id) {
		switch (type) {
		case ClothParts.HAIR:
			return id + "_hair";
		case ClothParts.EYES:
			return id + "_eyes";
		case ClothParts.BODY:
			return id + "_body";
		case ClothParts.CHEST:
			return id + "_chest";
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
