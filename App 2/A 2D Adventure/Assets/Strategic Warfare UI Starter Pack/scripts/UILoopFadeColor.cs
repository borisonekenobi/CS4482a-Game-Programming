using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class UILoopFadeColor_NoTransparent : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Graphic targetGraphic;

        [Header("Colors")]
        [SerializeField] private Color fromColor = Color.white;
        [SerializeField] private Color toColor = Color.yellow;

        [Header("Timing")]
        [SerializeField] private float seconds = 1f;
        [SerializeField] private bool playOnEnable = true;
        [SerializeField] private bool useUnscaledTime = false;

        [Header("Alpha")]
        [SerializeField] private bool keepOriginalAlpha = true;

        private float timer;
        private bool isPlaying;
        private float originalAlpha = 1f;

        private void Reset()
        {
            targetGraphic = GetComponent<Graphic>();
        }

        private void Awake()
        {
            if (targetGraphic != null)
                originalAlpha = targetGraphic.color.a;
        }

        private void OnEnable()
        {
            if (targetGraphic != null)
                originalAlpha = targetGraphic.color.a;

            if (playOnEnable)
                Play();
        }

        private void OnDisable()
        {
            StopFade();
        }

        private void Update()
        {
            if (!isPlaying || targetGraphic == null) return;

            float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            timer += deltaTime / Mathf.Max(0.01f, seconds);

            float t = Mathf.PingPong(timer, 1f);
            Color newColor = Color.Lerp(fromColor, toColor, t);

            if (keepOriginalAlpha)
                newColor.a = originalAlpha;

            targetGraphic.color = newColor;
        }

        public void Play()
        {
            timer = 0f;
            isPlaying = true;

            if (targetGraphic != null)
            {
                originalAlpha = targetGraphic.color.a;

                Color startColor = fromColor;
                if (keepOriginalAlpha)
                    startColor.a = originalAlpha;

                targetGraphic.color = startColor;
            }
        }

        public void StopFade()
        {
            isPlaying = false;
        }
    }
}
