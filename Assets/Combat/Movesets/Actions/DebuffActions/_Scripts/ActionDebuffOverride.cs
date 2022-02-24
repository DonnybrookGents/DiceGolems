using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionDebuffOverride : ActionOverride {
    public ActionDebuffOverride() {

    }

    public void Execute(Character defensiveCharacter, Character offensiveCharacter, ActionContainer action) {
        ActionDebuffContainer debuff = (ActionDebuffContainer)action;

        //generate damage
        StatusEffect negativeEffect = new StatusEffect();
        if (debuff.StatusEffectType == StatusEffectType.PeriodicEffect) {
            PeriodicEffectContainer effectContainer = (PeriodicEffectContainer)debuff.statusEffect;
            negativeEffect = new PeriodicEffect(effectContainer.Name, effectContainer.Priority, debuff.Efficacy, debuff.Cooldown);
        } else if (debuff.StatusEffectType == StatusEffectType.ActionFilter) {
            ActionFilterContainer filterContainer = (ActionFilterContainer)debuff.statusEffect;
            negativeEffect = new ActionFilter(filterContainer.Name, filterContainer.Type, filterContainer.Priority, debuff.Efficacy, debuff.Cooldown);
        }

        //loop through and apply all attacker filters
        foreach (ActionFilter filter in offensiveCharacter.ActionFilters) {
            if (filter.Type == FilterType.DebuffActor) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                negativeEffect = (StatusEffect)o.Execute(negativeEffect, filter);
            }
        }

        //loop through and apply all defender filters
        foreach (ActionFilter filter in defensiveCharacter.ActionFilters) {
            if (filter.Type == FilterType.DebuffRecipient) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                negativeEffect = (StatusEffect)o.Execute(negativeEffect, filter);
            }
        }

        //Execute the action
        if (debuff.StatusEffectType == StatusEffectType.PeriodicEffect) {
            defensiveCharacter.AddPeriodicEffect((PeriodicEffect)negativeEffect);
        } else if (debuff.StatusEffectType == StatusEffectType.ActionFilter) {
            defensiveCharacter.AddActionFilter((ActionFilter)negativeEffect);
        }

    }
}
