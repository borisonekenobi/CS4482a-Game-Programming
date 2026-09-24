using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
    public class ProfileProgressBarUI : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text progressNameText;
        [SerializeField] private TMP_Text progressValueText;

        [Header("Progress Fill")]
        [SerializeField] private RectTransform fillRect;
        [SerializeField] private Image fillImage;

        [Header("Progress Color Settings")]
        [Range(0f, 1f)]
        [SerializeField] private float highPercentThreshold = 0.7f;

        [Range(0f, 1f)]
        [SerializeField] private float averagePercentThreshold = 0.3f;

        [SerializeField] private Color highProgressColor = Color.green;
        [SerializeField] private Color averageProgressColor = Color.yellow;
        [SerializeField] private Color belowProgressColor = Color.red;

        private void Awake()
        {
            if (fillImage == null && fillRect != null)
                fillImage = fillRect.GetComponent<Image>();
        }

        public void SetProgress(
            ProfileScrollViewSpawner.ProgressData data,
            ProfileScrollViewSpawner.BarTextFormat textFormat
        )
        {
            if (progressNameText != null)
                progressNameText.text = data.progressName;

            float normalizedValue = GetNormalizedValue(
                data.currentValue,
                data.minValue,
                data.maxValue
            );

            if (fillRect != null)
            {
                Vector3 scale = fillRect.localScale;
                scale.x = normalizedValue;
                fillRect.localScale = scale;
            }

            if (fillImage != null)
                fillImage.color = GetProgressColor(normalizedValue);

            if (progressValueText != null)
            {
                switch (textFormat)
                {
                    case ProfileScrollViewSpawner.BarTextFormat.Percentage:
                        progressValueText.text = Mathf.RoundToInt(normalizedValue * 100f) + "%";
                        break;

                    case ProfileScrollViewSpawner.BarTextFormat.ValueOverMax:
                    default:
                        progressValueText.text =
                            Mathf.RoundToInt(data.currentValue) + "/" +
                            Mathf.RoundToInt(data.maxValue);
                        break;
                }
            }
        }

        private float GetNormalizedValue(float current, float min, float max)
        {
            if (max <= min)
                return 0f;

            return Mathf.Clamp01((current - min) / (max - min));
        }

        private Color GetProgressColor(float normalizedValue)
        {
            if (normalizedValue >= highPercentThreshold)
                return highProgressColor;

            if (normalizedValue >= averagePercentThreshold)
                return averageProgressColor;

            return belowProgressColor;
        }
    }
}
