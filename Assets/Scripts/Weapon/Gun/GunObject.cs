using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunObject : MonoBehaviour {
	[SerializeField]
	private BulletObject bullet;

	private int maxAmmo;
	private float fireTime;		//1つ1つの弾を打つスピード
	private float reloadTime;	//全弾打った後のリロードタイム


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
