using UnityEngine;
using System.Collections.Generic;
public class ActionConfusion : ActionTypeDebuff, ActionInterface {
    public static readonly string NAME = "Confusion";
    public int Turns;

    public string GetName() {
        return NAME;
    }

    public ActionConfusion(int turns) {
        Turns = turns;
    }

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        if (defenseCharacter.ActionsFilters.ContainsKey(ActionFilterConfusion.NAME)) {
            defenseCharacter.ActionsFilters[ActionFilterConfusion.NAME].Cooldown += Turns;
        } else {
            defenseCharacter.ActionsFilters.Add(ActionFilterConfusion.NAME, new ActionFilterConfusion(Turns));
        }
    }
}