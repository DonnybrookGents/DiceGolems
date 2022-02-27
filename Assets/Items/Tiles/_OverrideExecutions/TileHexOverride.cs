using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHexOverride : TileOverride {

    public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, List<Die> dice, Tile tile) {
        //generate damage


        
        int effectNumber = dice[0].Value % tile.OptionalStatusEffects.Count;
        int effectEfficacy = 1;
        int effectCooldown = 3;
        FilterType filterType = FilterType.AttackActor;

        switch (effectNumber){
            case 1:
            effectEfficacy = 1;
            effectCooldown = 3; 
            break;
            case 2:
            effectEfficacy = 1;
            effectCooldown = 3; 
            break;
            case 3:
            effectEfficacy = 1;
            effectCooldown = 3; 
            break;
            case 4:
            effectEfficacy = 1;
            effectCooldown = 3; 
            break;
            case 5:
            effectEfficacy = 1;
            effectCooldown = 3; 
            break;
            case 6:
            effectEfficacy = 1;
            effectCooldown = 3; 
            break;


        }
        StatusEffectContainer effectContainer = tile.OptionalStatusEffects[effectNumber];
        StatusEffect negativeEffect = new StatusEffect();
        bool isPeriodic = effectContainer.GetType() == typeof(PeriodicEffectContainer);
        if (isPeriodic) {
            PeriodicEffectContainer periodicEffectContainer = (PeriodicEffectContainer)effectContainer;
            negativeEffect = new PeriodicEffect(periodicEffectContainer.Name, periodicEffectContainer.Priority, effectEfficacy, effectCooldown);
        } else {
            ActionFilterContainer actionFilterContainer = (ActionFilterContainer)effectContainer;
            negativeEffect = new ActionFilter(actionFilterContainer.Name, filterType, actionFilterContainer.Priority, effectEfficacy, effectCooldown);
        }


        negativeEffect = (StatusEffect)ActionFilterUtility.ApplyFiltersOfType(negativeEffect, offensiveCharacter.ActionFilters, FilterType.DebuffActor);
        negativeEffect = (StatusEffect)ActionFilterUtility.ApplyFiltersOfType(negativeEffect, defensiveCharacter.ActionFilters, FilterType.DebuffRecipient);



        //Execute the action
        if (isPeriodic) {
            defensiveCharacter.AddPeriodicEffect((PeriodicEffect)negativeEffect);
        } else {
            defensiveCharacter.AddActionFilter((ActionFilter)negativeEffect);
        }


    }
}
