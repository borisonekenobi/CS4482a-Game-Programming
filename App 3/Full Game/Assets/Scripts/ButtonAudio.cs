using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] buttonSounds;

    public void PlayRandomButtonSound()
    {
        var audioSource = GetComponent<AudioSource>();
        var randomIndex = Random.Range(0, buttonSounds.Length);
        audioSource.PlayOneShot(buttonSounds[randomIndex]);
    }
}
