﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareDrawing : MonoBehaviour {


	public void setDisease(string dis){
		SimulationConfig sc = GameObject.Find ("GlobalGameManager").GetComponent<SimulationConfig> ();
		sc.next_drawing_disease = dis;
	}
	public void setIsLeftEye(bool is_left_eye){
		SimulationConfig sc = GameObject.Find ("GlobalGameManager").GetComponent<SimulationConfig> ();
		sc.next_drawing_is_left_eye = is_left_eye;
	}
	public void setMode(string mode){
		SimulationConfig sc = GameObject.Find ("GlobalGameManager").GetComponent<SimulationConfig> ();
		sc.mode = mode;

	}

	public void setFOVLevel(){
		SimulationConfig sc = GameObject.Find ("GlobalGameManager").GetComponent<SimulationConfig> ();
		Slider slider = GameObject.Find ("LevelSlider").GetComponent<Slider> ();
		sc.level = slider.value;
	}
}
