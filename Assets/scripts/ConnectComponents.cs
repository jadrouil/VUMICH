using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectComponents : MonoBehaviour {
	public SetUpSimulation simulationManager;
	public bool debugOnlyThisSceneMode;

	// Use this for initialization
	void Start () {
		if (debugOnlyThisSceneMode) {
			simulationManager.gameObject.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	bool setUpDrawings(){

		GameObject manager = GameObject.Find ("GlobalGameManager");

		if (manager) {
			simulationManager.leftDrawings = manager.GetComponent<DrawingCollector> ().left();
			simulationManager.rightDrawings = manager.GetComponent<DrawingCollector> ().right ();
			return true;
		}
		return false;
	
	}
	// Update is called once per frame
	void Update () {
		
		GameObject lcam = GameObject.Find ("StereoCameraLeft");
		GameObject rcam = GameObject.Find ("StereoCameraRight");
		if (lcam && rcam && lcam.transform.position != rcam.transform.position && setUpDrawings()) {
			simulationManager.hscale = 3.67f;
			simulationManager.wscale = 1.60f;
			simulationManager.gameObject.SetActive (true);
			gameObject.SetActive (false);
		}
	}
}
