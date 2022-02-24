using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackOverride : ActionOverride {

    public ActionAttackOverride() {

    }
    public void Execute(Character defensiveCharacter, Character offensiveCharacter, ActionContainer action) {
        ActionAttackContainer attack = (ActionAttackContainer)action;
        //generate damage
        int damage = Random.Range(attack.InclusiveMinDamage, attack.ExclusiveMaxDamage);
        //loop through and apply all attacker filters
        foreach (ActionFilter filter in offensiveCharacter.ActionFilters) {
            if (filter.Type == FilterType.AttackActor) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                damage = (int)o.Execute(damage, filter);
            }
        }
        //loop through and apply all defender filters
        foreach (ActionFilter filter in defensiveCharacter.ActionFilters) {
            if (filter.Type == FilterType.AttackRecipient) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                damage = (int)o.Execute(damage, filter);
            }
        }
        //Execute the action

        defensiveCharacter.TakeDamage(damage);

    }
}
