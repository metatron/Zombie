using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerObject : MonoBehaviour {
	[SerializeField]
	private GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	private Animator _animator;
	private Skeleton _skeleton;

	public GameObject weaponHandObject;

	void Start() {
	}

	public void initPlayer() {
		_animator = GetComponentInChildren<Animator> ();
		if (_animator == null) {
			Debug.LogError ("PlayerObject.Skeleton does not have Animator attached!");
			return;
		}
		_animator.Play ("stand");

		//ディフォルトは右向き
		_skeleton = GetComponentInChildren<Skeleton> ();
		_skeleton.flipY = true;
	}

	public void Play(string anim) {
		_animator.Play (anim);
	}

	public void initPlayerGunObject(string gunPrefabPath) {
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load (gunPrefabPath))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		_gunObject.transform.SetParent (transform);
		_gunObject.transform.localPosition = Vector3.zero;
	}
}
