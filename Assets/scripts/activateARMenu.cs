using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activateARMenu : MonoBehaviour {
	public int menu_layer1 = 13;
	public int save_layer = 11;
	public int backgroundLayer = 12;
	public int status_layer = 14;

	// Use this for initialization
	bool first_touch_detected = false;
	//time at start ... = time since last click or last time the window was too large, whichever is smaller
	float time_at_start_of_window;

	enum ScreenState {OFF,MAINMENU, SAVEMENU, STATUSMENU};
	ScreenState currentState = ScreenState.OFF;


	public float time_between_clicks;

	GameObject findChildWithName(string name){
		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			GameObject go = gameObject.transform.GetChild (i).gameObject;
			if (go.name == name) {
				return go;
			}
		}
		return null;
	}


	public void displaySaveStatus(){
		saveSimulation saver = GameObject.Find ("_SimulationManager").GetComponent<saveSimulation> ();
		string status = saver.status_msg;
		GameObject textobj = findChildWithName ("Status");

		textobj.GetComponent<Text> ().text = status;
			
		setEachItemInLayerTo(save_layer, false);
		setEachItemInLayerTo (status_layer, true);

		currentState = ScreenState.STATUSMENU;
	}


	public void displaySaveScreen(){

		setEachItemInLayerTo (menu_layer1, false);
		setEachItemInLayerTo (save_layer, true);
	
		currentState = ScreenState.SAVEMENU;
	}

	void setEachItemInLayerTo(int layer, bool state){
		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			Transform child = gameObject.transform.GetChild (i);
			string name = child.gameObject.name;
			if (name == "RawImage" || child.gameObject.layer == layer)
				child.gameObject.SetActive (state);
		}
	
	}

	void registerDoubleClick(){
		if (currentState == ScreenState.OFF) {
			setEachItemInLayerTo (menu_layer1, true);
			currentState = ScreenState.MAINMENU;
		}else if (currentState == ScreenState.SAVEMENU) {
			setEachItemInLayerTo (save_layer, false);
			currentState = ScreenState.OFF;
		} else if (currentState == ScreenState.STATUSMENU) {
			setEachItemInLayerTo (status_layer, false);
			currentState = ScreenState.OFF;

		} else {
			//main menu
			setEachItemInLayerTo (menu_layer1, false);
			currentState = ScreenState.OFF;
		}
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
		} else if (FirstClick ()) {
			first_touch_detected = true;
			time_at_start_of_window = Time.time;
		} else if (SecondClick()){
			registerDoubleClick();
			first_touch_detected = false;
			time_at_start_of_window = Time.time;
		}
		

		
	}
}
