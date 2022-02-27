using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDoubleEdgeOverride : TileOverride {
    public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, List<Die> dice, Tile tile) {
        int damage = DieUtility.SumDice(dice) * 2;
        tile.TileCharges--;

        damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, offensiveCharacter.ActionFilters, FilterType.AttackActor);
        damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, defensiveCharacter.ActionFilters, FilterType.AttackRecipient);

        //Execute the action
        defensiveCharacter.TakeDamage(damage);
        offensiveCharacter.TakeDamage(damage/2);
    }
}
