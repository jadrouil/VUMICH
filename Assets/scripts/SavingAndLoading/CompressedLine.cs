using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class CompressedLine{
	public float width;
	//how transparent line is
	public float alpha;

	public List<CompressedPt> pts;

	public CompressedLine(LineRenderer lr){
		GameObject line = lr.gameObject;
		alpha = lr.material.GetColor ("_Color").a;
		width = lr.startWidth;
		pts = new List<CompressedPt> ();
		for (int i = 0; i < lr.numPositions; ++i) {
			pts.Add (new CompressedPt(lr.GetPosition (i)));
		}
	}
	public Vector3[] v3(){
		Vector3[] res = new Vector3[pts.Count];
		for (int i = 0; i < pts.Count; ++i) {
			res [i] = new Vector3 (pts[i].x , pts[i].y, pts[i].z);
//			MonoBehaviour.print (res [i]);
		}
		return res;
	}
}



