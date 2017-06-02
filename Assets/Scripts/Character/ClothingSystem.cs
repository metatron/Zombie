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

	private static List<Color> ColorList = new List<Color> () {
		Color.white,
		Color.red,
		Color.blue,
		Color.black,
		Color.cyan,
		Color.gray,
		Color.green,
		Color.yellow,
		Color.magenta
	};

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

	public static string GetClothingID(ClothParts type, string groupId) {
		switch (type) {
		case ClothParts.HAIR:
			return groupId + "_hair";
		case ClothParts.EYES:
			return groupId + "_eyes";
		case ClothParts.BODY:
			return groupId + "_body";
		case ClothParts.CHEST:
			return groupId + "_chest";
		case ClothParts.ARM_L:
			return groupId + "_leftarm";
		case ClothParts.ARM_R:
			return groupId + "_rightarm";
		case ClothParts.HIP:
			return groupId + "_lowerbody";
		case ClothParts.LEG_UPPER_L:
			return groupId + "_leftightsupper";
		case ClothParts.LEG_UPPER_R:
			return groupId + "_righttightsupper";
		case ClothParts.LEG_LOWER_L:
			return groupId + "_lefttightslower";
		case ClothParts.LEG_LOWER_R:
			return groupId + "_righttightslower";
		}

		Debug.LogError ("Error getting ClothData ID: " + groupId + ", Type: " + type);
		return "";
	}


	/**
	 * 
	 * キャラオブジェクトと性別を元に、
	 * 服の形と色をランダムに決める。
	 * 
	 * 返り値はセーブ用のデータ文字列。
	 * <箇所>@<服グループID>@<R>&<G>&<B>|
	 * 
	 * ex)
	 * hair@GroupID@r&g&b|eye@GroupID@r&g&b|....
	 * 
	 * 
	 */
	public static string AutoClothGenerator(ClothingSystem CharacterObject, CharaData.Gender gender = CharaData.Gender.Male) {
		//髪
		Color rndColor = RandomGenColor();
		ClothData clothData = RandomGenClothParts (ClothParts.HAIR, gender);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.HAIR, clothData.GroupID, rndColor);
		string clothDataStr = ClothParts.HAIR + "@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b + "|";

		//目
		rndColor = RandomGenColor();
		clothData = RandomGenClothParts (ClothParts.EYES, gender);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.EYES, clothData.GroupID, rndColor);
		clothDataStr += ClothParts.EYES + "@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b + "|";

		//胸
		if (gender == CharaData.Gender.Female) {
			rndColor = RandomGenColor ();
			clothData = RandomGenClothParts (ClothParts.CHEST, gender);
			CharacterObject.SetClothParts (ClothingSystem.ClothParts.CHEST, clothData.GroupID, rndColor);
			clothDataStr += ClothParts.CHEST + "@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b + "|";
		}

		//服
		rndColor = RandomGenColor ();
		clothData = RandomGenClothParts (ClothParts.BODY, gender);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.BODY, clothData.GroupID, rndColor);
		clothDataStr += ClothParts.BODY + "@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b + "|";

		//腕
		rndColor = RandomGenColor();
		clothData = RandomGenClothParts (ClothParts.ARM_L, gender);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.ARM_L, clothData.GroupID, rndColor);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.ARM_R, clothData.GroupID, rndColor);
		clothDataStr += "ARM@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b + "|";

		//腰
		rndColor = RandomGenColor();
		clothData = RandomGenClothParts (ClothParts.HIP, gender);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.HIP, clothData.GroupID, rndColor);
		clothDataStr += ClothParts.HIP + "@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b + "|";

		//足
		rndColor = RandomGenColor();
		clothData = RandomGenClothParts (ClothParts.LEG_UPPER_L, gender);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.LEG_UPPER_L, clothData.GroupID, rndColor);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.LEG_UPPER_R, clothData.GroupID, rndColor);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.LEG_LOWER_L, clothData.GroupID, rndColor);
		CharacterObject.SetClothParts (ClothingSystem.ClothParts.LEG_LOWER_R, clothData.GroupID, rndColor);
		clothDataStr += "LEG@" + clothData.GroupID + "@" + rndColor.r + "&" + rndColor.g + "&" + rndColor.b;

		Debug.LogError (clothDataStr);

		return clothDataStr;
	}

	private static ClothData RandomGenClothParts(ClothParts type, CharaData.Gender gender) {
		//ジェンダー別のGroupIDリスト取得
		List<string> groupIdList = ClothDataTableObject.Instance.GetGroupIdList (gender);

		//服のグループIDを取得
		int rnd = UnityEngine.Random.Range(0, groupIdList.Count);
		string groupId = groupIdList[rnd];

		//服のIDを取得
		string clothId = GetClothingID (type, groupId);

		//ClothDataを取得（あるかどうか確認)
		ClothData clothData = ClothDataTableObject.Instance.GetParams (clothId);

		if (clothData != null) {
			return clothData;
		}

		return null;
	}

	private static Color RandomGenColor() {
		int rnd = UnityEngine.Random.Range (0, ColorList.Count);
		return ColorList [rnd];
	}

}
