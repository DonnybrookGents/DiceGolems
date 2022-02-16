using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ActionLightAttack : ActionTypeAttack, ActionInterface {
    public static string Name = "Light Attack";
    public System.Tuple<int, int> DamageRange = System.Tuple.Create(4, 6);

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        defenseCharacter.TakeDamage(base.ApplyDamage(defenseCharacter, attackCharacter, Damage(), combatState));
    }

    private int Damage() {
        return base.Damage(DamageRange.Item1, DamageRange.Item2);
    }

}