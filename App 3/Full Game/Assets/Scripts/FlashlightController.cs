using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private InputAction toggleFlashlightAction;
    
    private void Start()
    {
        flashlight.enabled = false;
        toggleFlashlightAction.Enable();
    }

    private void Update()
    {
        if (toggleFlashlightAction.WasPressedThisFrame())
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
