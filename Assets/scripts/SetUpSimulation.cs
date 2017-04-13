using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpSimulation : MonoBehaviour {
	public DrawingPreserver.corners leftCamCorners;
	public DrawingPreserver.corners rightCamCorners;
	public List<DrawingPreserver> leftDrawings;
	public List<DrawingPreserver> rightDrawings;
	public float distanceFromCam;
	public float hscale;
	public float wscale;
	public bool loaded;
	// Use this for initialization
	DrawingPreserver.corners findCamCorners(Camera arcam){
		DrawingPreserver.corners crns = new DrawingPreserver.corners ();

		Transform transform = arcam.transform;
		Vector3 pos = transform.position;
		float halfFOV = (arcam.fieldOfView * .5f) * Mathf.Deg2Rad;
		float aspect = arcam.aspect;

		float height = Mathf.Tan (halfFOV) * distanceFromCam;
		float width = height * aspect ;

		height /= hscale;
		width /= wscale;


		//Lower Right
		crns.bottomRight = pos + transform.forward * distanceFromCam;
		crns.bottomRight += transform.right * width;
		crns.bottomRight -= transform.up * height;

		//Lower Left
		crns.bottomLeft = pos + transform.forward * distanceFromCam;
		crns.bottomLeft -= transform.right * width;
		crns.bottomLeft -= transform.up * height;

		//Upper Right
		crns.topRight = pos + transform.forward * distanceFromCam;
		crns.topRight += transform.right * width;
		crns.topRight += transform.up * height;

		//Upper Left
		crns.topLeft = pos + transform.forward * distanceFromCam;
		crns.topLeft -= transform.right * width;
		crns.topLeft += transform.up * height;

		crns.Refresh ();
		return crns;
	}

	void normalizeLineRenderer(LineRenderer lr, DrawingPreserver drawing, float half_width, float half_height){
		for (int j = 0; j < lr.numPositions; ++j) {
			Vector3 temp = lr.GetPosition (j);
			temp -= drawing.drawPlaneCorners.relativeOrigin;
			temp.x /= half_width;
			temp.y /= half_height;
			UnityEngine.Assertions.Assert.IsTrue (temp.x <= 1);
			UnityEngine.Assertions.Assert.IsTrue (temp.y <= 1);
			UnityEngine.Assertions.Assert.IsTrue (temp.x >= -1);
			UnityEngine.Assertions.Assert.IsTrue (temp.y >= -1);
			lr.SetPosition (j, temp);
		}
	}

	void normalize(DrawingPreserver drawing){
		float half_width = drawing.drawPlaneCorners.width * .5f;
		float half_height = drawing.drawPlaneCorners.height * .5f;
		for (int i = 0; i < drawing.lines.Length; ++i) {
			LineRenderer lr = drawing.lines [i];
			normalizeLineRenderer (lr, drawing, half_width, half_height);
		}
	
	}

	void translate(DrawingPreserver drawing, DrawingPreserver.corners corners){
		
		float half_width_scale = corners.width * .5f * drawing.scale;
		float half_height_scale = corners.height * .5f * drawing.scale;

		for (int i = 0; i < drawing.lines.Length; ++i) {
			LineRenderer lr = drawing.lines [i];
			translateLine (lr, half_width_scale, half_height_scale, corners.relativeOrigin);
		
		}

	}

	void translateLine (LineRenderer lr, float x_scale, float y_scale, Vector3 newOrigin){
		for (int j = 0; j < lr.numPositions; ++j) {
			Vector3 temp = lr.GetPosition (j);
			temp.x *= x_scale;
			temp.y *= y_scale;
			temp += newOrigin;
			lr.SetPosition (j, temp);
		}


	}

	void scaleAndEnable(DrawingPreserver drawing, DrawingPreserver.corners corners){
		float line_width_scale = corners.width * corners.height / drawing.drawPlaneCorners.width / drawing.drawPlaneCorners.height * hscale * wscale;
		line_width_scale *= drawing.scale;
	

		for (int i = 0; i < drawing.lines.Length; ++i) {
			LineRenderer lr = drawing.lines [i];

			lr.widthMultiplier *= line_width_scale;
			lr.gameObject.SetActive (true);
		}
	}

	void draw(DrawingPreserver.corners corns, List<DrawingPreserver> drawings){
		foreach (DrawingPreserver d in drawings) {
			if (!loaded) {
				normalize (d);
				translate (d, corns);
				scaleAndEnable (d, corns);
			}
		}
	
	}

	void Start () {

		Camera lcam= GameObject.Find ("StereoCameraLeft").GetComponent<Camera>();
		Camera rcam = GameObject.Find ("StereoCameraRight").GetComponent<Camera> ();
		if (leftDrawings.Count > 0) {
			leftCamCorners = findCamCorners (lcam);
			draw (leftCamCorners, leftDrawings);

		}

		if (rightDrawings.Count > 0) {
			rightCamCorners = findCamCorners (rcam);
			draw (rightCamCorners, rightDrawings);
		
		}

	}


}
