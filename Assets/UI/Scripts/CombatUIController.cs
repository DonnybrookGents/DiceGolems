using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour {
    public CombatController Combat;
    public Text EnergyLevel;
    public Text VatInfo;
    public Transform DicePool;

    private List<Text> Dice = new List<Text>();

    public void Start() {
        GetEnergy();
        GetVatDice();
        GetDicePlaceholders();
    }

    public void GetEnergy() {
        EnergyLevel.text = Combat.Energy.ToString();

        if (Combat.Energy == 0) {
            EnergyLevel.color = Color.red;
        } else {
            EnergyLevel.color = Color.white;
        }
    }

    public void GetVatDice() {
        string vatInfo = "";

        foreach (Die die in Combat.Vat) {
            foreach (int face in die.Faces) {
                vatInfo += face.ToString() + " ";
            }

            vatInfo += "\n";
        }

        VatInfo.text = vatInfo;
    }

    public void RollDice() {
        if (Combat.Energy > 0 && Combat.Pool.Count < Dice.Count) {
            Combat.GenerateDice();

            int index = Combat.Pool.Count - 1;
            Dice[index].text = Combat.Pool[index].Value.ToString();
        }

        GetEnergy();
    }

    private void GetDicePlaceholders() {
        foreach (Transform transform in DicePool) {
            Dice.Add(transform.Find("Value").GetComponent<Text>());
        }
    }
}
