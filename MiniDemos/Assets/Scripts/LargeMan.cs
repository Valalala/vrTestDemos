using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Large : MonoBehaviour {

    public Transform planet;

	Valve.VR.HmdQuad_t playSize;
	bool check = false;
    Vector3 startCorner;
    float playX, playZ;


	// Use this for initialization
	void Start () {
		//StartCoroutine("playSizeFind");
	}
	
	// Update is called once per frame
	void Update () {
		// if (playX != 0 && playZ != 0){
		//     transform.rotation = new Quaternion(InputTracking.GetLocalPosition(XRNode.Head).z/playZ,0,InputTracking.GetLocalPosition(XRNode.Head).x/playX,0);
        // }

        transform.up = InputTracking.GetLocalPosition(XRNode.Head) - planet.position;


        print(playX);
        print(playZ);

	}

	IEnumerator playSizeFind(){
        
        playSize = new Valve.VR.HmdQuad_t();
        
        while(!check) { 
            yield return new WaitForSeconds(1); 
            check = Valve.VR.OpenVR.Chaperone.GetPlayAreaRect(ref playSize);
            //print("running?");
        }
        
        startCorner = new Vector3(playSize.vCorners1.v0, 0 ,playSize.vCorners1.v2);
        playX = playSize.vCorners0.v0 - playSize.vCorners1.v0;
        playZ = -playSize.vCorners0.v2 + playSize.vCorners2.v2;
	}

}
