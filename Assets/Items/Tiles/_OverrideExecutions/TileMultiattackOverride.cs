using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMultiattackOverride : TileOverride
{
        public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, List<Die> dice, Tile tile) {
        //generate damage
        int damage = DieUtility.SumDice(dice);
        tile.TileCharges--;

        damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, offensiveCharacter.ActionFilters, FilterType.AttackActor);
        damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, defensiveCharacter.ActionFilters, FilterType.AttackRecipient);

        //Execute the action
        defensiveCharacter.TakeDamage(damage);
    }
}
