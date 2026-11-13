using UnityEngine;

public class DayNightAudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light sun;
    [SerializeField] private Transform player;
    [SerializeField] private AudioSource daytimeAudio;
    [SerializeField] private AudioSource footstepsAudio;

    [Header("Daytime Ambience")]
    [SerializeField] private float daytimeVolume = 0.5f;
    [SerializeField] private float eveningStart = -0.15f;
    [SerializeField] private float nightStart = 0.05f;
    [SerializeField] private float fadeSpeed = 0.5f;

    [Header("Nighttime Footsteps")]
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float minInterval = 12f;
    [SerializeField] private float maxInterval = 30f;
    [SerializeField] private float minDistance = 6f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float footstepHeight = 1f;

    private float footstepTimer;

    private void Start()
    {
        footstepTimer = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        var sunHeight = sun.transform.forward.y;

        UpdateDaytimeAudio(sunHeight);
        UpdateNighttimeAudio(sunHeight);
    }

    private void UpdateDaytimeAudio(float sunHeight)
    {
        var nightAmount = Mathf.InverseLerp(
            eveningStart,
            nightStart,
            sunHeight
        );

        var targetVolume = daytimeVolume * (1f - Mathf.SmoothStep(0f, 1f, nightAmount));
        daytimeAudio.volume = Mathf.MoveTowards(
            daytimeAudio.volume,
            targetVolume,
            fadeSpeed * Time.deltaTime
        );
    }

    private void UpdateNighttimeAudio(float sunHeight)
    {
        if (sunHeight <= nightStart) return;

        footstepTimer -= Time.deltaTime;
        if (footstepTimer > 0f) return;

        PlayFootsteps();
        footstepTimer = Random.Range(minInterval, maxInterval);
    }

    private void PlayFootsteps()
    {
        if (footstepClips == null || footstepClips.Length == 0) return;

        var direction = Random.insideUnitCircle.normalized;
        var distance = Random.Range(minDistance, maxDistance);

        var position = player.position + new Vector3(
            direction.x * distance,
            -footstepHeight,
            direction.y * distance
        );

        footstepsAudio.transform.position = position;
        var clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepsAudio.PlayOneShot(clip);
    }
}
