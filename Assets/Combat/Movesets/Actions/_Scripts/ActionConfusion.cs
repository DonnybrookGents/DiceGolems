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

        if (defenseCharacter.StatusEffects.ContainsKey(StatusEffectConfusion.NAME)) {

            defenseCharacter.StatusEffects[StatusEffectConfusion.NAME].Count += Turns;
            Debug.Log("Adding to Confusion: " + defenseCharacter.StatusEffects[StatusEffectConfusion.NAME].Count);
        } else {
            defenseCharacter.StatusEffects.Add(StatusEffectConfusion.NAME,
                new StatusEffectConfusion(new List<CombatState>() { CombatState.PlayerMidTurn }, new List<CombatState>() { CombatState.PlayerPostTurn }, Turns)
            );
            Debug.Log("Adding new Confusion: " + defenseCharacter.StatusEffects[StatusEffectConfusion.NAME].Count);
        }
    }

    // private int Debuff() {
    //     return base.Damage(DamageRange.Item1, DamageRange.Item2);
    // }

}