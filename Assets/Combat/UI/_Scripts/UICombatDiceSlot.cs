using UnityEngine;
using UnityEngine.UI;

public class UICombatDiceSlot : MonoBehaviour {
    public RectTransform DieTemplate;
    [HideInInspector] public string DieUUID;
    private Image DisplayImage;

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
