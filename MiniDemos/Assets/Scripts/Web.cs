using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Web : MonoBehaviour {
    
    public GameObject webPoint;
    
    private Rigidbody body;
    private SteamVR_TrackedController controller;

    [SerializeField]
    private float jumpMultipler = 2000f;
    
    public float pullStrength = 50;

    [SerializeField]
    private float maxVelocity = 20f;
    
    RaycastHit web, feet;
    ulong jumping;
    GameObject swingPoint;
    bool stickCheck = false;
    Vector3 pullPoint;
    float pullDistance;

    LineRenderer wstring;

    void Start(){
        wstring = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        body = transform.GetComponentInParent<Rigidbody>(); ;
    }

    private void FixedUpdate()
    {
        var thrust = controller.controllerState.rAxis1.x;
		
		jumping = controller.controllerState.ulButtonPressed;
		


		//print(controller.controllerState.ulButtonPressed);

		if(jumping == 4 ){ //&& body.velocity == Vector3.zero
			if(Physics.Raycast(transform.position ,Vector3.down, out feet, 2f) && body.velocity.x < maxVelocity && body.velocity.z <maxVelocity) body.AddForce(transform.forward * jumpMultipler);
		}


        if (thrust == 1)
        {

            if(stickCheck == false) {
                SlingWeb();
                //Physics.Raycast(transform.position, transform.forward, out web, 200);
                stickCheck = true;
            }
            else if( !web.Equals(null)){
                //body.velocity += Vector3.Project( -body.velocity, transform.position - web.point);
                if(!swingPoint.Equals(null)) if(swingPoint.GetComponent<SpringJoint>().maxDistance > Vector3.Distance(transform.position, web.point)) 
                    swingPoint.GetComponent<SpringJoint>().maxDistance = Vector3.Distance(transform.position, web.point);
                    
                // if(pullDistance > Vector3.Distance(InputTracking.GetLocalPosition(VRNode.Head),transform.localPosition)+.01f)
                //     body.AddForce(Vector3.Normalize(-transform.position + web.point)*pullStrength*(1 - Vector3.Distance(InputTracking.GetLocalPosition(VRNode.Head),transform.localPosition)/pullDistance));
                // //swingPoint.GetComponent<SpringJoint>().maxDistance += 20f;
                // //swingPoint.GetComponent<SpringJoint>().maxDistance *= .9f;
                // pullDistance = Vector3.Distance(InputTracking.GetLocalPosition(VRNode.Head),transform.localPosition);
                
                if(pullDistance < Vector3.Distance(pullPoint,transform.localPosition)-.01f)
                    body.AddForce(Vector3.Normalize(-transform.position + web.point)*pullStrength);//*(Vector3.Distance(InputTracking.GetLocalPosition(VRNode.Head),transform.localPosition)/pullDistance));
                
                pullDistance = Vector3.Distance(pullPoint,transform.localPosition);
                
                wstring.enabled = true;
                wstring.SetPosition(0, transform.position);
                wstring.SetPosition(1, web.point);

            }
            //SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)(200f * forceVector.magnitude));
        }
        else {
            stickCheck = false;
            Destroy(swingPoint);
            wstring.enabled = false;
        }
        
    }
    
    void SlingWeb(){
        
        if(Physics.Raycast(transform.position ,transform.forward, out web)) {
            
            swingPoint = Instantiate( webPoint, web.point, new Quaternion(0,0,0,0));
            swingPoint.GetComponent<SpringJoint>().connectedBody = body;
            swingPoint.GetComponent<SpringJoint>().maxDistance = Vector3.Distance(transform.position, web.point);
            //swingPoint.GetComponent<SpringJoint>().connectedAnchor = Vector3.zero;
        
            //pullDistance = Vector3.Distance(InputTracking.GetLocalPosition(VRNode.Head),transform.localPosition);
            pullPoint = transform.localPosition;
            pullDistance = 0;
            //pullDistance = Vector3.Distance(swingPoint.transform.position,transform.localPosition);
        }
        
    }
    
	
}
