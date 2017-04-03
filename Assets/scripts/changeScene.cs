using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class changeScene : MonoBehaviour {


	public void ChangeToScene (string target_scene) {
		if (target_scene == "MainScreen") {
			GameObject ggm = GameObject.Find ("GlobalGameManager");
			ggm.GetComponent<DrawingCollector> ().Reset ();

			DestroyObject (ggm);
		}
		SceneManager.LoadScene (target_scene);
	}


	public void Collect(string d_name){
		DrawingCollector dc = GameObject.Find ("GlobalGameManager").GetComponent<DrawingCollector>();
		DrawingPreserver d = GameObject.Find (d_name).GetComponent<DrawingPreserver> ();
		dc.add_drawing (d);

	}

}
