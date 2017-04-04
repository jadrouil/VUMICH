using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class loadScreen : MonoBehaviour {

	string saves_path;
	string[] saves;
	public string[] full_paths;
	int SavesAvailable;
	int NoSavesAvailable;

	void noSaves(){
		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			GameObject c = gameObject.transform.GetChild (i).gameObject;
			if (c.layer == SavesAvailable) {
				c.SetActive (false);
			} else if (c.layer == NoSavesAvailable) {
				c.SetActive (true);
			}
		}
	}
	public void delete(int itemToDelete){
		if (itemToDelete < saves.Length) {
			File.Delete (full_paths [itemToDelete]);
			setUpLoadScreen ();
		}

	}



	void displaySaves(string[] fullnames){
		for (int i = 0; i < fullnames.Length; ++i) {
			saves [i] = Path.GetFileNameWithoutExtension (fullnames [i]);
		}



		Dropdown saves_drop = GameObject.Find ("Dropdown-Saves").GetComponent<Dropdown>();
		saves_drop.ClearOptions ();


		saves_drop.AddOptions (new List<string>(saves));
		saves_drop.value = 0;
	}
	void setUpLoadScreen(){
		if (!Directory.Exists (saves_path)) {
			noSaves ();
			return;
		}

		full_paths = Directory.GetFiles (saves_path);

		if (full_paths.Length > 0) {
			saves = new string[full_paths.Length];
			displaySaves (full_paths);
		} else {
			noSaves ();
		}
	
	}


	void Start () {
		SavesAvailable = LayerMask.NameToLayer ("SavesAvailable");
		NoSavesAvailable = LayerMask.NameToLayer("NoSavesAvailable");

		saves_path = Application.persistentDataPath +  saveSimulation.saveFolder;
		setUpLoadScreen ();

	}

}
