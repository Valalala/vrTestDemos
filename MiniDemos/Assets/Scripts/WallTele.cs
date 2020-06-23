using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTele : MonoBehaviour {

	private Rigidbody body;
    private SteamVR_TrackedController controller;

    [SerializeField]
    private float thrustMultipler = 14f;

    [SerializeField]
    private float maxVelocity = 1f;
    
    RaycastHit teleRay;
    bool teleCheck = false;

    private void OnEnable()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        body = transform.GetComponentInParent<Rigidbody>(); ;
    }

    private void FixedUpdate()
    {
        var thrust = controller.controllerState.rAxis1.x;

        if (thrust == 1)
        {
            if(teleCheck == false) {
                Teleport();
                //Physics.Raycast(transform.position, transform.forward, out web, 200);
                teleCheck = true;
            }
        }
        else {
            teleCheck = false;
        }
    }
    
    void Teleport(){
        
        if(Physics.Raycast(transform.position ,transform.forward, out teleRay)) {
            
            body.transform.position = teleRay.point;
            //body.transform.rotation = Quaternion.FromToRotation(Vector3.up, teleRay.normal);
            body.transform.up = teleRay.normal;
            
            //set start color
            SteamVR_Fade.Start(Color.clear, 0f);
            //set and start fade to
            SteamVR_Fade.Start(Color.black, 1f);
            //set and start fade to
            SteamVR_Fade.Start(Color.clear, 1f);
            
        }
        
    }
	
}
