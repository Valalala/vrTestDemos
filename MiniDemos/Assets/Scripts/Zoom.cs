using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {
	public float offset;

	Camera cam;

	// Use this for initialization
	void Start () {
		cam  = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//cam.fieldOfView = 1 + Mathf.PingPong(45*Time.time+offset, 178);
		cam.fieldOfView = 88*(1 + Mathf.Sin(Time.time+offset));

	}
}
