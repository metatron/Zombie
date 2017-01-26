using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerObject : AbstractCharacterObject {

	void Start() {
	}

	public void InitPlayer() {
		_animator = GetComponentInChildren<Animator> ();
		if (_animator == null) {
			Debug.LogError ("PlayerObject.Skeleton does not have Animator attached!");
			return;
		}
		_animator.Play ("stand");

		//ディフォルトは右向き
		FaceTo(AbstractCharacterObject.CharDirection.RIGHT);
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

	public void InitPlayerGunObject(string gunPrefabPath) {
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load (gunPrefabPath))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		_gunObject.transform.SetParent (transform);
		_gunObject.transform.localPosition = Vector3.zero;
	}

	public void InitPlayerSwordObject(string gunPrefabPath) {
		//剣の場合はweaponオブジェクトが既にあるのでパラメータのみの受け渡しOwnerだけ設定しておく
		//パラメータコピー
		SwordObject srcSwordObject = ((GameObject)Resources.Load (gunPrefabPath)).GetComponent<SwordObject>();
		srcSwordObject.CopyParamsTo (_swordObject);

		//コピー後にメモリ解放
		srcSwordObject = null;

		//Owerだけ上書き
		_swordObject.Owner = gameObject;

		//マーカー移動
		Vector3 markerPos = transform.position;
		markerPos.x += _swordObject.ReachLength;
		GameManager.Instance.swordReachMarker.transform.position = markerPos;
	}


	public void Attack() {
		if (this.SwordObject.CanReachEnemy ()) {
			this.SwordObject.Slash ();
		}
		else {
			this.GunObject.Fire();
		}
	}

}
