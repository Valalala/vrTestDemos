using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Throw : MonoBehaviour {

	public GameObject ball;
	
	private Rigidbody body;
    private SteamVR_TrackedController controller;

    private void OnEnable()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        body = transform.GetComponentInParent<Rigidbody>();
		controller.TriggerUnclicked += throwBall; 
    }
	
	 private void OnDisable()
    {
		controller.TriggerUnclicked -= throwBall; 
    }

    private void FixedUpdate()
    {
        var thrust = controller.controllerState.rAxis1.x;
		
		if (thrust==1) { 
			ball.transform.position = controller.transform.position;
		}
		
		// if(controller.OnTriggerUnclicked) ;
		
	}
	
	
	
	void throwBall(object sender, ClickedEventArgs e){
		
		ball.GetComponent<Rigidbody>().velocity = controller.transform.forward *100;
		
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
