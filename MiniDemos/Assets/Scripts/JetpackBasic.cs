// Source: https://unity3d.college/2017/07/11/vr-movement-ironman-jetpack-flying-with-steamvr/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackBasic : MonoBehaviour
{
    private Rigidbody body;
    private SteamVR_TrackedController controller;

    [SerializeField]
    private float thrustMultipler = 14f;

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

        if (thrust > 0.1)
        {
            var forceVector = transform.forward * thrust * thrustMultipler;
            body.AddForce(forceVector);

            //SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)(200f * thrust));
        }
    }
}