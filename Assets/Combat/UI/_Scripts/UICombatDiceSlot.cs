using UnityEngine;
using UnityEngine.UI;

public class UICombatDiceSlot : MonoBehaviour {
    [HideInInspector] public string dieUUID;
    [HideInInspector] public Text DisplayText;

    private void Start() {
        DisplayText = GetComponentInChildren<Text>();
    }

    public void Set(Die die) {
        if (die == null) {
            return;
        }

        dieUUID = die.UUID;
        DisplayText.text = die.Value.ToString();
    }

    public void Clear() {
        dieUUID = "";
        DisplayText.text = "";
        Highlight(Color.white);
    }

    public void Highlight(Color color) {
        GetComponent<Image>().color = color;
    }
}
