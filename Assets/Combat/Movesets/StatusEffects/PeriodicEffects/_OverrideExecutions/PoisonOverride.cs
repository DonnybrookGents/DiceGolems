using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonOverride : StatusEffectOverride {
    public int Efficacy;
    public override void Execute(Character defenseCharacter, Character attackCharacter) {
        defenseCharacter.TakeDamage(Efficacy);
    }
}
