using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpDrawingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SimulationConfig sc = GameObject.Find ("GlobalGameManager").GetComponent<SimulationConfig> ();
		GameObject graphs = GameObject.Find ("GraphCollection");

		for (int i = 0; i < graphs.transform.childCount; ++i) {
			GameObject child = graphs.transform.GetChild (i).gameObject;
			child.SetActive (child.name == sc.mode);
		
		}

		swipeTrail swiper = GameObject.Find ("DrawingManager").GetComponent<swipeTrail> ();
		swiper.mode = sc.mode;
	}

}
