using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour {

	AudioSource wind;

	void Start(){

		wind = GetComponent<AudioSource>();

	}
	
	void Update(){

		wind.volume = GetComponent<Rigidbody>().velocity.magnitude * GetComponent<Rigidbody>().velocity.magnitude / 2000;

	}

	void OnCollisionEnter(){
		
		//GetComponent<Rigidbody>().velocity = Vector3.zero;
		
	}


	
}
