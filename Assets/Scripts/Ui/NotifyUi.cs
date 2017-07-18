using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyUi : MonoBehaviour {
	public Image notifyImage;
	public Text notifyInfo;

	public void Initialize(string info, Image img = null) {
		notifyInfo.text = info;

		if (img == null) {
			notifyImage.gameObject.SetActive (false);
		}
	}
}
