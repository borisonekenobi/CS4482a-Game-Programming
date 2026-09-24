using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class ProfileScrollViewSpawner : MonoBehaviour
    {
        public enum BarTextFormat
        {
            ValueOverMax,
            Percentage
        }

        public enum ListDirection
        {
            Vertical,
            Horizontal
        }

        [System.Serializable]
        public class ProgressData
        {
            public string progressName = "HP";
            public float minValue = 0f;
            public float maxValue = 100f;
            public float currentValue = 100f;
        }

        [System.Serializable]
        public class ProfileData
        {
            [Header("Profile")]
            public Sprite profileSprite;
            public string profileName;
            public string category;
            public string status = "Ready";

            [Header("Multiple Progress Bars")]
            public List<ProgressData> progressBars = new List<ProgressData>();

            [Header("Custom Spacing")]
            public float spacingBefore = 0f;
            public float spacingAfter = 10f;
        }

        [Header("Scroll View")]
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Transform contentParent;

        [Header("Panel Prefab")]
        [SerializeField] private ProfileUIPanel profilePanelPrefab;

        [Header("Listing Direction")]
        [SerializeField] private ListDirection listDirection = ListDirection.Vertical;

        [Header("General Panel Size")]
        [SerializeField] private bool useCustomPanelWidth = false;
        [SerializeField] private float panelWidth = 350f;

        [SerializeField] private bool useCustomPanelHeight = true;
        [SerializeField] private float panelHeight = 120f;

        [Header("Display")]
        [SerializeField] private BarTextFormat barTextFormat = BarTextFormat.ValueOverMax;

        [Header("Profiles")]
        [SerializeField] private List<ProfileData> profiles = new List<ProfileData>();

        [Header("Options")]
        [SerializeField] private bool rebuildOnStart = true;
        [SerializeField] private bool clearExistingPanels = true;
        [SerializeField] private bool autoSetupContentLayout = true;

        private readonly List<ProfileUIPanel> spawnedPanels = new List<ProfileUIPanel>();
    private float refreshTimer;

        private void Start()
        {
            if (rebuildOnStart)
                Rebuild();
        }
    private void Update()
    {
        refreshTimer += Time.deltaTime;

        if (refreshTimer >= 2f)
        {
            refreshTimer = 0f;
            Rebuild();
        }
    }

        public void SetProfiles(List<ProfileData> newProfiles)
        {
            profiles = newProfiles;
            Rebuild();
        }

        public void Rebuild()
        {
            if (contentParent == null && scrollRect != null)
                contentParent = scrollRect.content;

            if (contentParent == null)
            {
                Debug.LogWarning("Content Parent is missing.");
                return;
            }

            if (profilePanelPrefab == null)
            {
                Debug.LogWarning("Profile Panel Prefab is missing.");
                return;
            }

            if (autoSetupContentLayout)
                SetupContentLayout();

            if (clearExistingPanels)
                ClearPanels();

            for (int i = 0; i < profiles.Count; i++)
            {
                ProfileData data = profiles[i];

                if (data.spacingBefore > 0f)
                    CreateSpacer("Spacing Before " + data.profileName, data.spacingBefore);

                ProfileUIPanel panel = Instantiate(profilePanelPrefab, contentParent);
                panel.SetProfile(data, barTextFormat);

                ApplyGeneralPanelSize(panel.gameObject);

                spawnedPanels.Add(panel);

                if (data.spacingAfter > 0f)
                    CreateSpacer("Spacing After " + data.profileName, data.spacingAfter);
            }

            ForceRebuild();
        }

        public void UpdateProgressValue(int profileIndex, int progressIndex, float newValue)
        {
            if (!IsValidProgressIndex(profileIndex, progressIndex))
                return;

            profiles[profileIndex].progressBars[progressIndex].currentValue = newValue;

            if (profileIndex < spawnedPanels.Count && spawnedPanels[profileIndex] != null)
            {
                spawnedPanels[profileIndex].UpdateProgress(
                    progressIndex,
                    profiles[profileIndex].progressBars[progressIndex],
                    barTextFormat
                );
            }
        }

        public void UpdateProfileStatus(int profileIndex, string newStatus)
        {
            if (profileIndex < 0 || profileIndex >= profiles.Count)
            {
                Debug.LogWarning("Profile index is out of range.");
                return;
            }

            profiles[profileIndex].status = newStatus;

            if (profileIndex < spawnedPanels.Count && spawnedPanels[profileIndex] != null)
            {
                spawnedPanels[profileIndex].SetStatus(newStatus);
            }
        }

        public void UpdateProfileData(int profileIndex, ProfileData newData)
        {
            if (profileIndex < 0 || profileIndex >= profiles.Count)
            {
                Debug.LogWarning("Profile index is out of range.");
                return;
            }

            profiles[profileIndex] = newData;

            if (profileIndex < spawnedPanels.Count && spawnedPanels[profileIndex] != null)
            {
                spawnedPanels[profileIndex].SetProfile(newData, barTextFormat);
                ApplyGeneralPanelSize(spawnedPanels[profileIndex].gameObject);
            }

            ForceRebuild();
        }

        public void SetListDirection(ListDirection newDirection)
        {
            listDirection = newDirection;

            SetupContentLayout();
            Rebuild();
        }

        public void SetGeneralPanelSize(float newWidth, float newHeight)
        {
            panelWidth = newWidth;
            panelHeight = newHeight;

            foreach (ProfileUIPanel panel in spawnedPanels)
            {
                if (panel != null)
                    ApplyGeneralPanelSize(panel.gameObject);
            }

            ForceRebuild();
        }

        private void ApplyGeneralPanelSize(GameObject panelObject)
        {
            LayoutElement layoutElement = panelObject.GetComponent<LayoutElement>();

            if (layoutElement == null)
                layoutElement = panelObject.AddComponent<LayoutElement>();

            if (useCustomPanelWidth)
            {
                layoutElement.minWidth = panelWidth;
                layoutElement.preferredWidth = panelWidth;
            }

            if (useCustomPanelHeight)
            {
                layoutElement.minHeight = panelHeight;
                layoutElement.preferredHeight = panelHeight;
            }

            layoutElement.flexibleWidth = 0f;
            layoutElement.flexibleHeight = 0f;
        }

        private void CreateSpacer(string spacerName, float size)
        {
            GameObject spacer = new GameObject(spacerName, typeof(RectTransform));
            spacer.transform.SetParent(contentParent, false);

            LayoutElement layoutElement = spacer.AddComponent<LayoutElement>();

            if (listDirection == ListDirection.Vertical)
            {
                layoutElement.minHeight = size;
                layoutElement.preferredHeight = size;
                layoutElement.flexibleHeight = 0f;
            }
            else
            {
                layoutElement.minWidth = size;
                layoutElement.preferredWidth = size;
                layoutElement.flexibleWidth = 0f;
            }
        }

        private void ClearPanels()
        {
            spawnedPanels.Clear();

            for (int i = contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(contentParent.GetChild(i).gameObject);
            }
        }

        private void SetupContentLayout()
        {
            VerticalLayoutGroup verticalLayout = contentParent.GetComponent<VerticalLayoutGroup>();
            HorizontalLayoutGroup horizontalLayout = contentParent.GetComponent<HorizontalLayoutGroup>();

            if (listDirection == ListDirection.Vertical)
            {
                if (horizontalLayout != null)
                    horizontalLayout.enabled = false;

                if (verticalLayout == null)
                    verticalLayout = contentParent.gameObject.AddComponent<VerticalLayoutGroup>();

                verticalLayout.enabled = true;
                verticalLayout.spacing = 0f;
                verticalLayout.childControlWidth = true;
                verticalLayout.childControlHeight = true;
                verticalLayout.childForceExpandWidth = true;
                verticalLayout.childForceExpandHeight = false;

                if (scrollRect != null)
                {
                    scrollRect.vertical = true;
                    scrollRect.horizontal = false;
                }
            }
            else
            {
                if (verticalLayout != null)
                    verticalLayout.enabled = false;

                if (horizontalLayout == null)
                    horizontalLayout = contentParent.gameObject.AddComponent<HorizontalLayoutGroup>();

                horizontalLayout.enabled = true;
                horizontalLayout.spacing = 0f;
                horizontalLayout.childControlWidth = true;
                horizontalLayout.childControlHeight = true;
                horizontalLayout.childForceExpandWidth = false;
                horizontalLayout.childForceExpandHeight = true;

                if (scrollRect != null)
                {
                    scrollRect.vertical = false;
                    scrollRect.horizontal = true;
                }
            }

            ContentSizeFitter fitter = contentParent.GetComponent<ContentSizeFitter>();

            if (fitter == null)
                fitter = contentParent.gameObject.AddComponent<ContentSizeFitter>();

            if (listDirection == ListDirection.Vertical)
            {
                fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            else
            {
                fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            }
        }

        private bool IsValidProgressIndex(int profileIndex, int progressIndex)
        {
            if (profileIndex < 0 || profileIndex >= profiles.Count)
            {
                Debug.LogWarning("Profile index is out of range.");
                return false;
            }

            if (progressIndex < 0 || progressIndex >= profiles[profileIndex].progressBars.Count)
            {
                Debug.LogWarning("Progress index is out of range.");
                return false;
            }

            return true;
        }

        private void ForceRebuild()
        {
            RectTransform contentRect = contentParent as RectTransform;

            if (contentRect != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        }
    }
}
