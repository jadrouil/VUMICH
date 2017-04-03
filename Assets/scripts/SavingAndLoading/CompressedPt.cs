using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CompressedPt{

	public float x;
	public float y;
	public float z;


	public CompressedPt(Vector3 v){
		x = v.x;
		y = v.y;
		z = v.z;
	}
}



