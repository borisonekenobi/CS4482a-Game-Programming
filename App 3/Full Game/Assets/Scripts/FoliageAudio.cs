using UnityEngine;

public class FoliageAudio : MonoBehaviour
{
    [Header("Foliage Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] foliageClips;

    [Header("Timing")]
    [SerializeField] private float minInterval = 45f;
    [SerializeField] private float maxInterval = 100f;

    private void Start()
    {
        ScheduleNextSound();
    }

    private void ScheduleNextSound()
    {
        Invoke(nameof(PlayRandomSound), Random.Range(minInterval, maxInterval));
    }

    private void PlayRandomSound()
    {
        var index = Random.Range(0, foliageClips.Length);
        audioSource.PlayOneShot(foliageClips[index]);
        ScheduleNextSound();
    }
}
