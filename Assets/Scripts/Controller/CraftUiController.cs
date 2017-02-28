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

		InitSwordObjButton (weaponDataTabViewportContent);
	}

	public void OnOpenCraftMenuPressed() {
		craftingPanel.SetActive (true);
	}

	public void OnOpenCharacterMenuPressed() {
		characterPanel.SetActive (true);
		characterPanel.GetComponent<CharacterPanel> ().InitCharacterList ();
	}



	public void OnCloseCraftMenuPressed() {
		craftingPanel.SetActive (false);
	}

	public void InitSwordObjButton(GameObject content) {
		foreach (SwordData swordData in _swordDataTableObj.Table.All) {
			ItemUI initedItemUIObj = (ItemUI)Instantiate (itemUIPrefab);
			initedItemUIObj.InitItemMenu ("WeaponAtlas", swordData, 1);
			initedItemUIObj.transform.SetParent (content.transform, false);
			initedItemUIObj.SetItemImageSize ();
		}
	}

}