using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class saveSimulation : MonoBehaviour {
	public string saveFileName;
	List<DrawingPreserver> leftDrawings;
	List<DrawingPreserver> rightDrawings;
	public string status_msg;
	public static string saveFolder = "/Saves";


	public void setSaveFileName(string newname){
		saveFileName = newname;
	}

	public void save(string drawingObjectName){
		status_msg = "An unexpected error occurred";
		GameObject simMan = GameObject.Find (drawingObjectName);
		if (simMan) {
			SetUpSimulation sim = simMan.GetComponent<SetUpSimulation> ();
			leftDrawings = sim.leftDrawings;
			rightDrawings = sim.rightDrawings;
		} else {
			print ("could not find " + drawingObjectName);
		}
	}

	bool errors(string filelocation){
		if (saveFileName.Length == 0) {
			status_msg = "Invalid save name";
			return true;

		}
		if (File.Exists (filelocation)) {
			status_msg = "Save name is already in use";
			return true;
		}
		return false;
	}


	public void commenceSave(){
		string path = Application.persistentDataPath + saveFolder;
		string filelocation = path +"/" +  saveFileName + ".vum";
		if (!Directory.Exists (path)) {
			Directory.CreateDirectory (path);
		}

		if (errors (filelocation))
			return;

		CompressedSimulationPreserver comp = new CompressedSimulationPreserver (leftDrawings, rightDrawings);
		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create (filelocation);
		bf.Serialize (file, comp);
		file.Close ();
		status_msg = "Simulation successfully saved";

	}


}
