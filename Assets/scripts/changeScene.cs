using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class changeScene : MonoBehaviour {

	public string scene_after_disease_clinical = "DegreeSelection";
	public string scene_after_disease_athome = "SelectEye";

	public void ChangeToScene (string target_scene) {
		if (target_scene == "MainScreen") {
			GameObject ggm = GameObject.Find ("GlobalGameManager");
			if (ggm) {
				ggm.GetComponent<DrawingCollector> ().Reset ();
				DestroyObject (ggm);
			}
		}
		SceneManager.LoadScene (target_scene);
	}

	public void ChangeFromDiseaseTo(){
		SimulationConfig sc = GameObject.Find ("GlobalGameManager").GetComponent<SimulationConfig> ();
		if (sc.mode == "Clinical")
			ChangeToScene (scene_after_disease_clinical);
		else
			ChangeToScene (scene_after_disease_athome);
	
	}

	public void Collect(string d_name){
		DrawingCollector dc = GameObject.Find ("GlobalGameManager").GetComponent<DrawingCollector>();
		DrawingPreserver d = GameObject.Find (d_name).GetComponent<DrawingPreserver> ();
		dc.add_drawing (d);

	}

}
