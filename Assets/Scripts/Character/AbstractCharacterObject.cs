using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterObject : MonoBehaviour {
	protected Animator _animator;

	public enum CharDirection : int {
		LEFT,
		RIGHT
	}

	public CharaData charaData = new CharaData();

	[SerializeField]
	protected GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	[SerializeField]
	protected SwordObject _swordObject; //referredオブジェクト
	public SwordObject SwordObject { get { return _swordObject; } set { _swordObject = value; } }

	//ここにGunやswordをおく。
	private GameObject weaponPosObj;

	public void Play(string anim) {
		_animator.Play (anim, -1, 0.0f);
	}

	public void InitChar(CharaData charaData, AbstractCharacterObject.CharDirection dir=AbstractCharacterObject.CharDirection.RIGHT) {
		this.charaData = charaData;
		//Animatorオブジェクトを見つける
		_animator = GetComponentInChildren<Animator> ();
		if (_animator == null) {
			Debug.LogError ("PlayerObject.Skeleton does not have Animator attached!");
			return;
		}
		_animator.Play ("stand");

		//weaponオブジェクトを見つける。（ソード持ってる手のweaponオブジェクト）
		if (weaponPosObj == null) {
			weaponPosObj = Utils.getChildGameObject (gameObject, "weapon");
		}
		_swordObject = weaponPosObj.GetComponent<SwordObject>();

		//服を着せる
		if (GetComponent<ClothingSystem> () != null) {
			GetComponent<ClothingSystem> ().SetClothPartsByStringData (charaData.ClothDataStr);
		}

		//ディフォルトは右向き
		FaceTo(dir);

		//描画順設定
		SetSortingLayer (GetSortingLayerName());
	}

	public void FaceTo(CharDirection dir) {
		if (dir == CharDirection.LEFT) {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		} else {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 180.0f, 0.0f));
		}
	}

	private string GetSortingLayerName() {
		string sortingLayerName = "Default";
		//プレイヤーの隣はプレイヤーの手前
		if (charaData.BattlePosition == 0) {
			sortingLayerName = "NpcPos1";
		} 
		//BattlePosition <= MAX_WALL_NUM/2の場合は奥
		else if (charaData.BattlePosition > 0 && charaData.BattlePosition <= (WallController.MAX_WALL_NUM / 2)) {
			sortingLayerName = "NpcPos3";
		}
		//6以上10以下
		else if (charaData.BattlePosition > (WallController.MAX_WALL_NUM / 2) && charaData.BattlePosition <= WallController.MAX_WALL_NUM) {
			sortingLayerName = "NpcPos2";
		}

		return sortingLayerName;
	}

	private void SetSortingLayer(string sortingLayerName) {
		foreach (SpriteRenderer spRenderer in GetComponentsInChildren<SpriteRenderer>()) {
			spRenderer.sortingLayerName = sortingLayerName;
		}
	}

	public void InitCharGunObject(string gunId) {
		if (string.IsNullOrEmpty (gunId)) {
			return;
		}

		GunData gunData = GunDataTableObject.Instance.GetParams (gunId);
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/Items/Guns/"+gunData.GunPrefab))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		//ソードを持ってる手のgameObjectを取得
		if (weaponPosObj == null) {
			weaponPosObj = Utils.getChildGameObject (gameObject, "weapon");
		}
		_gunObject.transform.SetParent (weaponPosObj.transform);
		_gunObject.transform.localPosition = Vector3.zero;

		//ポジションとレンダーオーダーを整える。
		_gunObject.transform.localRotation = Quaternion.Euler( new Vector3(0.0f, 0.0f, 90.0f));
		_gunObject.GetComponent<SpriteRenderer> ().sortingLayerName = GetSortingLayerName();
		_gunObject.GetComponent<SpriteRenderer> ().sortingOrder = 59;

		_gunObject.CopyParamsFrom (gunId);

		//gunをdisable
		_gunObject.DisplaySprite(false);

	}

	public void InitCharSwordObject(string swordId) {
		if (string.IsNullOrEmpty (swordId)) {
			return;
		}

		//剣の場合はweaponオブジェクトが既にあるのでパラメータのみの受け渡しOwnerだけ設定しておく
		//パラメータコピー
		_swordObject.CopyParamsFrom (swordId);

		//Ower上書き
		_swordObject.Owner = gameObject;

		//自キャラの場合はマーカー移動
		if (gameObject.GetComponent<PlayerObject> () != null) {
			Vector3 markerPos = transform.position;
			markerPos.x += _swordObject.ReachLength;
			GameManager.Instance.swordReachMarker.transform.position = markerPos;
		}
	}


	public void Attack() {
		//Swordオブジェクトはキャラに既に付いているのでNullチェックだけでは装備してるかわからない。
		//Owner、SwingEffectをチェック
		if (this.SwordObject != null && SwordObject.SwingEffectPrefab != null && this.SwordObject.CanReachEnemy ()) {
			SwordObject.DisplaySprite (true);
			if (GunObject != null) {
				GunObject.DisplaySprite (false);
			}
			SwordObject.Slash ();
		}
		else if (this.GunObject != null) {
			if (SwordObject != null) {
				SwordObject.DisplaySprite (false);
			}
			GunObject.DisplaySprite (true);
			GunObject.Fire();
		}
	}
}
