using UnityEngine;
using UnityEngine.XR;

public class GestureDash : MonoBehaviour
{
    public XRNode handNode = XRNode.RightHand;
    public float gestureMultiplier = 2.5f;   // scales how strong a dash is
    public float minVelocityForDash = 1.2f;  // threshold for detecting a deliberate swing
    public float dashTime = 0.2f;            // how long dash lasts

    CharacterController controller;
    float dashRemaining = 0f;
    Vector3 dashDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
            {
                // forward component of velocity in headset-space:
                Vector3 forwardVel = transform.InverseTransformDirection(velocity);
                float forwardMag = forwardVel.z; // positive = forward swing

                if (forwardMag > minVelocityForDash && dashRemaining <= 0f)
                {
                    dashDirection = transform.forward; // dash relative to player facing
                    dashRemaining = dashTime;
                    Debug.Log($"Dash triggered: v={forwardMag}");
                }
            }
        }

        if (dashRemaining > 0f)
        {
            float t = Mathf.Min(Time.deltaTime, dashRemaining);
            controller.Move(dashDirection * gestureMultiplier * t);
            dashRemaining -= t;
        }
    }
}
