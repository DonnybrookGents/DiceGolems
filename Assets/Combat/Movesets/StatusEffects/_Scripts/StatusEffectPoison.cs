using System.Collections.Generic;
using UnityEngine;
public class StatusEffectPoison : StatusEffect {
    public static readonly string NAME = "Poison";
    public StatusEffectPoison(int count, int level, int priority = 20) : base(count, level, priority) { }

    public override void Execute(Character character) {
        character.Health -= Level;
    }
    public override string GetName() {
        return NAME;
    }


}
