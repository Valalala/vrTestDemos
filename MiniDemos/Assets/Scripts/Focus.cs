using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Focus : MonoBehaviour {

	public PostProcessingProfile startGaze;
	
	DepthOfFieldModel.Settings gaze;
	Camera cam;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		gaze = startGaze.depthOfField.settings;
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Physics.SphereCast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), .2f, out hit)) {
			gaze.focusDistance = hit.distance;
		} else {
			gaze.focusDistance = 0;
		}
		
		startGaze.depthOfField.settings = gaze;
	}
}
