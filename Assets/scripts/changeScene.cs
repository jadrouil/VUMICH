using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class changeScene : MonoBehaviour {


	public void ChangeToScene (string target_scene) {
		SceneManager.LoadScene (target_scene);
	}

}
