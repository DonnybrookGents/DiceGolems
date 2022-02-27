using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealOverride : TileOverride {
    public void Execute(CombatCharacter recipientCharacter, CombatCharacter actorCharacter, List<Die> dice, Tile tile) {
        int healing = DieUtility.SumDice(dice);
        tile.TileCharges--;

        healing = (int)ActionFilterUtility.ApplyFiltersOfType(healing, actorCharacter.ActionFilters, FilterType.SupportActor);
        healing = (int)ActionFilterUtility.ApplyFiltersOfType(healing, recipientCharacter.ActionFilters, FilterType.SupportRecipient);

        recipientCharacter.Heal(healing); // Execute the action
    }
}
