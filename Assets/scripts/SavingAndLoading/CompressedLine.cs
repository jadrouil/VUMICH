using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
}



