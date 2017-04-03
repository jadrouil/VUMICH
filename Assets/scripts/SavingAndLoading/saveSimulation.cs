using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class saveSimulation : MonoBehaviour {
	public string saveFileName;
	List<DrawingPreserver> leftDrawings;
	List<DrawingPreserver> rightDrawings;

	public void setSaveFileName(string newname){
		saveFileName = newname;
	}

	public void save(string drawingObjectName){
		GameObject simMan = GameObject.Find (drawingObjectName);
		if (simMan) {
			SetUpSimulation sim = simMan.GetComponent<SetUpSimulation> ();
			leftDrawings = sim.leftDrawings;
			rightDrawings = sim.rightDrawings;
		} else {
			print ("could not find " + drawingObjectName);
		}
	}

	public void commenceSave(){
		CompressedSimulationPreserver comp = new CompressedSimulationPreserver (leftDrawings, rightDrawings);
		BinaryFormatter bf = new BinaryFormatter ();
		string filelocation = Application.persistentDataPath + "/" + saveFileName + ".vum";
		FileStream file = File.Create (filelocation);
		bf.Serialize (file, comp);
		file.Close ();
		print (filelocation);
	}


}
