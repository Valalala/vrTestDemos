using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

	public Matrix4x4 originalProjection;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        //Matrix4x4 p = originalProjection;
		//Matrix4x4 p = GL.GetGPUProjectionMatrix(cam.projectionMatrix, false);
		Matrix4x4 p = cam.projectionMatrix;
        p.m01 += Mathf.Sin(Time.time * 1.2F) * .005f;
        p.m10 += Mathf.Sin(Time.time * 1.5F) * .005f;
        //cam.projectionMatrix = GL.GetGPUProjectionMatrix(p, false);
		cam.projectionMatrix = p;
	}
	
}
