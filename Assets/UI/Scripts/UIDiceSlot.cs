using UnityEngine;
using UnityEngine.UI;

public class UIDiceSlot : MonoBehaviour {
    [HideInInspector] public string UUID;
    [HideInInspector] public Text DisplayText;

    private void Start() {
        DisplayText = GetComponentInChildren<Text>();
    }

    public void Add(Die die) {
        if (die == null) {
            return;
        }

        UUID = die.UUID;
        DisplayText.text = die.Value.ToString();
    }

    public void Clear() {
        UUID = "";
        DisplayText.text = "";
        Highlight(Color.white);
    }

    public void Highlight(Color color) {
        GetComponent<Image>().color = color;
    }
}
