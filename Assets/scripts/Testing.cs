using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera arcam= GameObject.Find ("StereoCameraLeft").GetComponent<Camera>();
		print(arcam.aspect);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
