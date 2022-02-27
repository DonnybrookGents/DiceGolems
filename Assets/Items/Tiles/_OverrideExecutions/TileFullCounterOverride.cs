using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFullCounterOverride : TileOverride {
    public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, List<Die> dice, Tile tile) {
        int startingCost = 20;
        int costIncrease = 10;
        int diceSum = DieUtility.SumDice(dice);
        if (tile.TileParameters[0].ParameterValue > 0) {
            tile.TileParameters[0].ParameterValue -= diceSum;
            if (tile.TileParameters[0].ParameterValue <= 0) {
                tile.TileCharges--;
                PlayerCombatController pc = (PlayerCombatController)offensiveCharacter;
                int damage = pc.StartingCombatHealth - pc.Health;
                damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, offensiveCharacter.ActionFilters, FilterType.AttackActor);
                damage = (int)ActionFilterUtility.ApplyFiltersOfType(damage, defensiveCharacter.ActionFilters, FilterType.AttackRecipient);
                defensiveCharacter.TakeDamage(damage);
                tile.TileParameters[0].ParameterValue = startingCost + costIncrease;
            }
        }

    }
}
