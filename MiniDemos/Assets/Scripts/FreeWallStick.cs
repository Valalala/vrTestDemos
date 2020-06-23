using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeWallStick : MonoBehaviour {

	AudioSource wind;

	public float speed = 40;

	void Start(){

		wind = GetComponent<AudioSource>();

	}
	
	void Update(){

		wind.volume = GetComponent<Rigidbody>().velocity.magnitude * GetComponent<Rigidbody>().velocity.magnitude / 2000;

		//transform.forward = GetComponent<Rigidbody>().velocity;
		//transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity, Vector3.up );
		float step = speed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards( transform.rotation, Quaternion.LookRotation(GetComponent<Rigidbody>().velocity, Vector3.up), step);
		//transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation(GetComponent<Rigidbody>().velocity, Vector3.up), step);

	}

	//void OnCollisionEnter(){
		
		//GetComponent<Rigidbody>().velocity = Vector3.zero;
		
	//}


	
}
