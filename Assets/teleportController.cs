using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class teleportController : MonoBehaviour
{
    public InputActionProperty teleportActivationAction;
    public XRRayInteractor teleportInteractor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        teleportInteractor.gameObject.SetActive(false);
        teleportActivationAction.action.Enable();
        teleportActivationAction.action.performed += ActionPerformed;
        teleportActivationAction.action.canceled += ActionCanceled;
    }

    private void ActionPerformed(InputAction.CallbackContext obj)
    {
        teleportInteractor.gameObject.SetActive(true);
    }

    private void ActionCanceled(InputAction.CallbackContext obj)
    {
        StartCoroutine(JumpOneFrame());
    }

    System.Collections.IEnumerator JumpOneFrame()
    {
        yield return null;
        teleportInteractor.gameObject.SetActive(false);
    }
}
