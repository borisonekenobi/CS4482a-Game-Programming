using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class UIDragDropPanel : MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IInitializePotentialDragHandler
    {
        [Header("Drag Settings")]
        [SerializeField] private bool canDrag = true;
        [SerializeField] private bool bringToFrontOnDrag = true;
        [SerializeField] private bool keepInsideParent = true;

        [Header("Optional Drag Handle")]
        [Tooltip("Leave empty to drag from the whole panel. Assign a header/title RectTransform to drag only from that area.")]
        [SerializeField] private RectTransform dragHandle;

        [Header("Layout Fix")]
        [Tooltip("Enable this if the panel is inside a Vertical/Horizontal/Grid Layout Group. Layout Groups can fight dragging and cause flicker.")]
        [SerializeField] private bool ignoreLayout = true;

        private RectTransform panelRect;
        private RectTransform parentRect;
        private LayoutElement layoutElement;

        private Vector2 pointerStartLocalPosition;
        private Vector2 panelStartAnchoredPosition;
        private bool isDragging;

        private readonly Vector3[] panelCorners = new Vector3[4];
        private readonly Vector3[] parentCorners = new Vector3[4];

        private void Awake()
        {
            panelRect = GetComponent<RectTransform>();
            parentRect = panelRect.parent as RectTransform;

            if (dragHandle == null)
                dragHandle = panelRect;

            if (ignoreLayout)
            {
                layoutElement = GetComponent<LayoutElement>();
                if (layoutElement == null)
                    layoutElement = gameObject.AddComponent<LayoutElement>();

                layoutElement.ignoreLayout = true;
            }
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            // Removes small drag delay. This helps the panel feel stable and responsive.
            eventData.useDragThreshold = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!canDrag) return;

            if (bringToFrontOnDrag)
                panelRect.SetAsLastSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!canDrag || panelRect == null || parentRect == null) return;
            if (!IsPointerOnDragHandle(eventData)) return;

            isDragging = true;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out pointerStartLocalPosition
            );

            panelStartAnchoredPosition = panelRect.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging || panelRect == null || parentRect == null) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 pointerCurrentLocalPosition
            );

            Vector2 pointerDelta = pointerCurrentLocalPosition - pointerStartLocalPosition;
            panelRect.anchoredPosition = panelStartAnchoredPosition + pointerDelta;

            if (keepInsideParent)
                KeepPanelInsideParent();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }

        private bool IsPointerOnDragHandle(PointerEventData eventData)
        {
            if (dragHandle == null) return true;

            return RectTransformUtility.RectangleContainsScreenPoint(
                dragHandle,
                eventData.position,
                eventData.pressEventCamera
            );
        }

        private void KeepPanelInsideParent()
        {
            panelRect.GetWorldCorners(panelCorners);
            parentRect.GetWorldCorners(parentCorners);

            float panelMinX = float.PositiveInfinity;
            float panelMaxX = float.NegativeInfinity;
            float panelMinY = float.PositiveInfinity;
            float panelMaxY = float.NegativeInfinity;

            float parentMinX = float.PositiveInfinity;
            float parentMaxX = float.NegativeInfinity;
            float parentMinY = float.PositiveInfinity;
            float parentMaxY = float.NegativeInfinity;

            for (int i = 0; i < 4; i++)
            {
                Vector3 localPanelCorner = parentRect.InverseTransformPoint(panelCorners[i]);
                panelMinX = Mathf.Min(panelMinX, localPanelCorner.x);
                panelMaxX = Mathf.Max(panelMaxX, localPanelCorner.x);
                panelMinY = Mathf.Min(panelMinY, localPanelCorner.y);
                panelMaxY = Mathf.Max(panelMaxY, localPanelCorner.y);

                Vector3 localParentCorner = parentRect.InverseTransformPoint(parentCorners[i]);
                parentMinX = Mathf.Min(parentMinX, localParentCorner.x);
                parentMaxX = Mathf.Max(parentMaxX, localParentCorner.x);
                parentMinY = Mathf.Min(parentMinY, localParentCorner.y);
                parentMaxY = Mathf.Max(parentMaxY, localParentCorner.y);
            }

            Vector2 correction = Vector2.zero;

            if (panelMinX < parentMinX)
                correction.x += parentMinX - panelMinX;
            else if (panelMaxX > parentMaxX)
                correction.x += parentMaxX - panelMaxX;

            if (panelMinY < parentMinY)
                correction.y += parentMinY - panelMinY;
            else if (panelMaxY > parentMaxY)
                correction.y += parentMaxY - panelMaxY;

            panelRect.anchoredPosition += correction;
        }

        public void SetCanDrag(bool value)
        {
            canDrag = value;
        }
    }
}
