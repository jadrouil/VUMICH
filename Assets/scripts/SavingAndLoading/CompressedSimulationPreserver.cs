using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CompressedSimulationPreserver{
	public List<CompressedDrawing> left;
	public List<CompressedDrawing> right;

	void compress(List<CompressedDrawing> compressed_side, List<DrawingPreserver> original){
		foreach (DrawingPreserver d in original) {
			compressed_side.Add (new CompressedDrawing (d));
		}

	
	}

	public CompressedSimulationPreserver(List<DrawingPreserver> left_original, List<DrawingPreserver> right_original){
		left = new List<CompressedDrawing> ();
		right = new List<CompressedDrawing> ();
		compress (left, left_original);
		compress (right, right_original);
	}
}



