using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShieldOverride : TileOverride {

    public void Execute(CombatCharacter recipientCharacter, CombatCharacter actorCharacter, List<Die> dice, Tile tile) {
        Debug.Log("Shield");
        tile.TileCharges--;

        StatusEffectContainer effectContainer = tile.OptionalStatusEffects[0];
        ActionFilter shield = new ActionFilter(ActionFilterName.Shield, FilterType.AttackRecipient, 1000, DieUtility.SumDice(dice), 1);

        shield.FormattedName = effectContainer.FormattedName;
        shield.Description = effectContainer.Description;
        shield.Color = effectContainer.Color;

        shield = (ActionFilter)ActionFilterUtility.ApplyFiltersOfType(shield, actorCharacter.ActionFilters, FilterType.BuffActor);
        shield = (ActionFilter)ActionFilterUtility.ApplyFiltersOfType(shield, actorCharacter.ActionFilters, FilterType.BuffRecipient);

        actorCharacter.AddActionFilter(shield);

    }
}
