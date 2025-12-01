using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    public BridgeMover bridge;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PushBox"))
        {
            bridge.ActivateBridge();
        }
    }
}
