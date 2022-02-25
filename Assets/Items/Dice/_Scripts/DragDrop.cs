using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    public bool OnTarget = false;
    private Canvas HUD;
    private CanvasGroup CanvasGroup;
    private RectTransform Moveable;
    private Transform OriginalSlot;

    private void Start() {
        HUD = GameObject.Find("Canvas").GetComponent<Canvas>();
        CanvasGroup = GetComponent<CanvasGroup>();
        Moveable = GetComponent<RectTransform>();
        OriginalSlot = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        CanvasGroup.alpha = .7f;
        CanvasGroup.blocksRaycasts = false;

        Moveable.SetParent(HUD.transform, true);
        Moveable.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        Moveable.anchoredPosition += eventData.delta / HUD.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;

        if (OnTarget) {
            OriginalSlot = Moveable.parent;
            OnTarget = false;
        } else {
            Moveable.SetParent(OriginalSlot, false);
            Moveable.anchoredPosition = Vector2.zero;
        }
    }
}
