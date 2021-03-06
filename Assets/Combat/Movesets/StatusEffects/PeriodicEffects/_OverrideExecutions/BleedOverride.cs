using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedOverride : PeriodicEffectOverride
{
    public override void Execute(CombatCharacter character, PeriodicEffect effect) {
        character.TakeDamage(character.Health / 10);
    }
}
