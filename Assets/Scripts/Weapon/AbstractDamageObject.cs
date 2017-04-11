using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractDamageObject : MonoBehaviour {
	[SerializeField]
	private AbstractWeaponObject _weapon;
	public AbstractWeaponObject WeaponObject { get { return _weapon; } set { _weapon = value; } }

	public virtual void CalculateDamage(EnemyObject enemyObject) {
		enemyObject.EnemyData.HP -= (int)System.Math.Ceiling (_weapon.Damage);
		Debug.LogError ("weapon: " + _weapon + ", dmg: " + _weapon.Damage + ", enemyHP: " + enemyObject.EnemyData.HP);

		//敵が死んだ場合の処理
		if (enemyObject.EnemyData.HP <= 0) {
			Debug.LogError ("@@@enemyObject: " + enemyObject);
			//ドロップはenemyObjectを破壊する前にコピーしておく。
			if (enemyObject.dropData != null) {
				DropData dropData = new DropData(enemyObject.dropData);
				int total = PlayerData.AddItem (dropData.GetDropItemData ());
				enemyObject.dropData = null; //メモリの解放

				//リザルト画面に必要なので各ドロップをセーブしておく。
				if (GameManager.Instance.CurrentStageObject.GetComponent<StageObject> ().DropItemNum.ContainsKey (dropData.ID)) {
					GameManager.Instance.CurrentStageObject.GetComponent<StageObject> ().DropItemNum [dropData.ID]++;
				}
				else {
					GameManager.Instance.CurrentStageObject.GetComponent<StageObject> ().DropItemNum.Add (dropData.ID, 1);
				}

				//ExpPoint保存
				PlayerData.unusedExpPoints += enemyObject.EnemyData.GetExpPoint();

				Debug.LogError ("@@@@@@@Dropping: " + dropData.ID + ": " + total + ", exp added: " + enemyObject.EnemyData.GetExpPoint() + ", unusedExp: " + PlayerData.unusedExpPoints);
			}

			bool isBoss = enemyObject.IsBoss;
			Destroy (enemyObject.gameObject);

			//ボスを倒した場合リザルト画面表示
			if (isBoss) {
				GameManager.Instance.InitResult ();
			}
		}
	}

//	protected virtual void OnCollisionEnter(Collision other) {
	void OnTriggerEnter(Collider other) {
		EnemyObject enemyObj = other.gameObject.GetComponent<EnemyObject> ();
		if (enemyObj != null) {
			CalculateDamage (enemyObj);
		}
		//Destroy (gameObject);
		gameObject.GetComponent<Collider> ().enabled = false;

		//弾は当たった瞬間消す。
		if(GetType() == typeof(BulletObject)) {
			Destroy(gameObject);
		}
		//剣の場合は数秒後に消す
		else {
			StartCoroutine (DestroyEffect());
		}
	}

	private IEnumerator DestroyEffect() {
		yield return new WaitForSeconds (3.0f);
		Destroy (gameObject);
	}
}
