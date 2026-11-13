using UnityEngine;

public class StreamAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] streamClips;

    private void Start()
    {
        PlayRandomStream();
    }

    private void Update()
    {
        if (!audioSource.isPlaying) PlayRandomStream();
    }

    private void PlayRandomStream()
    {
        var index = Random.Range(0, streamClips.Length);
        audioSource.clip = streamClips[index];
        audioSource.Play();
    }
}
