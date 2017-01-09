using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	[SerializeField]
	private GameObject playerObject;
	public GameObject PlayerObject { get { return playerObject; } set { playerObject = value; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
