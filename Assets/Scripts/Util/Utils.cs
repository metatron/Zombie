using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Utils {

	/**
	 * 
	 * オブジェクトからwithNameの子オブジェクトをさがして１つだけ返す。
	 * 
	 */
	public static GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}

	/**
	 * 
	 * Listからランダムでエレメントを取得する。
	 * 
	 */
	public static T RandomElementAt<T>(IEnumerable<T> ie) {
		System.Random _Rand = new System.Random ((int) DateTime.Now.Ticks & 0x0000FFFF);
		return ie.ElementAt(_Rand.Next(ie.Count()));
	}


	/**
	 * 
	 * SpriteRendererのLayer及び、sortingLayerNameを置き換える。
	 * キャラ一覧の時などに使用。（UIの上にサムネがきてしまう）
	 * 
	 */
	public static void ChangeSpriteRendererLayer(GameObject obj, int layerNum, string sortingLayerName = null) {
		SpriteRenderer[] sprites = obj.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sprite in sprites) {
			sprite.gameObject.layer = layerNum;
			if (!string.IsNullOrEmpty (sortingLayerName)) {
				sprite.sortingLayerName = sortingLayerName;
			}
		}
	}
}
