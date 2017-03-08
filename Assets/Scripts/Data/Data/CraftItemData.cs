using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemData : AbstractData {
	public string ID { get; private set; }
	public string Name { get; private set; }
	public string ImgPath { get; private set; }
	public string RequirementStr { get; private set; }
}
