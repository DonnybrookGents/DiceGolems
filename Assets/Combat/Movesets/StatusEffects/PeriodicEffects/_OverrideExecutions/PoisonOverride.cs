using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonOverride : PeriodicEffectOverride
{
    public override void Execute(CombatCharacter character, PeriodicEffect effect) {
        character.TakeDamage(effect.Efficacy);
    }
}