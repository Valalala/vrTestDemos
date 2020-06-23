using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingBasic : MonoBehaviour {
private Rigidbody body;
    private SteamVR_TrackedController controller;

    [SerializeField]
    private float glideMultipler = 2f;

    [SerializeField]
    private float maxVelocity = 1f;

    private void OnEnable()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        body = transform.GetComponentInParent<Rigidbody>(); ;
    }

    private void FixedUpdate()
    {
        var thrust = controller.controllerState.rAxis1.x;
		
		var flap = controller.controllerState.ulButtonPressed;
		
				
		//print(controller.controllerState.ulButtonPressed);

		if(flap == 4){
			
			body.AddForce(new Vector3(0,20,0));
			
		}


        if (thrust == 1)
        {
            var forceVector =  glideMultipler * Vector3.Project( -body.velocity, transform.up);
            
			
			body.AddForce(forceVector);

            //SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)(200f * forceVector.magnitude));
        }
    }
	
}
