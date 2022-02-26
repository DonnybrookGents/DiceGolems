using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackOverride : ActionOverride {

    public ActionAttackOverride() {

    }
    public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, ActionContainer action) {
        ActionAttackContainer attack = (ActionAttackContainer)action;
        //generate damage
        int damage = Random.Range(attack.InclusiveMinDamage, attack.ExclusiveMaxDamage);
        //loop through and apply all attacker filters


        damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, offensiveCharacter.ActionFilters, FilterType.AttackActor);
        damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, defensiveCharacter.ActionFilters, FilterType.AttackRecipient);

        //Execute the action

        defensiveCharacter.TakeDamage(damage);

    }
}
