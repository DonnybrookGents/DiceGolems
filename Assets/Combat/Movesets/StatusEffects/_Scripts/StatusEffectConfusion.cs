using System.Collections.Generic;
using UnityEngine;

public class StatusEffectConfusion : StatusEffect {
    public static readonly string NAME = "Confusion";
    public StatusEffectConfusion(List<CombatState> executionStates, List<CombatState> countdownStates, int count)
        : base(executionStates, countdownStates, count, 0, 100) { }

    public override int Execute(int damage, CombatState combatState) {

        Debug.Log("Current Combat State: " + combatState);

        if (!ExecutionStates.Contains(combatState)) {
            Debug.Log("Confusion not triggered");
            return damage;
        }
        Debug.Log("Rolling for confusion...");
        if (Random.Range(0, 2) == 0) {
            Debug.Log("Double!");
            return damage * 2;
        }
        Debug.Log("Zero!");
        return 0;
    }
    public override void Execute(Character character, CombatState combatState) {
        return;
    }

    public override string GetName() {
        return NAME;
    }


}
