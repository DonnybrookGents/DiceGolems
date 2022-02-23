using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonOverride : PeriodicEffectOverride
{
    public override void Execute(Character character, PeriodicEffect effect) {
        character.TakeDamage(effect.Efficacy);
    }
}