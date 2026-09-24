using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Project.UI
{
    public class ProfileUIPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Panel Background")]
        [SerializeField] private Image panelBackground;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color hoverColor = new Color(0.8f, 0.9f, 1f, 1f);

        [Header("Profile UI")]
        [SerializeField] private Image profileImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text categoryText;
        [SerializeField] private TMP_Text statusText;

        [Header("Existing Progress Bars In This Panel")]
        [SerializeField] private List<ProfileProgressBarUI> progressBars = new List<ProfileProgressBarUI>();

        [Header("Options")]
        [SerializeField] private bool hideUnusedProgressBars = true;

        private void Awake()
        {
            if (panelBackground == null)
                panelBackground = GetComponent<Image>();

            SetPanelColor(normalColor);
        }

        public void SetProfile(
            ProfileScrollViewSpawner.ProfileData data,
            ProfileScrollViewSpawner.BarTextFormat textFormat
        )
        {
            if (profileImage != null)
                profileImage.sprite = data.profileSprite;

            if (nameText != null)
                nameText.text = data.profileName;

            if (categoryText != null)
                categoryText.text = data.category;

            SetStatus(data.status);

            SetProgressBars(data.progressBars, textFormat);
        }

        public void SetStatus(string status)
        {
            if (statusText != null)
                statusText.text = status;
        }

        public void UpdateProgress(
            int progressIndex,
            ProfileScrollViewSpawner.ProgressData progressData,
            ProfileScrollViewSpawner.BarTextFormat textFormat
        )
        {
            if (progressIndex < 0 || progressIndex >= progressBars.Count)
            {
                Debug.LogWarning("Progress bar index is out of range.");
                return;
            }

            if (progressBars[progressIndex] == null)
                return;

            progressBars[progressIndex].gameObject.SetActive(true);
            progressBars[progressIndex].SetProgress(progressData, textFormat);
        }

        private void SetProgressBars(
            List<ProfileScrollViewSpawner.ProgressData> progressDataList,
            ProfileScrollViewSpawner.BarTextFormat textFormat
        )
        {
            if (progressDataList == null)
                return;

            int usableCount = Mathf.Min(progressDataList.Count, progressBars.Count);

            for (int i = 0; i < usableCount; i++)
            {
                if (progressBars[i] == null)
                    continue;

                progressBars[i].gameObject.SetActive(true);
                progressBars[i].SetProgress(progressDataList[i], textFormat);
            }

            if (progressDataList.Count > progressBars.Count)
            {
                Debug.LogWarning(
                    "Profile has more progress data than existing progress bars in the panel. Extra progress values will not be shown."
                );
            }

            if (hideUnusedProgressBars)
            {
                for (int i = usableCount; i < progressBars.Count; i++)
                {
                    if (progressBars[i] != null)
                        progressBars[i].gameObject.SetActive(false);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetPanelColor(hoverColor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetPanelColor(normalColor);
        }

        private void SetPanelColor(Color color)
        {
            if (panelBackground != null)
                panelBackground.color = color;
        }
    }
}
