using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : AbstractWeaponObject {
	[SerializeField]
	private int maxAmmo;

	[SerializeField]
	private float rapidFireTime;	//1つ1つの弾を打つスピード

	[SerializeField]
	private float reloadTime;		//全弾打った後のリロードタイム

	override public void AttackAction() {
	}

}
