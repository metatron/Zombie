using UnityEngine;
using TouchScript;
using UnityEngine.UI;

public class CraftUiController : SingletonMonoBehaviourFast<CraftUiController> {
	public GameObject craftingPanel;
	public GameObject createPanel;

	public GameObject characterPanel;
	public GameObject statusPanel;


	public SwordDataTableObject _swordDataTableObj;
//	public FoodDataTableObject _foodDataTableObj;
//	public ToolDataTableObject _toolDataTableObj;
//	public MaterialDataTableObject _materialDataTableObj;

	public GameObject weaponDataTabViewportContent;


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

	}

	public void OnOpenCraftMenuPressed() {
		//初期化
		ResetContent(weaponDataTabViewportContent);
		InitSwordObjButton (weaponDataTabViewportContent,
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

	public void InitSwordObjButton(GameObject content, ItemUI.ClickItemAction clickItemAction) {
		foreach (SwordData swordData in _swordDataTableObj.Table.All) {
			ItemUI initedItemUIObj = (ItemUI)Instantiate (itemUIPrefab);
			//「使用可能な数」を表示
			int totalCnt = PlayerData.GetItemNum(swordData.ID);
			int equippedCnt = PlayerData.TotalEquipedNum (swordData.ID);

			//装備してる数が多い場合は0
			int count = Mathf.Max (0, (totalCnt - equippedCnt));
			Debug.LogError (swordData.ID + ", totalCnt: " + totalCnt + ", equippedCnt: " + equippedCnt + ", count: " + count);

			initedItemUIObj.InitItemMenu ("WeaponAtlas", swordData, "" + count);
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
		InitSwordObjButton (weaponDataTabViewportContent,
			//ボタンが押された時の挙動を追加
			(AbstractData itemData) => {
				CraftUiController.Instance.createPanel.GetComponent<CreatePanel> ().InitItemCraftingData (itemData);
			}
		);

	}
}