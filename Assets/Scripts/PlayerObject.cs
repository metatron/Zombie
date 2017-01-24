using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerObject : MonoBehaviour {
	[SerializeField]
	private GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	[SerializeField]
	private SwordObject _swordObject; //instantiatedオブジェクト
	public SwordObject SwordObject { get { return _swordObject; } set { _swordObject = value; } }

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

	public void initPlayerSwordObject(string gunPrefabPath) {
		//剣の場合はweaponオブジェクトが既にあるのでパラメータのみの受け渡しOwnerだけ設定しておく
		//パラメータコピー
		SwordObject srcSwordObject = ((GameObject)Resources.Load (gunPrefabPath)).GetComponent<SwordObject>();
		System.Type type = srcSwordObject.GetType ();
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields) {
			field.SetValue (_swordObject, field.GetValue (srcSwordObject));
		}

		//Owerだけ上書き
		_swordObject.Owner = gameObject;
	}

}
