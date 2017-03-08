using UnityEngine;
using TouchScript;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CraftUiController : SingletonMonoBehaviourFast<CraftUiController> {
	public GameObject craftingPanel;
	public GameObject createPanel;

	public GameObject characterPanel;
	public GameObject statusPanel;


	public SwordDataTableObject _swordDataTableObj;
//	public FoodDataTableObject _foodDataTableObj;
//	public ToolDataTableObject _toolDataTableObj;
	public CraftItemDataTableObject _craftItemDataTableObj;

	public GameObject weaponDataTabViewportContent;
	public GameObject craftItemDataTabViewportContent;


	public ItemUI itemUIPrefab;

	void Start() {
		//最初は消しておく
		craftingPanel.SetActive (false);
		createPanel.SetActive (false);
		characterPanel.SetActive (false);
		statusPanel.SetActive (false);

		//GameManagerでやってるが、Uiが出た段階でInitしてなかった場合Initする。
		if (!_swordDataTableObj.isInitialized()) {
			_swordDataTableObj.InitData();
		}
		if (!_craftItemDataTableObj.isInitialized()) {
			_craftItemDataTableObj.InitData();
		}

	}

	public void OnOpenCraftMenuPressed() {
		//初期化
		ResetContent(weaponDataTabViewportContent);
		InitItemObjButton<SwordData> (weaponDataTabViewportContent,
			//ボタンが押された時の挙動を追加
			(AbstractData itemData) => {
				CraftUiController.Instance.createPanel.GetComponent<CreatePanel> ().InitItemCraftingData (itemData);
			}
		);

		ResetContent(craftItemDataTabViewportContent);
		InitItemObjButton<CraftItemData> (craftItemDataTabViewportContent,
			//ボタンが押された時の挙動を追加
			(AbstractData itemData) => {
				CraftUiController.Instance.createPanel.GetComponent<CreatePanel> ().InitItemCraftingData (itemData);
			}
		);


		craftingPanel.SetActive (true);
	}

	public void OnOpenCharacterMenuPressed() {
		characterPanel.SetActive (true);
		characterPanel.GetComponent<CharacterPanel> ().InitCharacterList ();
	}



	public void OnCloseCraftMenuPressed() {
		craftingPanel.SetActive (false);
	}

	public void InitItemObjButton<T>(GameObject content, ItemUI.ClickItemAction clickItemAction) where T: AbstractData {
		List<AbstractData> itemList = new List<AbstractData>();
		string atlasName = "WeaponAtlas";
		if(typeof(T) == typeof(SwordData)) {
			//List<SwordData>をList<AbstractData>にConvert。
			itemList = _swordDataTableObj.Table.All.ConvertAll<AbstractData> (x => (AbstractData)x);
			atlasName = "WeaponAtlas";
		}

		//ItemUIをInstantiateし、値をセットし、contentに追加。
		foreach (AbstractData itemData in itemList) {
			ItemUI initedItemUIObj = (ItemUI)Instantiate (itemUIPrefab);
			//「使用可能な数」を表示
			int totalCnt = PlayerData.GetItemNum(itemData.ID);
			int equippedCnt = PlayerData.TotalEquipedNum (itemData.ID);

			Debug.LogError (itemData.ID + ", totalCnt: " + totalCnt + ", equippedCnt: " + equippedCnt);

			initedItemUIObj.InitItemMenu (atlasName, itemData, "" + totalCnt);
			initedItemUIObj.transform.SetParent (content.transform, false);
			initedItemUIObj.SetItemImageSize ();

			//ボタンを押した際はクラフト、装備、（食べ物の場合は）食べるアクションをとる。
			initedItemUIObj._clickItemAction = clickItemAction;
		}
	}


	/**
	 * 
	 * content配下にある全てのTransformを消す。
	 * 
	 * 
	 */
	public void ResetContent(GameObject content) {
		Transform[] itemUIButtonArray = content.GetComponentsInChildren<Transform> ();
		int length = itemUIButtonArray.Length;
		for(int i=0; i<length; i++) {
			Transform itemUIBtn = itemUIButtonArray [i];
			//content自身は消さない
			if (itemUIBtn.name == "Content") {
				continue;
			}

			Destroy (itemUIBtn.gameObject);
		}
		itemUIButtonArray = null;
	}

	public void ResetAllContent() {
		//Sword初期化
		ResetContent(weaponDataTabViewportContent);
		InitItemObjButton<SwordData> (weaponDataTabViewportContent,
			//ボタンが押された時の挙動を追加
			(AbstractData itemData) => {
				CraftUiController.Instance.createPanel.GetComponent<CreatePanel> ().InitItemCraftingData (itemData);
			}
		);

	}
}