using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Footstep Audio")]
    [SerializeField] private AudioClip[] grassFootsteps;
    [SerializeField] private AudioSource audioSource;

    [Header("Movement")]
    [SerializeField] private float stepDistance = 1.3f;
    [SerializeField] private float movementThreshold = 0.01f;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckDistance = 1.5f;

    private float distanceTravelled;
    private Vector3 previousPosition;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        previousPosition = transform.position;
    }

    private void Update()
    {
        var movement = transform.position - previousPosition;
        previousPosition = transform.position;

        movement.y = 0f;

        var distance = movement.magnitude;

        var grounded = Physics.Raycast(
            transform.position + Vector3.up * 0.2f,
            Vector3.down,
            groundCheckDistance,
            groundLayers
        );

        if (!grounded)
        {
            distanceTravelled = 0f;
            return;
        }

        if (distance < movementThreshold) return;

        distanceTravelled += distance;

        if (!(distanceTravelled >= stepDistance)) return;

        PlayFootstep();
        distanceTravelled = 0f;
    }

    private void PlayFootstep()
    {
        if (grassFootsteps == null || grassFootsteps.Length == 0) return;

        var index = Random.Range(0, grassFootsteps.Length);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(grassFootsteps[index]);
    }
}
