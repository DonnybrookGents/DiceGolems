using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICombatDiceSlot : MonoBehaviour, IDropHandler {
    public RectTransform DieTemplate;
    [HideInInspector] public string DieUUID;
    private Image DisplayImage;

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        RectTransform die = eventData.pointerDrag.GetComponent<RectTransform>();
        eventData.pointerDrag.GetComponent<DragDrop>().OnTarget = true;

        if (gameObject.transform.Find("Die")) {
            eventData.pointerDrag.GetComponent<DragDrop>().OnTarget = false;

            return;
        }

        die.SetParent(transform, false);
        die.localPosition = Vector2.zero;
    }

    public void Set(Die die) {
        if (die == null) {
            return;
        }

        DieUUID = die.UUID;

        RectTransform newDie = Instantiate<RectTransform>(DieTemplate);
        newDie.SetParent(transform, false);
        newDie.GetComponent<Image>().sprite = die.ImageValue;
        newDie.name = DieTemplate.name;
    }

    public void Clear() {
        DieUUID = "";
        Transform die = gameObject.transform.Find("Die");
        if (die) {
            Destroy(die.gameObject);
        }
    }
}
