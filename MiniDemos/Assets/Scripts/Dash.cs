using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Dash : MonoBehaviour {
    
    private Rigidbody body;

    [SerializeField]
    private float multipler = 500f;
    Vector3 pastPoint;
    int count = 0;

    private void OnEnable()
    {
        body = transform.GetComponentInParent<Transform>().GetComponentInParent<Rigidbody>();
        pastPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if ( .2f < Vector3.Distance(transform.localPosition, pastPoint)) {
            //body.AddForce(Vector3.Normalize(transform.localPosition - pastPoint)*multipler);//*(Vector3.Distance(InputTracking.GetLocalPosition(VRNode.Head),transform.localPosition)/pullDistance));
            //body.transform.position = body.transform.position + Vector3.Normalize(transform.localPosition - pastPoint);//*multipler;
            body.velocity += Vector3.Normalize(transform.localPosition - pastPoint)*multipler;
        }
        count++;
        if (count > 10) {
            pastPoint = transform.localPosition;
            count = 0;
        }
        
    }
    
	
}
