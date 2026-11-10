using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Light flashlight;
    [SerializeField] private InputAction toggleFlashlightAction;

    [Header("Sounds")]
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;
    [SerializeField] private AudioSource audioSource;
    
    private void Start()
    {
        flashlight.enabled = false;
        toggleFlashlightAction.Enable();
    }

    private void Update()
    {
        if (!toggleFlashlightAction.WasPressedThisFrame()) return;

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flashlight.enabled ? onSound : offSound);
    }
}
