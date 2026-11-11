using UnityEngine;

public class Ruby : Collectible
{
    [SerializeField] private float rotationSpeed = 100f;

    protected override Item Item => Item.Ruby;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
