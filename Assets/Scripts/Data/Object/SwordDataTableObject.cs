using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDataTableObject : MonoBehaviour {
	private SwordDataTable _table = new SwordDataTable();
	public SwordDataTable Table { get { return _table; } set {_table = value; } }


	public void InitData() {
		_table.Load ();
	}


}
