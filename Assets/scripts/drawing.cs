using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawing : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{       
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			transform.position = mousePos;            
		}   
	}
}
