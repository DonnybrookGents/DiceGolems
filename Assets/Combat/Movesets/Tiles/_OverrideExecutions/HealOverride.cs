using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverride : TileOverride {
    public void Execute(Character recipientCharacter, Character actorCharacter, List<Die> dice, Tile tile){
        //generate damage
        int healing = DieUtility.SumDice(dice);
        //loop through and apply all attacker filters
        foreach(ActionFilter filter in actorCharacter.ActionFilters){
            if(filter.Type == FilterType.BuffActor){
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                o.Execute(healing, filter);
            }
        }
        //loop through and apply all defender filters
        foreach(ActionFilter filter in recipientCharacter.ActionFilters){
            if(filter.Type == FilterType.BuffRecipient){
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                o.Execute(healing, filter);
            }
        }
        //Execute the action

        recipientCharacter.Heal(healing);
    }
}
