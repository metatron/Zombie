using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().AddForce (new Vector3 (-100.0f, 0.0f, 0.0f), ForceMode.Acceleration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
