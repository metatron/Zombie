using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerObject : MonoBehaviour {
	[SerializeField]
	private GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	public void initPlayerGunObject(string gunPrefabPath) {
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load (gunPrefabPath))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		_gunObject.transform.SetParent (transform);
		_gunObject.transform.localPosition = Vector3.zero;
	}
}
