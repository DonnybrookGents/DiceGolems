using UnityEngine;
using System.Collections.Generic;
public class ActionConfusion : ActionTypeDebuff, ActionInterface {
    public static readonly string NAME = "Confusion";
    public int Turns;

    public ActionConfusion(int turns) {
        Turns = turns;
    }

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        Debug.Log("Executing Confusion");

        if (defenseCharacter.ActionsFilters.ContainsKey(ActionFilterConfusion.NAME)) {
            defenseCharacter.ActionsFilters[ActionFilterConfusion.NAME].Cooldown += Turns;
            Debug.Log("Adding to Confusion: " + defenseCharacter.ActionsFilters[ActionFilterConfusion.NAME].Cooldown);
        } else {
            defenseCharacter.ActionsFilters.Add(ActionFilterConfusion.NAME, new ActionFilterConfusion(Turns));
            Debug.Log("Adding new Confusion: " + defenseCharacter.ActionsFilters[ActionFilterConfusion.NAME].Cooldown);
        }
    }
}