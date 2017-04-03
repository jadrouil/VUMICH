using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimLoader : MonoBehaviour {
	public loadScreen screen;


	public int currentSaveSelection;
	public void setSelection(int val){
		currentSaveSelection = val;
	}

	public void deleteSelection(){
		screen.delete (currentSaveSelection);
		currentSaveSelection = 0;
	}
	void Start(){
		currentSaveSelection = 0;
	
	}
}
