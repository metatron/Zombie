using UnityEngine;
using TouchScript;

public class CraftUiController : SingletonMonoBehaviourFast<CraftUiController> {
	public GameObject craftingPanel;

	public SwordDataTableObject _swordDataTableObj;
//	public FoodDataTableObject _foodDataTableObj;
//	public ToolDataTableObject _toolDataTableObj;
//	public MaterialDataTableObject _materialDataTableObj;

	public GameObject weaponDataTabViewportContent;


	public ItemUI itemUIPrefab;

	void Start() {
		//最初は消しておく
		craftingPanel.SetActive (false);

		//GameManagerでやってるが、Uiが出た段階でInitしてなかった場合Initする。
		if (!_swordDataTableObj.isInitialized()) {
			_swordDataTableObj.InitData();
		}

		InitSwordObjButton ();
	}

	public void OnOpenCraftMenuPressed() {
		craftingPanel.SetActive (true);
	}

	public void OnCloseCraftMenuPressed() {
		craftingPanel.SetActive (false);
	}


	private void InitSwordObjButton() {
		foreach (SwordData swordData in _swordDataTableObj.Table.All) {
			ItemUI initedItemUIObj = (ItemUI)Instantiate (itemUIPrefab);
			initedItemUIObj.InitItemMenu ("WeaponAtlas", swordData.Image, 1);
			initedItemUIObj.transform.SetParent (weaponDataTabViewportContent.transform, false);
			initedItemUIObj.SetImageSize ();
		}
	}

}