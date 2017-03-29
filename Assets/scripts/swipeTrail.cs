using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class swipeTrail : MonoBehaviour {

	public float line_width;
	public float shading = 0f;
	public float threshold = 13f;
	public GameObject linePrefab;
	Plane objplane;
	private Vector3 lastpt = Vector3.one * float.MaxValue;
	Vector3 startPos;

	GameObject thisTrail;

	LineRenderer lr; 

	public Stack<LineRenderer> all_lines = new Stack<LineRenderer>();



	void start(){
		
	}

	public void setWidth(float width){
		line_width = width;
	}

	public void undo(){
		if(all_lines.Count > 0)
			Destroy (all_lines.Pop());
	
	}

	public void setShading(float new_val){
		shading = new_val;
	}

	Color getColor(){
		Color mine = new Color (0, 0, 0, 1 - shading);
		return mine;
	}

	bool firstTouch(){
		bool fingerTouch = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began;
		bool mouseTouch =  Input.GetMouseButtonDown(0);
		return fingerTouch || mouseTouch;
	}

	bool continuingTouch(){
		bool fingerTouch = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved;
		bool mouseTouch = Input.GetMouseButton (0) && !Input.GetMouseButtonDown (0) && !Input.GetMouseButtonUp(0);
		return fingerTouch || mouseTouch;
	}

	void Update () {

		objplane =  new Plane (Camera.main.transform.forward * -1, this.transform.position);
	

		if (firstTouch() && inBounds(Input.mousePosition)) {
			startNewLine ();

		} 
		else if (continuingTouch() && inBounds(Input.mousePosition)) {
			continueLine ();
		}


	}

	public Vector3 convertToDrawingPlanePoint(Vector3 pt){
		Ray mRay = Camera.main.ScreenPointToRay (pt);

		float rayDistance;

		if (objplane.Raycast (mRay, out rayDistance))
			return mRay.GetPoint (rayDistance);

		return new Vector3 (-1, -1, -1);
	}

	void startNewLine(){
		thisTrail = (GameObject)Instantiate (linePrefab,
			this.transform.position,
			Quaternion.identity);

		lastpt = Vector3.one * float.MaxValue;

		startPos = convertToDrawingPlanePoint (Input.mousePosition);

		lr = thisTrail.GetComponent<LineRenderer> ();

		lr.startWidth = line_width;
		lr.endWidth = line_width;
//		lr.startColor = getColor ();
//		lr.endColor = getColor ();
//		lr.material.color = getColor();
		print(getColor());
		lr.material.SetColor ("_Color", getColor ());
		lr.material.SetColor ("_EmissionColor", getColor ());
		//lr.material.SetColor ("_Color", getColor());
		

		addPoint (lr, startPos);
		all_lines.Push (lr);
	
	}

	void continueLine(){
		Ray mRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		float rayDistance;
		if (objplane.Raycast (mRay, out rayDistance)) {
			Vector3 pt = mRay.GetPoint (rayDistance);
			addPoint (lr, pt);
		}
	}

	void addPoint(LineRenderer line, Vector3 pt){
		if (meetsThreshold (pt)) {
			int current_num = line.numPositions++;
			line.SetPosition (current_num, pt);
		}
	}

	bool meetsThreshold(Vector3 pt){
		float dist = Vector3.Distance(lastpt, pt);
		return dist > threshold;

	}



	public bool inBounds(Vector3 pt){
		PointerEventData pe = new PointerEventData(EventSystem.current);
		pe.position = pt;

		List<RaycastResult> hits = new List<RaycastResult>();
		EventSystem.current.RaycastAll( pe, hits );

		bool hit = false;
		foreach(RaycastResult h in hits)
		{
			GameObject g = h.gameObject;
			hit = ( g.name == "Graph" &&
				(g.GetComponent<UnityEngine.UI.Image>() ||
					g.GetComponent<Canvas>() ||
					g.GetComponent<UnityEngine.UI.InputField>())
			);
			if(hit)
			{
				return true;
			}
		}
		return false;
	
	}
		
}
