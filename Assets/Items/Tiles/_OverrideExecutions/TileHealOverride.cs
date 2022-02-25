using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealOverride : TileOverride {
    public void Execute(Character recipientCharacter, Character actorCharacter, List<Die> dice, Tile tile) {
        int healing = DieUtility.SumDice(dice); // Generate damage

        // Loop through and apply all attacker filters
        foreach (ActionFilter filter in actorCharacter.ActionFilters) {
            if (filter.Type == FilterType.BuffActor) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                healing = (int)o.Execute(healing, filter);
            }
        }

        // Loop through and apply all defender filters
        foreach (ActionFilter filter in actorCharacter.ActionFilters) {
            if (filter.Type == FilterType.BuffRecipient) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                healing = (int)o.Execute(healing, filter);
            }
        }

        actorCharacter.Heal(healing); // Execute the action
    }
}
