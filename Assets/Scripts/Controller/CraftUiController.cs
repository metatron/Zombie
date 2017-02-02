using UnityEngine;
using TouchScript;

public class CraftUiController : SingletonMonoBehaviourFast<CraftUiController> {
	public GameObject craftingPanel;


	public void OnOpenCraftMenuPressed() {
		craftingPanel.SetActive (true);
	}

	public void OnCloseCraftMenuPressed() {
		craftingPanel.SetActive (false);
	}

}