using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionDebuffOverride : ActionOverride {
    public ActionDebuffOverride() {

    }

    public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, ActionContainer action) {
        ActionDebuffContainer debuff = (ActionDebuffContainer)action;

        StatusEffect negativeEffect = new StatusEffect();
        if (debuff.StatusEffectType == StatusEffectType.PeriodicEffect) {
            PeriodicEffectContainer effectContainer = (PeriodicEffectContainer)debuff.statusEffect;
            negativeEffect = new PeriodicEffect(effectContainer.Name, effectContainer.Priority, debuff.Efficacy, debuff.Cooldown);
        } else if (debuff.StatusEffectType == StatusEffectType.ActionFilter) {
            ActionFilterContainer filterContainer = (ActionFilterContainer)debuff.statusEffect;
            negativeEffect = new ActionFilter(filterContainer.Name, filterContainer.Type, filterContainer.Priority, debuff.Efficacy, debuff.Cooldown);
        }


        negativeEffect = (StatusEffect)ActionFilterUtility.ApplyFiltersOfType(negativeEffect, offensiveCharacter.ActionFilters, FilterType.DebuffActor);
        negativeEffect = (StatusEffect)ActionFilterUtility.ApplyFiltersOfType(negativeEffect, defensiveCharacter.ActionFilters, FilterType.DebuffRecipient);



        //Execute the action
        if (debuff.StatusEffectType == StatusEffectType.PeriodicEffect) {
            defensiveCharacter.AddPeriodicEffect((PeriodicEffect)negativeEffect);
        } else if (debuff.StatusEffectType == StatusEffectType.ActionFilter) {
            defensiveCharacter.AddActionFilter((ActionFilter)negativeEffect);
        }

    }
}
