using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.005f;

    private void FixedUpdate()
    {
        transform.Rotate(rotationSpeed, 0, 0);
    }
}
