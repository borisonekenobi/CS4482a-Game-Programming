using UnityEngine;

public class Ruby : MonoBehaviour, ICollectible
{
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    public string Name => "Ruby";
}
