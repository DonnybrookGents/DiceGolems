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
    public ZoneController Zones;
    public StateController State;
    public Text EnergyLevel;
    public Text VatInfo;
    public Transform DicePool;
    public Die SelectedDie;

    private UIState _State = UIState.Deselected;

    public void Start() {
        GetEnergy();
        GetBankInfo();
    }

    public void ActivateTile(GameObject slotParent) {
        DiceZone tileZone = slotParent.GetComponentInParent<UITile>().Zone;

        foreach (UIDiceSlot slot in slotParent.GetComponentsInChildren<UIDiceSlot>()) {
            Zones.RemoveDie(slot.dieUUID);
            slot.Clear();
        }
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
            Die rolledDie = Combat.GenerateDice();

            poolSlot.Set(rolledDie);
            Zones.AddDie(DiceZone.Pool, rolledDie);
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

    public void EndTurn() {
        foreach (UIDiceSlot diceSlot in GetComponentsInChildren<UIDiceSlot>()) {
            diceSlot.Clear();
        }

        Zones.Clear();
        State.IsPlayerTurnEnded = true;
    }

    private void SelectDice(UIDiceSlot diceSlot) {
        if (_State == UIState.Selected || diceSlot.dieUUID == "") {
            return;
        }

        SelectedDie = Zones.GetDie(diceSlot.dieUUID);
        diceSlot.Highlight(new Color(0, .5f, 1));

        _State = UIState.Selected;
    }

    private void SetSlot(UIDiceSlot toSlot) {
        if (_State != UIState.Selected) {
            return;
        }

        DiceZone toZone = toSlot.GetComponentInParent<UITile>().Zone;

        Die tempSlot = Zones.GetDie(toSlot.dieUUID);

        UIDiceSlot fromSlot = GetAllDiceSlot(SelectedDie.UUID);

        if (tempSlot == null) {
            Zones.MoveDie(toZone, SelectedDie.UUID);
        } else {
            Zones.SwapDice(tempSlot.UUID, SelectedDie.UUID);
        }

        DiceZone fromZone = fromSlot.GetComponentInParent<UITile>().Zone;
        fromSlot.Clear();

        toSlot.Set(SelectedDie);

        if (tempSlot != null) {
            fromSlot.Set(tempSlot);
        }

        SelectedDie = null;
        _State = UIState.Deselected;
    }

    private UIDiceSlot GetPoolDiceSlot(string uuid) {
        foreach (UIDiceSlot diceSlot in DicePool.GetComponentsInChildren<UIDiceSlot>()) {
            if (uuid == diceSlot.dieUUID) {
                return diceSlot;
            }
        }

        return null;
    }

    private UIDiceSlot GetAllDiceSlot(string uuid) {
        foreach (UIDiceSlot diceSlot in GetComponentsInChildren<UIDiceSlot>()) {
            if (uuid == diceSlot.dieUUID) {
                return diceSlot;
            }
        }
        return null;
    }
}
