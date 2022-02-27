using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStrengthenOverride : TileOverride {
    public void Execute(CombatCharacter recipientCharacter, CombatCharacter actorCharacter, List<Die> dice, Tile tile) {
        tile.TileCharges--;

        StatusEffectContainer effectContainer = tile.OptionalStatusEffects[0];
        ActionFilter strength = new ActionFilter(ActionFilterName.Strength, FilterType.AttackRecipient, 10, 1, 999);

        strength.FormattedName = effectContainer.FormattedName;
        strength.Description = effectContainer.Description;
        strength.Color = effectContainer.Color;

        strength = (ActionFilter)ActionFilterUtility.ApplyFiltersOfType(strength, actorCharacter.ActionFilters, FilterType.BuffActor);
        strength = (ActionFilter)ActionFilterUtility.ApplyFiltersOfType(strength, actorCharacter.ActionFilters, FilterType.BuffRecipient);

        actorCharacter.AddActionFilter(strength);

    }
}
