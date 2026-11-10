using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Image image;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] buttonSounds;
    [SerializeField] private AudioSource audioSource;

    public void Start()
    {
        image.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
    }

    public void PlayRandomButtonSound()
    {
        var randomIndex = Random.Range(0, buttonSounds.Length);
        audioSource.PlayOneShot(buttonSounds[randomIndex]);
    }
}
