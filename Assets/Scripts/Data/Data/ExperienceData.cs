using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceData: AbstractData {
	public string Level { get; set; } //ExperienceDataTableObjectでのIDがstringで読み込むようになってるため
	public int Exp { get; set; }
}
