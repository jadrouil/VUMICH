using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateText : MonoBehaviour {

	public void updateTargetText(float fovlevel){
		Text target = GameObject.Find ("TargetText").GetComponent<Text> ();
		target.text = "Degrees" + " " +(5 + fovlevel * 5).ToString ();
	}
}
