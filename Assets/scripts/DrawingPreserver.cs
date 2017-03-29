using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class DrawingPreserver : MonoBehaviour {

	public struct corners{
		public Vector3 topRight;
		public Vector3 bottomLeft;
		public Vector3 bottomRight;
		public Vector3 topLeft;
		public float width;
		public float height;
		public Vector3 relativeOrigin;

		public void Refresh (){
			width = topRight.x - topLeft.x;
			height = topRight.y - bottomRight.y;
			relativeOrigin = (topLeft + bottomRight) * .5f;
		
		}
				

	};

	public swipeTrail drawing;
	public corners drawPlaneCorners;

	public LineRenderer[] lines;




	public bool exists(){
		return lines.Length > 0;
	
	}


	public void ProbeForDrawingSpace(){
		lines = new LineRenderer[drawing.all_lines.Count];
		lines = drawing.all_lines.ToArray();

		if (drawing.all_lines.Count == 0) {
			print ("No lines");

		} else {
			findCorners();
		}

		foreach(LineRenderer lr in lines){
			DontDestroyOnLoad (lr.gameObject);
			lr.gameObject.SetActive (false);
		}
		DontDestroyOnLoad (drawing);
		DontDestroyOnLoad (gameObject);

	}
		
	void findCorners(){
		print ("alt");
		Vector3[] imageCorners = new Vector3[4];
		RectTransform rt = GameObject.Find ("Graph").GetComponent<RectTransform> ();
		rt.GetWorldCorners (imageCorners);


		Vector3[] drawingPlaneCorn = new Vector3[4];
		for (int i = 0; i < 4; ++i) {
			drawingPlaneCorn[i] = drawing.convertToDrawingPlanePoint (Camera.main.WorldToScreenPoint(imageCorners[i]));
		}


		drawPlaneCorners.bottomRight = drawingPlaneCorn [3];
		drawPlaneCorners.topRight = drawingPlaneCorn [2];
		drawPlaneCorners.topLeft = drawingPlaneCorn [1];
		drawPlaneCorners.bottomLeft = drawingPlaneCorn [0];

		drawPlaneCorners.Refresh ();
		print ("end");
	}
}
