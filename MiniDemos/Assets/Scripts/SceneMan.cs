#region fileheader
/*H**********************************************************************
* FILENAME :	SceneMan.cs
*
* DESCRIPTION :
*		Scene manager template, for chossing where to save data and select scene. Essentialy an options menu.
*
* DEPENDANCIES: 
*		User input regarding public variables.
*
* USE:
*		Add to empty gameobject.
*
* NOTES :
*		You are free to change this assuming you are not working in the "Project Template" project.
* 
* AUTHOR :	Walter Rasmussen		START DATE :	6/10/2018
*
*H*/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour {

	// Ensures this script persists during scene change and is not duplicated when the Intro scene is revisited.
	private static SceneMan singleton;

	[Tooltip("Will this project record data?(Leave unchecked, incomplete feature.)")]
	public bool recordsData;
	//[Tooltip("If left unchecked, scenes are selected using their names in editor.(If you named your scenes appropriately leave this unchecked.)")]
	[Tooltip("If left unchecked, scenes' names are shown. (If you named your scenes appropriately leave this unchecked.)")]
	public bool useCustomSceneNames;
	public string[] sceneNames;

	//public GameObject test;

	//////////////UNFINISHED////////////////////
	string customPath = "";
	string filename = "";
	int sceneNum = 0;
	int participant = 0;
	int run = 0;
	// Is the program currently recording data?
	bool recordingData;
	//////////////////////////////////


	Vector2 scrollPosition = Vector2.zero;

	// Ensures this script persists during scene change and is not duplicated when the Intro scene is revisited. 
	void Awake(){
		DontDestroyOnLoad (this);
			
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy(this.gameObject);
		}
 	}

	void Start () {

		if (SceneManager.GetActiveScene().buildIndex != 0) {
			Debug.LogError("Please make sure the scene this script is in has a build index of 0.");
			Debug.Break();
		}
		
		//Default save path
		//customPath = Application.dataPath + "/RecordedData/";
		filename = PlayerPrefs.GetString("filename", "data"); // if no name, uses data

		// If your scenes are very large your demo might be slow starting up. Using custom scene names can help with this. 
		if (!useCustomSceneNames) {
			sceneNames = new string[SceneManager.sceneCountInBuildSettings-1];
			for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
				// The only way to get the names of scenes are to load them first. This can be slow, but only needs to happen once. 
				SceneManager.LoadScene(i);
				sceneNames[i-1] = SceneManager.GetSceneByBuildIndex(i).name;
				//SceneManager.UnloadSceneAsync(i);
			}
			SceneManager.LoadScene(0);
			//SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
		}
	}

	// Handles all GUI needed by the sceen editor. This also serves as an example of code based GUI. 
	void OnGUI(){

		// Everything is relative to screen height and width, ergo shorter names:
		int h = Screen.height;
		int w = Screen.width;

		if (recordsData) {


			//Allows you to edit save path.
			//GUI.Label( new Rect(Screen.width/2-300, Screen.height/2-220, 600, 40), "Change path and filename(the last term) as needed.(Do not include .txt, must be added later) The default path is convenient because the file shows up in the Unity Editor.");
			//customPath = GUI.TextField( new Rect(Screen.width/2-300, Screen.height/2-180, 600, 20), customPath);

			filename = GUI.TextField( new Rect(w*2/20, 0, w*2/20, h/20), filename);

			bool newParticipant = false;
			bool newRun = false;
			foreach (string file in System.IO.Directory.GetFiles(Application.dataPath + "/RecordedData/")){ 
				newParticipant = newParticipant || file.Contains("participant" + participant);
				newRun = newRun || (file.Contains(filename) && file.Contains(participant + "run" + run));
			}

			customPath = Application.dataPath + "/RecordedData/" + filename + "participant" + participant + "run" + run;

			//waiting for button
			PlayerPrefs.SetString("filename", filename);

		} 

		if(SceneManager.sceneCountInBuildSettings>1){
			scrollPosition = GUI.BeginScrollView(new Rect(0, h/20, w*2/20, h*3/20), scrollPosition, new Rect(0, 0, w*2/20-16, h*(SceneManager.sceneCountInBuildSettings-1)/20), false, true);

			for (int i = 0; i < SceneManager.sceneCountInBuildSettings-1; i++) 
				if (GUI.Button(new Rect(0, h*i/20, w*2/20-16, h/20), sceneNames[i])) sceneNum = i;

			// End the scroll view that we began above.
			GUI.EndScrollView();

			//Starts the scene that was selected.
			//Use PlayerPrefs to save options for use in later scenes.
			if (GUI.Button(new Rect(0, 0, w*2/20, h/20), new GUIContent("Open: " + sceneNames[sceneNum], "Click this to open the scene named inside this button. Click on the buttons in the scroll view to change the selected scene. "))){
				PlayerPrefs.SetString("customPath", customPath);
				SceneManager.LoadScene(sceneNum+1);
			}
	 	}

		GUI.Label(new Rect(Input.mousePosition.x+15, h-Input.mousePosition.y+15, 300, 80), GUI.tooltip);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
