using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateARMenu : MonoBehaviour {

	// Use this for initialization
	bool first_touch_detected = false;
	//time at start ... = time since last click or last time the window was too large, whichever is smaller
	float time_at_start_of_window;
	bool menu_activated = false;

	public float time_between_clicks;



	void setEachMenuItemTo(bool state){
	
		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			gameObject.transform.GetChild (i).gameObject.SetActive (state);
		
		
		}
//
//		foreach (RectTransform child in gameObject.GetComponentsInChildren<RectTransform>()) {
//			child.gameObject.SetActive (state);
//			gameObject.transform.chil
//		}
	
	}

	void Start () {
		setEachMenuItemTo (false);
	}

	bool FirstClick(){
		bool fingerTouch = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began;
		bool mouseTouch =  Input.GetMouseButtonDown(0);
		return (fingerTouch || mouseTouch) && !first_touch_detected;
	}
		

	bool TooLongSinceLastClick(){
		return  (Time.time - time_at_start_of_window >= time_between_clicks);


	
	}
	bool NeverClicked(){
		bool nofingerTouch = Input.touchCount == 0;
		bool noMouseTouch = !Input.GetMouseButtonDown (0);
	
		return nofingerTouch && noMouseTouch && !first_touch_detected;
	}

	bool SecondClick(){
		bool fingerTouch = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began;
		bool mouseTouch = Input.GetMouseButtonDown (0);

		return (fingerTouch || mouseTouch) && first_touch_detected;
	
	}


	// Update is called once per frame
	void Update () {
		if (NeverClicked () || TooLongSinceLastClick()) {
			first_touch_detected = false;
			time_at_start_of_window = Time.time;
			print ("here");
		} else if (FirstClick ()) {
			first_touch_detected = true;
			time_at_start_of_window = Time.time;
			print ("first");
		} else if (SecondClick()){
			//meets criteria for double click
			print("second");
			menu_activated = !menu_activated;
			setEachMenuItemTo(menu_activated);
			first_touch_detected = false;
			time_at_start_of_window = Time.time;
		}
		

		
	}
}
