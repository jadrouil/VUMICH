using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingCollector : MonoBehaviour {

	List<DrawingPreserver> left_eye_filters;
	List<DrawingPreserver> right_eye_filters;


	void Start(){
		DontDestroyOnLoad (gameObject);
		left_eye_filters = new List<DrawingPreserver>();
		right_eye_filters = new List<DrawingPreserver>();
	}

	void DestroyPreservers(List<DrawingPreserver> ds){
		foreach (DrawingPreserver d in ds) {
			d.prepForDestruction ();
			Destroy (d.gameObject);
		}
	}

	public void Reset(){
		DestroyPreservers(left_eye_filters);
		DestroyPreservers(right_eye_filters);

	}

	public void add_drawing(DrawingPreserver drawing_to_preserve){
		bool left_eye = gameObject.GetComponent<SimulationConfig> ().next_drawing_is_left_eye;
		if (left_eye) {
			left_eye_filters.Add (drawing_to_preserve);
		} else {
			right_eye_filters.Add (drawing_to_preserve);
		}
	}
	public List<DrawingPreserver> left(){
		return left_eye_filters;
	}

	public List<DrawingPreserver> right(){
		return right_eye_filters;
	}


}