﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerObject : MonoBehaviour {
	[SerializeField]
	private GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	private Animator _animator;

	public enum CharDirection : int {
		LEFT,
		RIGHT
	}

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
		FaceTo(CharDirection.RIGHT);
	}

	public void Play(string anim) {
		_animator.Play (anim, -1, 0.0f);
	}

	public void FaceTo(CharDirection dir) {
		if (dir == CharDirection.LEFT) {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		} else {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 180.0f, 0.0f));
		}
	}

	public void initPlayerGunObject(string gunPrefabPath) {
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load (gunPrefabPath))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		_gunObject.transform.SetParent (transform);
		_gunObject.transform.localPosition = Vector3.zero;
	}
}
