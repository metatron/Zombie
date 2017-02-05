using UnityEngine;
using TouchScript;

public class CraftUiController : SingletonMonoBehaviourFast<CraftUiController> {
	public GameObject craftingPanel;

	void Start() {
		//最初は消しておく
		craftingPanel.SetActive (false);
	}

	public void OnOpenCraftMenuPressed() {
		craftingPanel.SetActive (true);
	}

	public void OnCloseCraftMenuPressed() {
		craftingPanel.SetActive (false);
	}

}