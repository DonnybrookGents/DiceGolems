using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum UIState {
    Selected,
    Deselected,
    None
}

public class CombatUIController : MonoBehaviour {
    public CombatController Combat;
    public Text EnergyLevel;
    public Text VatInfo;
    public Transform DicePool;
    public Die SelectedDie;

    private UIState _State = UIState.Deselected;

    private Dictionary<DiceZone, Dictionary<string, Die>> ZoneMap;

    public void Start() {
        ZoneMap = new Dictionary<DiceZone, Dictionary<string, Die>>{
            {DiceZone.Pool, Combat.Pool},
            {DiceZone.AttackTile, Combat.AttackTile},
            {DiceZone.DefenseTile, Combat.DefenseTile}
        };
        GetEnergy();
        GetBankInfo();
    }

    public void GetEnergy() {
        EnergyLevel.text = Combat.Energy.ToString();

        if (Combat.Energy == 0) {
            EnergyLevel.color = Color.red;
        } else {
            EnergyLevel.color = Color.white;
        }
    }

    public void GetBankInfo() {
        string vatInfo = "";

        foreach (Die die in Combat.Bank) {
            foreach (int face in die.Faces) {
                vatInfo += face.ToString() + " ";
            }

            vatInfo += "\n";
        }

        VatInfo.text = vatInfo;
    }

    public void RollDice() {
        if (Combat.Energy > 0) {
            UIDiceSlot poolSlot = GetPoolDiceSlot("");

            if (poolSlot == null) {
                return;
            }

            poolSlot.Set(Combat.GenerateDice());
        }

        GetEnergy();
    }

    public void MoveDice(UIDiceSlot slot) {
        switch (_State) {
            case UIState.Selected:
                SetSlot(slot);
                break;
            case UIState.Deselected:
                SelectDice(slot);
                break;
        }
    }

    private void SelectDice(UIDiceSlot diceSlot) {
        if (_State == UIState.Selected || diceSlot.UUID == "") {
            return;
        }
        /*

        SelectedDie = Combat.Hand[diceSlot.UUID];
        if(SelectedDie == null){
            SelectedDie = Combat.AttackTile[diceSlot.UUID];
        }
        
        */
        SelectedDie = Combat.Table[diceSlot.UUID];
        diceSlot.Highlight(new Color(0, .5f, 1));

        _State = UIState.Selected;
    }

    private void SetSlot(UIDiceSlot toSlot) {
        if (_State != UIState.Selected) {
            return;
        }

        Die tempSlot = (toSlot.UUID != "") ? Combat.Table[toSlot.UUID] : null;

        UIDiceSlot fromSlot = GetAllDiceSlot(SelectedDie.UUID);
        ZoneMap[fromSlot.Zone].Remove(SelectedDie.UUID);
        fromSlot.Clear();

        toSlot.Set(SelectedDie);
        ZoneMap[toSlot.Zone].Add(SelectedDie.UUID, SelectedDie);

        if (tempSlot != null) {
            fromSlot.Set(tempSlot);

            if (!ZoneMap[fromSlot.Zone].ContainsKey(fromSlot.UUID)) {
                ZoneMap[fromSlot.Zone].Add(fromSlot.UUID, Combat.Table[fromSlot.UUID]);
            }
        }

        SelectedDie = null;
        _State = UIState.Deselected;
    }

    private UIDiceSlot GetPoolDiceSlot(string uuid) {
        foreach (UIDiceSlot diceSlot in DicePool.GetComponentsInChildren<UIDiceSlot>()) {
            if (uuid == diceSlot.UUID) {
                return diceSlot;
            }
        }

        return null;
    }

    private UIDiceSlot GetAllDiceSlot(string uuid) {
        foreach (UIDiceSlot diceSlot in GetComponentsInChildren<UIDiceSlot>()) {
            if (uuid == diceSlot.UUID) {
                return diceSlot;
            }
        }

        return null;
    }
}
