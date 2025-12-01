using UnityEngine;
using Oculus.Haptics;   // Required for Meta Haptics (.haptic files)

public class PushHaptics : MonoBehaviour
{
    [Header("Haptics")]
    public HapticClip pushHapticClip;   // Drag your .haptic file
    [Range(0f, 1f)]
    public float hapticIntensity = 1f;

    private HapticClipPlayer _clipPlayer;

    [Header("Bridge")]
    public Transform bridge;        // The bridge to move
    public Vector3 bridgeMoveAmount = new Vector3(0, 0, 5f); // How far to move in Z
    private bool bridgeMoved = false;

    [Header("Button Detection")]
    public string buttonTag = "Button"; // Tag your button object

    void OnCollisionEnter(Collision collision)
    {
        // 1️⃣ Play haptics when player touches box
        if (collision.gameObject.CompareTag("PlayerHand"))
        {
            PlayHaptics();
        }

        // 2️⃣ Move bridge if box touches button
        if (!bridgeMoved && collision.gameObject.CompareTag(buttonTag))
        {
            MoveBridge();
        }
    }

    void PlayHaptics()
    {
        if (pushHapticClip == null) return;

        if (_clipPlayer != null)
        {
            _clipPlayer.Dispose();
            _clipPlayer = null;
        }

        _clipPlayer = new HapticClipPlayer(pushHapticClip);
        _clipPlayer.amplitude = Mathf.Clamp01(hapticIntensity);
        _clipPlayer.Play(Controller.Both);
    }

    void MoveBridge()
    {
        if (bridge == null) return;

        bridge.position += bridgeMoveAmount;
        bridgeMoved = true;
    }

    private void OnDestroy()
    {
        if (_clipPlayer != null)
        {
            _clipPlayer.Dispose();
            _clipPlayer = null;
        }
    }
}