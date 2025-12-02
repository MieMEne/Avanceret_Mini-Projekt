using UnityEngine;
using UnityEngine.XR;
using Oculus.Haptics;   // Required for Meta Haptics (.haptic files)

//using Meta.Haptics;

public class GestureDash : MonoBehaviour
{
    public XRNode handNode = XRNode.RightHand;
    public float dashDistance = 3f;
    public float dashDuration = 0.15f;
    public float minSpeedToDash = 0.3f;

    private CharacterController controller;
    private float dashTimer = 0f;
    private Vector3 dashVelocity;

    public HapticClip dashHapticClip;
    private HapticClipPlayer _clipPlayer;
    public float hapticIntensity = 1f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(handNode);

        if (device.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
        {
            // Debug to verify Quest is sending velocity
            Debug.Log("Velocity: " + velocity);

            if (velocity.magnitude > minSpeedToDash && dashTimer <= 0f)
            {
                Vector3 forward = Camera.main.transform.forward;
                forward.y = 0;
                forward.Normalize();

                dashVelocity = forward * (dashDistance / dashDuration);
                dashTimer = dashDuration;
                PlayDashHaptics();

                Debug.Log("Dash triggered!");
            }
        }

        if (dashTimer > 0)
        {
            controller.Move(dashVelocity * Time.deltaTime);
            dashTimer -= Time.deltaTime;

            // When the dash ends
            if (dashTimer <= 0f)
            {
                StopDashHaptics();
            }
        }
    }
    void PlayDashHaptics()
    {
         if (dashHapticClip == null) return;

        if (_clipPlayer != null)
        {
            _clipPlayer.Dispose();
            _clipPlayer = null;
        }

        _clipPlayer = new HapticClipPlayer(dashHapticClip);
        _clipPlayer.amplitude = Mathf.Clamp01(hapticIntensity);
        _clipPlayer.Play(Controller.Both);
    }
    void StopDashHaptics()
    {
        if (_clipPlayer != null)
        {
            _clipPlayer.Stop();
            _clipPlayer.Dispose();
            _clipPlayer = null;
        }
    }

    
}


