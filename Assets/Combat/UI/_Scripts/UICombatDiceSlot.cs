using UnityEngine;
using UnityEngine.UI;

public class UICombatDiceSlot : MonoBehaviour {
    [HideInInspector] public string dieUUID;
    private Image DisplayImage;

    private void Start() {
        DisplayImage = gameObject.transform.Find("Die").GetComponent<Image>();
    }

    public void Set(Die die) {
        if (die == null) {
            return;
        }

        dieUUID = die.UUID;

        DisplayImage.enabled = true;
        DisplayImage.sprite = die.ImageValue;
    }

    public void Clear() {
        dieUUID = "";

        DisplayImage.sprite = null;
        DisplayImage.enabled = false;
    }
}
