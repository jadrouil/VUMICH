using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CompressedDrawing{
	
	public List<CompressedLine> lines;


	public CompressedDrawing(DrawingPreserver d){
		lines = new List<CompressedLine> ();
		foreach (LineRenderer lr in d.lines) {
			lines.Add (new CompressedLine (lr));
		
		}
	}
}



