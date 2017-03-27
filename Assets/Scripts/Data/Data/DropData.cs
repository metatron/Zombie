using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropData {
	public string ID { get; set; }
	public float Percentage { get; set; }

	/**
	 * 
	 * コピーを作成。
	 * 敵が倒された際にそのデータを失わない為に敵の持っているDropData
	 * をコピーして持っておく。
	 * 
	 */
	public DropData(DropData dropData) {
		ID = dropData.ID;
		Percentage = dropData.Percentage;
	}


	/**
	 * 
	 * EnemyObjectが倒される時に呼ばれる。
	 * DropData.IDでCraftItemDataTableObjectのリストを検索して
	 * PlayerDataに加える。
	 * 
	 */
	public AbstractData GetDropItemData() {
		CraftItemData craftItem = CraftItemDataTableObject.Instance.Table.All.FirstOrDefault(itemData => itemData.ID == ID);
		return craftItem;
	}
}
