using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public RectTransform Tooltip;
    private bool _IsHovering = false;

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("test");

        _IsHovering = true;
        StartCoroutine(ShowTooltip(1));
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.gameObject.SetActive(false);
        _IsHovering = false;
    }

    private IEnumerator ShowTooltip(float seconds) {
        yield return new WaitForSeconds(seconds);

        if (_IsHovering) {
            Tooltip.gameObject.SetActive(true);
        }
    }
}
