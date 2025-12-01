using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class LoudnessJump : MonoBehaviour
{
    public float jumpThreshold = 0.25f;
    public float jumpSpeed = 4f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private XROrigin xrOrigin;
    private float verticalVelocity;

    void Start()
    {
        xrOrigin = GetComponent<XROrigin>();
        controller = GetComponent<CharacterController>();

        if (controller == null)
            Debug.LogError("CharacterController is missing on XR Origin");
    }

    void Update()
    {
        UpdateControllerHeight();
        ApplyGravity();

        // Jump if loudness exceeds threshold and player is grounded
        if (MicLoudness.loudness > jumpThreshold && controller.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }

        // Apply vertical movement
        Vector3 move = new Vector3(0, verticalVelocity, 0);
        controller.Move(move * Time.deltaTime);
    }

    void UpdateControllerHeight()
    {
        // Match collider height to camera height
        float headHeight = Mathf.Clamp(xrOrigin.CameraInOriginSpaceHeight, 1f, 2f);
        controller.height = headHeight;
        controller.center = new Vector3(0, controller.height / 2f, 0);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -0.1f; // keep grounded
        else
            verticalVelocity += gravity * Time.deltaTime;
    }
}
