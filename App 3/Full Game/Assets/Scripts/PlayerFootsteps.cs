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

    private float _distanceTravelled;
    private Vector3 _previousPosition;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        _previousPosition = transform.position;
    }

    private void Update()
    {
        var movement = transform.position - _previousPosition;
        _previousPosition = transform.position;

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
            _distanceTravelled = 0f;
            return;
        }

        if (distance < movementThreshold) return;

        _distanceTravelled += distance;

        if (!(_distanceTravelled >= stepDistance)) return;

        PlayFootstep();
        _distanceTravelled = 0f;
    }

    private void PlayFootstep()
    {
        if (grassFootsteps == null || grassFootsteps.Length == 0) return;

        var index = Random.Range(0, grassFootsteps.Length);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(grassFootsteps[index]);
    }
}
