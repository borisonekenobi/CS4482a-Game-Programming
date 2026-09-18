using UnityEngine;

public class CharacterData : ScriptableObject
{
    public GameObject prefab;
    public float maxHealth;
    public float maxEnergy;
    public float critChance;
    public float power;
    public new string name;
    public ColliderType collider;
    public bool rigidbody;
}

public enum ColliderType
{
    None,
    Box,
    Capsule,
    Mesh,
    Sphere,
    Terrain,
    Wheel
}
