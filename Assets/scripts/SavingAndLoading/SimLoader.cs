using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class SimLoader : MonoBehaviour {
	public loadScreen screen;
	CompressedSimulationPreserver sim;
	public GameObject linePrefab;


	public int currentSaveSelection;
	public void setSelection(int val){
		currentSaveSelection = val;
	}

	public void deleteSelection(){
		screen.delete (currentSaveSelection);
		currentSaveSelection = 0;
	}
	public void loadSave(){
		string saved_sim_name = screen.full_paths [currentSaveSelection];
		if (File.Exists (saved_sim_name)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(saved_sim_name, FileMode.Open);
			sim = (CompressedSimulationPreserver)bf.Deserialize(file);
			file.Close();
			print ("Successfully loaded save");

			DrawingCollector dc = GameObject.Find ("GlobalGameManager").GetComponent<DrawingCollector> ();
			foreach (DrawingPreserver dp in reconstruct(sim.left)) {

				dc.add_left (dp);
			}
			foreach (DrawingPreserver dp in reconstruct(sim.right)) {
				dc.add_right (dp);
			}
			SceneManager.LoadScene ("Simulation");
			dc.loaded = true;
		}

	}

	void Start(){
		currentSaveSelection = 0;
	
	}
	List<DrawingPreserver> reconstruct(List<CompressedDrawing> csp){
		List<DrawingPreserver> drawings = new List<DrawingPreserver> ();
		drawings.AddRange (reconstructDrawings (csp));
		print (drawings.Count);
		return drawings;
	}

	List<DrawingPreserver> reconstructDrawings(List<CompressedDrawing> cd){
		List<DrawingPreserver> ds = new List<DrawingPreserver> ();
		foreach (CompressedDrawing decompress in cd) {
			ds.Add (makeDrawingPreserver (decompress.lines));
		}
		return ds;

	}
	DrawingPreserver makeDrawingPreserver(List<CompressedLine> lines){
		LineRenderer[] ls = new LineRenderer[lines.Count];
		for (int i = 0; i < lines.Count; ++i) {
			GameObject thisTrail = (GameObject)Instantiate (linePrefab,
				this.transform.position,
				Quaternion.identity);
			LineRenderer lr = thisTrail.GetComponent<LineRenderer> ();

			lr.numPositions = lines [i].pts.Count;

			lr.SetPositions (lines [i].v3 ());
			lr.startWidth = lines [i].width;
			lr.endWidth = lines [i].width;

			lr.material.SetColor ("_Color", new Color(0,0,0, lines[i].alpha));
			lr.material.SetColor ("_EmissionColor", new Color(0,0,0, lines[i].alpha));
		
			ls [i] = lr;
			DontDestroyOnLoad (lr.gameObject);
		}
		DrawingPreserver dp = new DrawingPreserver ();
		dp.lines = ls;

		return dp;
	}
}
