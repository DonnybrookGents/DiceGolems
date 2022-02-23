using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedOverride : PeriodicEffectOverride
{
    public override void Execute(Character character, PeriodicEffect effect) {
        character.TakeDamage(character.MaxHealth / 10);
    }
}
