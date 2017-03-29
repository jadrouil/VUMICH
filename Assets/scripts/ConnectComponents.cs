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

		GameObject leftDraw = GameObject.Find ("_LeftDrawManager");

		if (leftDraw) {
			simulationManager.leftDrawing = leftDraw.GetComponent<DrawingPreserver> ();
			return true;
		}
		return false;
	
	}
	// Update is called once per frame
	void Update () {
		Camera lcam= GameObject.Find ("StereoCameraLeft").GetComponent<Camera>();
		Camera rcam = GameObject.Find ("StereoCameraRight").GetComponent<Camera> ();
		if (lcam && rcam && lcam.transform.position != rcam.transform.position && setUpDrawings()) {
			simulationManager.hscale = 3.67f;
			simulationManager.wscale = 1.60f;
			simulationManager.gameObject.SetActive (true);
			gameObject.SetActive (false);
		}
	}
}
