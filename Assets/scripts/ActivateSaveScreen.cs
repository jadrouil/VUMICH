using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivateSaveScreen : MonoBehaviour {
	public static int savelayer = 11;
	public static int backgroundLayer = 12;
	public void activate(){
		//turn off old buttons turn on text field and continue button
		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			int layer = gameObject.transform.GetChild (i).gameObject.layer;
			if (layer == ActivateSaveScreen.savelayer || layer == ActivateSaveScreen.backgroundLayer)
				gameObject.transform.GetChild (i).gameObject.SetActive (true);
			else
				gameObject.transform.GetChild (i).gameObject.SetActive (false);
		}
		activateARMenu.save_screen_phase = true;
	}
}
