using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum UIState {
    Selected,
    Deselected,
    None
}

public class UICombatController : MonoBehaviour {
    public Canvas HUD;
    public Text EnergyLevel;
    public Text VatInfo;
    public Transform DicePool;
    public Die SelectedDie;

    public CombatController _CombatController;
    public ZoneCombatController _ZonesController;
    public StateCombatController _StateController;
    private UIState _SelectedState = UIState.Deselected;

    public void Start() {
        _CombatController = GetComponent<CombatController>();
        _ZonesController = GetComponent<ZoneCombatController>();
        _StateController = GetComponent<StateCombatController>();

        GetEnergy();
        GetBankInfo();
    }

    public void ActivateTile(GameObject slotParent) {
        DiceZone tileZone = slotParent.GetComponentInParent<UICombatTile>().Zone;

        foreach (UICombatDiceSlot slot in slotParent.GetComponentsInChildren<UICombatDiceSlot>()) {
            _ZonesController.RemoveDie(slot.dieUUID);
            slot.Clear();
        }
    }

    public void GetEnergy() {
        EnergyLevel.text = _CombatController.Energy.ToString();

        if (_CombatController.Energy == 0) {
            EnergyLevel.color = Color.red;
        } else {
            EnergyLevel.color = Color.white;
        }
    }

    public void GetBankInfo() {
        string vatInfo = "";

        foreach (Die die in _CombatController.Bank) {
            foreach (int face in die.Faces) {
                vatInfo += face.ToString() + " ";
            }

            vatInfo += "\n";
        }

        VatInfo.text = vatInfo;
    }

    public void RollDice() {
        if (_CombatController.Energy > 0) {
            UICombatDiceSlot poolSlot = GetPoolDiceSlot("");

            if (poolSlot == null) {
                return;
            }
            Die rolledDie = _CombatController.GenerateDice();

            poolSlot.Set(rolledDie);
            _ZonesController.AddDie(DiceZone.Pool, rolledDie);
        }

        GetEnergy();

        _ZonesController.PrintDiceInZones();
    }

    public void MoveDice(UICombatDiceSlot slot) {
        switch (_SelectedState) {
            case UIState.Selected:
                SetSlot(slot);
                break;
            case UIState.Deselected:
                SelectDice(slot);
                break;
        }
    }

    public void EndTurn() {
        foreach (UICombatDiceSlot diceSlot in HUD.GetComponentsInChildren<UICombatDiceSlot>()) {
            diceSlot.Clear();
        }

        _StateController.IsPlayerTurnEnded = true;
    }

    private void SelectDice(UICombatDiceSlot diceSlot) {
        if (_SelectedState == UIState.Selected || diceSlot.dieUUID == "") {
            return;
        }

        SelectedDie = _ZonesController.GetDie(diceSlot.dieUUID);
        diceSlot.Highlight(new Color(0, .5f, 1));

        _SelectedState = UIState.Selected;
    }

    private void SetSlot(UICombatDiceSlot toSlot) {
        if (_SelectedState != UIState.Selected) {
            return;
        }

        DiceZone toZone = toSlot.GetComponentInParent<UICombatTile>().Zone;

        Die tempSlot = _ZonesController.GetDie(toSlot.dieUUID);

        UICombatDiceSlot fromSlot = GetAllDiceSlot(SelectedDie.UUID);

        if (tempSlot == null) {
            _ZonesController.MoveDie(toZone, SelectedDie.UUID);
        } else {
            _ZonesController.SwapDice(tempSlot.UUID, SelectedDie.UUID);
        }

        DiceZone fromZone = fromSlot.GetComponentInParent<UICombatTile>().Zone;
        fromSlot.Clear();

        toSlot.Set(SelectedDie);

        if (tempSlot != null) {
            fromSlot.Set(tempSlot);
        }

        SelectedDie = null;
        _SelectedState = UIState.Deselected;
    }

    private UICombatDiceSlot GetPoolDiceSlot(string uuid) {
        foreach (UICombatDiceSlot diceSlot in DicePool.GetComponentsInChildren<UICombatDiceSlot>()) {
            if (uuid == diceSlot.dieUUID) {
                return diceSlot;
            }
        }

        return null;
    }

    private UICombatDiceSlot GetAllDiceSlot(string uuid) {
        foreach (UICombatDiceSlot diceSlot in HUD.GetComponentsInChildren<UICombatDiceSlot>()) {
            if (uuid == diceSlot.dieUUID) {
                return diceSlot;
            }
        }
        return null;
    }
}
