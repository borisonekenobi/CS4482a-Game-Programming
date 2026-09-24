using System.Collections;
using UnityEngine;

namespace Project.UI
{
    public class UIPanelToggleAutoHide : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject panel;

        [Header("Layering")]
        [SerializeField] private bool bringToFrontOnShow = true;

        [Header("Auto Hide")]
        [SerializeField] private bool autoHide = true;
        [SerializeField] private float autoHideSeconds = 3f;

        private Coroutine autoHideCoroutine;

        private void Reset()
        {
            // Optional helper: assign this GameObject if the script is placed directly on the panel.
            panel = gameObject;
        }

        public void TogglePanel()
        {
            if (panel == null) return;

            if (panel.activeSelf)
                HidePanel();
            else
                ShowPanel();
        }

        public void ShowPanel()
        {
            if (panel == null) return;

            panel.SetActive(true);

            // Makes the latest shown panel appear on top of other UI elements
            // that share the same parent in the Canvas hierarchy.
            if (bringToFrontOnShow)
                panel.transform.SetAsLastSibling();

            if (autoHide)
            {
                StopAutoHideTimer();
                autoHideCoroutine = StartCoroutine(AutoHideTimer());
            }
        }

        public void HidePanel()
        {
            if (panel == null) return;

            StopAutoHideTimer();
            panel.SetActive(false);
        }

        private IEnumerator AutoHideTimer()
        {
            yield return new WaitForSeconds(autoHideSeconds);
            HidePanel();
        }

        private void StopAutoHideTimer()
        {
            if (autoHideCoroutine != null)
            {
                StopCoroutine(autoHideCoroutine);
                autoHideCoroutine = null;
            }
        }
    }
}
